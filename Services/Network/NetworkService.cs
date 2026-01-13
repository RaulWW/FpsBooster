using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace FpsBooster.Services.Network
{
    public class NetworkDiagnosticResult
    {
        public long Ping { get; set; }
        public double Jitter { get; set; }
        public double PacketLoss { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class NetworkService
    {
        public event Action<NetworkDiagnosticResult>? ResultProcessed;
        public event Action<string>? LogCaptured;

        private readonly List<long> _responseTimes = new List<long>();
        private readonly IPingProvider _pingProvider;
        private int _sentPacketsCount = 0;
        private int _lostPacketsCount = 0;

        public NetworkService(IPingProvider? pingProvider = null)
        {
            _pingProvider = pingProvider ?? new DefaultPingProvider();
        }

        public async Task RunTestAsync(string host, CancellationToken cancellationToken)
        {
            string cleanHost = ExtractHost(host);
            ResetState();

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    _sentPacketsCount++;
                    var response = await _pingProvider.PingHostAsync(cleanHost, 1000);
                    
                    var result = new NetworkDiagnosticResult();

                    if (response.Status == IPStatus.Success)
                    {
                        ProcessSuccessfulResponse(cleanHost, response, result);
                    }
                    else
                    {
                        ProcessFailedResponse(response, result);
                    }

                    result.PacketLoss = (double)_lostPacketsCount / _sentPacketsCount * 100;
                    ResultProcessed?.Invoke(result);
                }
                catch (Exception ex)
                {
                    LogCaptured?.Invoke($"Error: {ex.Message}");
                }

                await Task.Delay(1000, cancellationToken);
            }
        }

        private string ExtractHost(string host)
        {
            return host.Contains(":") ? host.Split(':')[0] : host;
        }

        private void ResetState()
        {
            _responseTimes.Clear();
            _sentPacketsCount = 0;
            _lostPacketsCount = 0;
        }

        private void ProcessSuccessfulResponse(string host, PingDiagnosticResponse response, NetworkDiagnosticResult result)
        {
            long currentPing = response.RoundtripTime;
            _responseTimes.Add(currentPing);
            
            result.Ping = currentPing;
            result.Jitter = CalculateAverageJitter();
            result.Status = "Success";
            
            LogCaptured?.Invoke($"Reply from {host}: bytes=32 time={currentPing}ms TTL={response.TimeToLive}");
        }

        private void ProcessFailedResponse(PingDiagnosticResponse response, NetworkDiagnosticResult result)
        {
            _lostPacketsCount++;
            result.Status = response.Status.ToString();
            LogCaptured?.Invoke($"Request timed out ({response.Status})");
        }

        private double CalculateAverageJitter()
        {
            if (_responseTimes.Count < 2) return 0;
            
            double sumOfDifferences = 0;
            for (int i = 1; i < _responseTimes.Count; i++)
            {
                sumOfDifferences += Math.Abs(_responseTimes[i] - _responseTimes[i - 1]);
            }
            
            return sumOfDifferences / (_responseTimes.Count - 1);
        }
    }
}
