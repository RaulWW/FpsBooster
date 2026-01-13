using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace FpsBooster.Services
{
    public class NetworkResult
    {
        public long Ping { get; set; }
        public double Jitter { get; set; }
        public double PacketLoss { get; set; }
        public string Status { get; set; } = "";
    }

    public class NetworkService
    {
        public event Action<NetworkResult>? OnResultReceived;
        public event Action<string>? OnLogReceived;

        private readonly List<long> _pingHistory = new List<long>();
        private readonly IPingProvider _pingProvider;
        private int _sentCount = 0;
        private int _lostCount = 0;

        public NetworkService(IPingProvider? pingProvider = null)
        {
            _pingProvider = pingProvider ?? new DefaultPingProvider();
        }

        public async Task RunTestAsync(string host, CancellationToken ct)
        {
            // Handle IP:Port format (strip port for ICMP ping)
            if (host.Contains(":"))
            {
                host = host.Split(':')[0];
            }

            _pingHistory.Clear();
            _sentCount = 0;
            _lostCount = 0;

            while (!ct.IsCancellationRequested)
            {
                try
                {
                    _sentCount++;
                    var reply = await _pingProvider.SendPingAsync(host, 1000);
                    
                    var result = new NetworkResult();

                    if (reply.Status == IPStatus.Success)
                    {
                        long currentPing = reply.RoundtripTime;
                        _pingHistory.Add(currentPing);
                        
                        result.Ping = currentPing;
                        result.Jitter = CalculateJitter();
                        result.Status = "Success";
                        OnLogReceived?.Invoke($"Reply from {host}: bytes=32 time={currentPing}ms TTL={reply.Ttl}");
                    }
                    else
                    {
                        _lostCount++;
                        result.Status = reply.Status.ToString();
                        OnLogReceived?.Invoke($"Request timed out ({reply.Status})");
                    }

                    result.PacketLoss = (double)_lostCount / _sentCount * 100;
                    OnResultReceived?.Invoke(result);
                }
                catch (Exception ex)
                {
                    OnLogReceived?.Invoke($"Error: {ex.Message}");
                }

                await Task.Delay(1000, ct);
            }
        }

        private double CalculateJitter()
        {
            if (_pingHistory.Count < 2) return 0;
            
            double sumOfDifferences = 0;
            for (int i = 1; i < _pingHistory.Count; i++)
            {
                sumOfDifferences += Math.Abs(_pingHistory[i] - _pingHistory[i - 1]);
            }
            
            return sumOfDifferences / (_pingHistory.Count - 1);
        }
    }
}
