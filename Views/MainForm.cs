using System.Runtime.InteropServices;
using FpsBooster.Services;

namespace FpsBooster.Views;

public partial class MainForm : Form
{
    private readonly BoosterService _boosterService;
    private readonly NetworkService _networkService;
    private CancellationTokenSource? _networkCts;

    public MainForm()
    {
        InitializeComponent();
        _boosterService = new BoosterService();
        
        // Wire up events
        _boosterService.OnLogMessage += AddLog;
        _boosterService.OnProgressUpdate += UpdateProgress;

        _networkService = new NetworkService();
        _networkService.OnResultReceived += UpdateNetworkResults;
        _networkService.OnLogReceived += AddNetworkLog;

        // Navigation
        btnMenuBoost.Click += (s, e) => SwitchTab(panelBoost, btnMenuBoost);
        btnMenuCS2.Click += (s, e) => {
            SwitchTab(panelCS2, btnMenuCS2);
            LoadCS2Config();
        };
        btnMenuNetwork.Click += (s, e) => SwitchTab(panelNetwork, btnMenuNetwork);
        btnMenuDocs.Click += (s, e) => SwitchTab(panelDocs, btnMenuDocs);

        // Title Bar
        btnClose.Click += (s, e) => Application.Exit();
        btnMinimize.Click += (s, e) => this.WindowState = FormWindowState.Minimized;
        panelTitleBar.MouseDown += TitleBar_MouseDown;

        // Button clicks
        btnBoost.Click += async (s, e) => await RunBoost();
        btnSaveCS2.Click += (s, e) => SaveCS2Config();
        btnStartNetworkTest.Click += async (s, e) => await ToggleNetworkTest();
        btnLoadFaceit.Click += (s, e) => txtTargetIp.Text = "169.150.220.9:20070";

        // Syntax highlighting logic
        rtbCS2Config.TextChanged += (s, e) => ApplyCS2SyntaxHighlighting();
    }

    private void SwitchTab(Panel activePanel, MenuButton activeButton)
    {
        panelBoost.Visible = (activePanel == panelBoost);
        panelCS2.Visible = (activePanel == panelCS2);
        panelNetwork.Visible = (activePanel == panelNetwork);
        panelDocs.Visible = (activePanel == panelDocs);
        
        btnMenuBoost.IsActive = (activeButton == btnMenuBoost);
        btnMenuCS2.IsActive = (activeButton == btnMenuCS2);
        btnMenuNetwork.IsActive = (activeButton == btnMenuNetwork);
        btnMenuDocs.IsActive = (activeButton == btnMenuDocs);
        
        sidebar.Invalidate(); // Redraw sidebar for activity indicator
    }

    // Window Dragging
    [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
    private extern static void ReleaseCapture();
    [DllImport("user32.DLL", EntryPoint = "SendMessage")]
    private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

    private void TitleBar_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }
    }

    private void LoadCS2Config()
    {
        if (string.IsNullOrEmpty(rtbCS2Config.Text))
        {
            rtbCS2Config.Text = @"// +mat_disable_fancy_blending 1 +exec autoexec.cfg -refresh 240
// C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive\game\csgo\cfg

bind ""KP_0"" ""buy deagle""
bind ""kp_1"" ""buy mp9;""
bind ""kp_2"" ""buy tec9; buy cz75a; buy fiveseven;""
bind ""kp_3"" ""buy ssg08;""
bind ""kp_4"" ""buy m4a1; buy ak47;""
bind ""kp_6"" ""buy famas; buy galilar;""
bind ""KP_7"" ""buy flashbang""
bind ""KP_8"" ""buy hegrenade""
bind ""KP_9"" ""buy defuser""
bind ""KP_MINUS"" ""buy awp""
bind ""KP_ENTER"" ""buy vesthelm""
bind ""KP_PLUS"" ""buy vest""
bind ""KP_MULTIPLY"" ""buy smokegrenade""
bind ""KP_DIVIDE"" ""buy incgrenade;buy molotov""
bind ""KP_DEL"" ""buy tec9; buy fiveseven;""

bind mwheelup +jump
bind tab +showscores

bind ""F5"" ""cl_crosshairsize 1; cl_crosshairthickness 1; cl_crosshairalpha 255; cl_crosshaircolor 1; cl_crosshairgap -4""
bind ""F6"" ""cl_crosshairsize 2; cl_crosshairthickness 1; cl_crosshairalpha 222; cl_crosshaircolor 1; cl_crosshairgap -3""
bind ""F7"" ""cl_crosshairsize 3; cl_crosshairthickness 1; cl_crosshairalpha 255; cl_crosshaircolor 1; cl_crosshairgap -2""

bind ""z"" ""radio""
bind ""x"" ""radio1""
bind ""c"" ""radio2""

unbind mwheeldown

zoom_sensitivity_ratio ""1.1""
sensitivity 1.62
mat_queue_mode 2

fps_max 400
fps_max_ui 500

cl_interp_ratio ""1""
cl_interp ""0.015625""

alias ""+jumpaction"" ""+jump;""
alias ""+throwaction"" ""-attack; -attack2""
alias ""-jumpaction"" ""-jump""
bind ""n"" ""+jumpaction;+throwaction;""

r_fullscreen_gamma 2.22
						
viewmodel_fov ""68""
viewmodel_offset_x ""2.500000""
viewmodel_offset_y ""1.5""
viewmodel_offset_z ""-1.500000""
viewmodel_presetpos ""0""
r_dynamic 0";
            ApplyCS2SyntaxHighlighting();
        }
    }

    private void ApplyCS2SyntaxHighlighting()
    {
        int selectionStart = rtbCS2Config.SelectionStart;
        int selectionLength = rtbCS2Config.SelectionLength;
        
        // Disable drawing to avoid flicker
        rtbCS2Config.SuspendLayout();
        
        // Reset color
        rtbCS2Config.SelectAll();
        rtbCS2Config.SelectionColor = Theme.Text;

        string[] lines = rtbCS2Config.Lines;
        int currentPos = 0;
        
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) 
            {
                currentPos += line.Length + 1;
                continue;
            }

            if (line.StartsWith("//"))
            {
                rtbCS2Config.Select(currentPos, line.Length);
                rtbCS2Config.SelectionColor = Color.Gray;
            }
            else
            {
                // Highlight the command (first word)
                int firstSpace = line.IndexOf(' ');
                if (firstSpace > 0)
                {
                    rtbCS2Config.Select(currentPos, firstSpace);
                    rtbCS2Config.SelectionColor = Color.LightBlue;
                }
                else
                {
                    rtbCS2Config.Select(currentPos, line.Length);
                    rtbCS2Config.SelectionColor = Color.LightBlue;
                }
            }
            currentPos += line.Length + 1;
        }

        // Restore selection
        rtbCS2Config.Select(selectionStart, selectionLength);
        rtbCS2Config.SelectionColor = Theme.Text;
        rtbCS2Config.ResumeLayout();
    }

    private void SaveCS2Config()
    {
        string path = @"C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Global Offensive\game\csgo\cfg\autoexec.cfg";
        try
        {
            // Ensure directory exists (might not on all machines, but user provided this specific path)
            string? directory = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory))
            {
                MessageBox.Show($"Directory not found or invalid: {directory}\nDefaulting to local save (autoexec.cfg)", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                path = "autoexec.cfg";
            }
            
            File.WriteAllText(path, rtbCS2Config.Text);
            MessageBox.Show("CS2 autoexec.cfg saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving config: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void AddLog(string message)
    {
        if (rtbLog.InvokeRequired)
        {
            rtbLog.Invoke(new Action(() => AddLog(message)));
            return;
        }

        string timestamp = DateTime.Now.ToString("HH:mm:ss");
        string fullMessage = $"[{timestamp}] {message}{Environment.NewLine}";
        
        int start = rtbLog.TextLength;
        rtbLog.AppendText(fullMessage);
        int end = rtbLog.TextLength;

        // Apply colors to tags
        HighlightLogTags(start, fullMessage);

        rtbLog.SelectionStart = rtbLog.Text.Length;
        rtbLog.ScrollToCaret();
    }

    private void HighlightLogTags(int startOffset, string text)
    {
        string[] tags = { "[INFO]", "[WARNING]", "[ERROR]", "SUCCESS", "FATAL" };
        foreach (var tag in tags)
        {
            int index = text.IndexOf(tag);
            while (index != -1)
            {
                rtbLog.Select(startOffset + index, tag.Length);
                rtbLog.SelectionColor = Theme.Success; // As requested, tags in green
                rtbLog.SelectionFont = new Font(rtbLog.Font, FontStyle.Bold);
                index = text.IndexOf(tag, index + tag.Length);
            }
        }
        rtbLog.SelectionColor = Theme.TextDim; // Reset
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
            btnBoost.Text = "  APPLY PERFORMANCE CFG";
        }
    }

    private async Task ToggleNetworkTest()
    {
        if (_networkCts != null)
        {
            _networkCts.Cancel();
            _networkCts = null;
            btnStartNetworkTest.Text = "START DIAGNOSTICS";
            btnStartNetworkTest.BackColor = Theme.Accent;
            txtTargetIp.Enabled = true;
            return;
        }

        string host = txtTargetIp.Text.Trim();
        if (string.IsNullOrEmpty(host)) return;

        _networkCts = new CancellationTokenSource();
        btnStartNetworkTest.Text = "STOP DIAGNOSTICS";
        btnStartNetworkTest.BackColor = Color.FromArgb(200, 50, 50); // Red-ish
        txtTargetIp.Enabled = false;
        rtbNetworkLog.Clear();

        try
        {
            await _networkService.RunTestAsync(host, _networkCts.Token);
        }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
            AddNetworkLog($"Error: {ex.Message}");
        }
        finally
        {
            if (_networkCts != null)
            {
                _networkCts.Dispose();
                _networkCts = null;
            }
            btnStartNetworkTest.Text = "START DIAGNOSTICS";
            btnStartNetworkTest.BackColor = Theme.Accent;
            txtTargetIp.Enabled = true;
        }
    }

    private void UpdateNetworkResults(NetworkResult result)
    {
        if (this.InvokeRequired)
        {
            this.Invoke(new Action(() => UpdateNetworkResults(result)));
            return;
        }

        lblPingResult.Text = $"PING: {result.Ping} ms";
        lblJitterResult.Text = $"JITTER: {result.Jitter:F1} ms";
        lblLossResult.Text = $"LOSS: {result.PacketLoss:F1} %";
        
        // Dynamic colors based on quality
        lblPingResult.ForeColor = result.Ping < 50 ? Theme.Success : (result.Ping < 100 ? Color.Yellow : Color.Red);
        lblJitterResult.ForeColor = result.Jitter < 5 ? Theme.Success : (result.Jitter < 15 ? Color.Yellow : Color.Red);
        lblLossResult.ForeColor = result.PacketLoss == 0 ? Theme.Success : Color.Red;
    }

    private void AddNetworkLog(string message)
    {
        if (rtbNetworkLog.InvokeRequired)
        {
            rtbNetworkLog.Invoke(new Action(() => AddNetworkLog(message)));
            return;
        }

        rtbNetworkLog.AppendText($"{message}{Environment.NewLine}");
        rtbNetworkLog.SelectionStart = rtbNetworkLog.Text.Length;
        rtbNetworkLog.ScrollToCaret();
    }
}
