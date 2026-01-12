using FpsBooster.Services;

namespace FpsBooster.Views;

public partial class MainForm : Form
{
    private readonly BoosterService _boosterService;

    public MainForm()
    {
        InitializeComponent();
        _boosterService = new BoosterService();
        
        // Wire up events
        _boosterService.OnLogMessage += AddLog;
        _boosterService.OnProgressUpdate += UpdateProgress;

        // Button click
        btnBoost.Click += async (s, e) => await RunBoost();
    }

    private void AddLog(string message)
    {
        if (rtbLog.InvokeRequired)
        {
            rtbLog.Invoke(new Action(() => AddLog(message)));
            return;
        }

        string timestamp = DateTime.Now.ToString("HH:mm:ss");
        rtbLog.AppendText($"[{timestamp}] {message}{Environment.NewLine}");
        rtbLog.SelectionStart = rtbLog.Text.Length;
        rtbLog.ScrollToCaret();
    }

    private void UpdateProgress(int value)
    {
        if (progressBar.InvokeRequired)
        {
            progressBar.Invoke(new Action(() => UpdateProgress(value)));
            return;
        }
        progressBar.Value = value;
    }

    private async Task RunBoost()
    {
        btnBoost.Enabled = false;
        btnBoost.Text = "  RUNNING...";
        rtbLog.Clear();
        
        try 
        {
            await _boosterService.ApplyUltimatePerformanceAsync();
            MessageBox.Show("Ultimate Performance Plan Applied!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            AddLog($"FATAL ERROR: {ex.Message}");
        }
        finally
        {
            btnBoost.Enabled = true;
            btnBoost.Text = "  APPLY ULTIMATE PERFORMANCE";
        }
    }
}
