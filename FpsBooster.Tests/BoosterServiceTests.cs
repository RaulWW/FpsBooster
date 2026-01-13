using System;
using System.Threading.Tasks;
using FpsBooster.Services;
using Moq;
using Xunit;

namespace FpsBooster.Tests
{
    public class BoosterServiceTests
    {
        [Fact]
        public async Task ApplyUltimatePerformanceAsync_ShouldCallPowerShellWithExpectedScripts()
        {
            // Arrange
            var mockPs = new Mock<IPowerShellService>();
            var booster = new BoosterService(mockPs.Object);
            
            // Act
            await booster.ApplyUltimatePerformanceAsync();
            
            // Assert
            // Check if Power Plan script was called (contains powercfg)
            mockPs.Verify(ps => ps.ExecuteCommandAsync(It.Is<string>(s => s.Contains("powercfg"))), Times.AtLeastOnce());
            
            // Check if Cleanup script was called (contains Get-ChildItem and Remove-Item)
            mockPs.Verify(ps => ps.ExecuteCommandAsync(It.Is<string>(s => s.Contains("Get-ChildItem") && s.Contains("Remove-Item"))), Times.AtLeastOnce());
            
            // Check if DISM/Copilot fix was called
            mockPs.Verify(ps => ps.ExecuteCommandAsync(It.Is<string>(s => s.Contains("Get-AppxPackage") || s.Contains("dism"))), Times.AtLeastOnce());
        }

        [Fact]
        public async Task ApplyUltimatePerformanceAsync_ShouldCaptureLogs()
        {
            // Arrange
            var mockPs = new Mock<IPowerShellService>();
            var booster = new BoosterService(mockPs.Object);
            string? capturedLog = null;
            booster.OnLogMessage += (msg) => capturedLog = msg;

            // Act
            await booster.ApplyUltimatePerformanceAsync();

            // Assert
            Assert.NotNull(capturedLog);
            Assert.Contains("SISTEMA OTIMIZADO AO MÁXIMO!", capturedLog);
        }
    }
}
