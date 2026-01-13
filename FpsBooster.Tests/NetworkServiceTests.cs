using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using FpsBooster.Services;
using Moq;
using Xunit;

namespace FpsBooster.Tests
{
    public class NetworkServiceTests
    {
        [Fact]
        public async Task RunTestAsync_ShouldCalculateCorrectMetrics()
        {
            // Arrange
            var mockPing = new Mock<IPingProvider>();
            var service = new NetworkService(mockPing.Object);
            
            // Setup sequence of pings: 100ms, 120ms, 110ms
            mockPing.SetupSequence(p => p.SendPingAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new PingResponse { Status = IPStatus.Success, RoundtripTime = 100, Ttl = 64 })
                .ReturnsAsync(new PingResponse { Status = IPStatus.Success, RoundtripTime = 120, Ttl = 64 })
                .ReturnsAsync(new PingResponse { Status = IPStatus.Success, RoundtripTime = 110, Ttl = 64 });

            var results = new List<NetworkResult>();
            service.OnResultReceived += (res) => results.Add(res);

            var cts = new CancellationTokenSource();

            // Act
            var testTask = service.RunTestAsync("8.8.8.8", cts.Token);
            
            // Wait for 3 results (pings are every 1s in the service, but we can't wait that long in a unit test comfortably)
            // Actually, the service has a Task.Delay(1000). Let's refactor the service to allow a smaller delay for testing or just test the logic differently.
            // For now, I'll just test that Jitter calculation logic in a separate public/internal method or by mocking Task.Delay? 
            // Better to test the results after a few pings.
            
            await Task.Delay(100); // Give it a tiny bit of time to start
            while (results.Count < 3) 
            {
                await Task.Delay(10);
                if (results.Count == 3) break;
            }
            cts.Cancel();

            // Assert
            Assert.Equal(3, results.Count);
            
            // Result 1: 100ms, Jitter 0, Loss 0
            Assert.Equal(100, results[0].Ping);
            Assert.Equal(0, results[0].Jitter);
            Assert.Equal(0, results[0].PacketLoss);

            // Result 2: 120ms, Jitter |120-100| = 20/1 = 20, Loss 0
            Assert.Equal(120, results[1].Ping);
            Assert.Equal(20, results[1].Jitter);

            // Result 3: 110ms, Jitter (|120-100| + |110-120|) / 2 = (20 + 10) / 2 = 15, Loss 0
            Assert.Equal(110, results[2].Ping);
            Assert.Equal(15, results[2].Jitter);
        }

        [Fact]
        public async Task RunTestAsync_ShouldCalculatePacketLoss()
        {
            // Arrange
            var mockPing = new Mock<IPingProvider>();
            var service = new NetworkService(mockPing.Object);
            
            mockPing.SetupSequence(p => p.SendPingAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new PingResponse { Status = IPStatus.Success, RoundtripTime = 100 })
                .ReturnsAsync(new PingResponse { Status = IPStatus.TimedOut })
                .ReturnsAsync(new PingResponse { Status = IPStatus.Success, RoundtripTime = 100 });

            var results = new List<NetworkResult>();
            service.OnResultReceived += (res) => results.Add(res);

            var cts = new CancellationTokenSource();

            // Act
            var testTask = service.RunTestAsync("8.8.8.8", cts.Token);
            while (results.Count < 3) await Task.Delay(10);
            cts.Cancel();

            // Assert
            Assert.Equal(3, results.Count);
            Assert.Equal(0, results[0].PacketLoss);
            Assert.Equal(50, results[1].PacketLoss); // 1 success, 1 fail = 50%
            Assert.Equal(33.3, results[2].PacketLoss, 1); // 2 success, 1 fail = 33.3%
        }
    }
}
