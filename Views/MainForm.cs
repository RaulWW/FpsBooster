using System.Runtime.InteropServices;
using FpsBooster.Services;
using FpsBooster.Services.Network;
using FpsBooster.Views.Controls;
using FpsBooster.Views.Helpers;

namespace FpsBooster.Views;

public partial class MainForm : Form
{
    private readonly BoosterService _boosterService;
    private readonly NetworkService _networkService;
    private readonly VersionCheckService _versionCheckService;
    private CancellationTokenSource? _networkCts;

    public MainForm()
    {
        InitializeComponent();
        
        _boosterService = new BoosterService();
        _networkService = new NetworkService();
        _versionCheckService = new VersionCheckService("RaulWW", "FpsBooster", Theme.AppVersion);

        InitializeEvents();
        InitializeNavigation();
        InitializeCustomTitleBar();
        
        // Check for updates asynchronously after load
        this.Load += async (s, e) => await CheckForUpdatesAsync();
    }

    private void InitializeEvents()
    {
        _boosterService.OnLogMessage += AddLog;
        _boosterService.OnProgressUpdate += UpdateProgress;

        _networkService.ResultProcessed += UpdateNetworkResults;
        _networkService.LogCaptured += AddNetworkLog;

        btnBoost.Click += async (s, e) => await RunBoost();
        btnSaveCS2.Click += (s, e) => SaveCS2Config();
        btnStartNetworkTest.Click += async (s, e) => await ToggleNetworkTest();
        btnLoadFaceit.Click += (s, e) => txtTargetIp.Text = "169.150.220.9:20070";
        btnLoadGC.Click += (s, e) => txtTargetIp.Text = "203.159.80.100:27027";

        rtbCS2Config.TextChanged += (s, e) => RichTextEditorHelper.ApplyCs2SyntaxHighlighting(rtbCS2Config, Theme.Text);
        
        // Monetization Events
        lblDonate.LinkClicked += (s, e) => ShowDonateForm();
        btnAffiliate.Click += (s, e) => OpenUrl(Theme.UrlAffiliate);
    }

    private void InitializeNavigation()
    {
        btnMenuBoost.Click += (s, e) => SwitchTab(panelBoost, btnMenuBoost);
        btnMenuCS2.Click += (s, e) => {
            SwitchTab(panelCS2, btnMenuCS2);
            LoadCS2Config();
        };
        btnMenuNetwork.Click += (s, e) => SwitchTab(panelNetwork, btnMenuNetwork);
        btnMenuDownloads.Click += (s, e) => SwitchTab(panelDownloads, btnMenuDownloads);
        btnMenuDocs.Click += (s, e) => SwitchTab(panelDocs, btnMenuDocs);
        
        btnInstallFeatures.Click += async (s, e) => await InstallSelectedFeatures();
        btnInstallVisualCpp.Click += async (s, e) => await InstallVisualCppRedistributables();
        chkLog.CheckedChanged += (s, e) => rtbDownloadsLog.Visible = chkLog.Checked;
    }

    private void InitializeCustomTitleBar()
    {
        btnClose.Click += (s, e) => Application.Exit();
        btnMinimize.Click += (s, e) => this.WindowState = FormWindowState.Minimized;
        panelTitleBar.MouseDown += TitleBar_MouseDown;
    }

    private void SwitchTab(Panel activePanel, MenuButton activeButton)
    {
        panelBoost.Visible = (activePanel == panelBoost);
        panelCS2.Visible = (activePanel == panelCS2);
        panelNetwork.Visible = (activePanel == panelNetwork);
        panelDownloads.Visible = (activePanel == panelDownloads);
        panelDocs.Visible = (activePanel == panelDocs);
        
        btnMenuBoost.IsActive = (activeButton == btnMenuBoost);
        btnMenuCS2.IsActive = (activeButton == btnMenuCS2);
        btnMenuNetwork.IsActive = (activeButton == btnMenuNetwork);
        btnMenuDownloads.IsActive = (activeButton == btnMenuDownloads);
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
            rtbCS2Config.Text = GetDefaultCs2Config();
            RichTextEditorHelper.ApplyCs2SyntaxHighlighting(rtbCS2Config, Theme.Text);
        }
    }

    private string GetDefaultCs2Config()
    {
        return @"// +mat_disable_fancy_blending 1 +exec autoexec.cfg -refresh 240
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
    }

    // Syntax highlighting logic removed as it's now in RichTextEditorHelper

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
        
        rtbLog.SelectionStart = start;
        rtbLog.SelectionLength = 0;
        rtbLog.SelectionColor = Theme.TextDim; // Reset color to avoid bleeding
        
        rtbLog.AppendText(fullMessage);

        RichTextEditorHelper.HighlightLogTags(rtbLog, start, fullMessage, Theme.Success);

        rtbLog.SelectionStart = rtbLog.Text.Length;
        rtbLog.ScrollToCaret();
    }

    // HighlightLogTags removed as it's now in RichTextEditorHelper

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

    private void UpdateNetworkResults(NetworkDiagnosticResult result)
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

    private async Task InstallSelectedFeatures()
    {
        if (!chkDotNet.Checked && !chkVisualCpp.Checked)
        {
            MessageBox.Show("Please select at least one feature to install.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        btnInstallFeatures.Enabled = false;
        btnInstallFeatures.Text = "  INSTALLING...";
        
        try 
        {
            var installer = new WindowsFeaturesInstaller();
            var progress = new Progress<string>(status => AddDownloadLog(status));

            if (chkDotNet.Checked)
            {
                AddDownloadLog("Starting .NET Framework installation...");
                await installer.InstallDotNetFrameworkAsync();
                AddDownloadLog(".NET Framework installation command sent.");
            }

            if (chkVisualCpp.Checked)
            {
                AddDownloadLog("Starting Visual C++ Redistributables installation...");
                await installer.InstallVisualCppRuntimesAsync(progress);
            }

            MessageBox.Show("Installation process finished!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            AddDownloadLog($"FATAL ERROR: {ex.Message}");
            MessageBox.Show($"Error during installation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnInstallFeatures.Enabled = true;
            btnInstallFeatures.Text = "  INSTALL SELECTED";
        }
    }

    private void AddDownloadLog(string message)
    {
        if (!chkLog.Checked) return;

        if (rtbDownloadsLog.InvokeRequired)
        {
            rtbDownloadsLog.Invoke(new Action(() => AddDownloadLog(message)));
            return;
        }

        string timestamp = DateTime.Now.ToString("HH:mm:ss");
        rtbDownloadsLog.AppendText($"[{timestamp}] {message}{Environment.NewLine}");
        rtbDownloadsLog.SelectionStart = rtbDownloadsLog.TextLength;
        rtbDownloadsLog.ScrollToCaret();
    }

    private async Task InstallVisualCppRedistributables()
    {
        btnInstallVisualCpp.Enabled = false;
        string originalText = btnInstallVisualCpp.Text;
        string originalInfo = lblDownloadsInfo.Text;

        try
        {
            var progress = new Progress<string>(status => {
                lblDownloadsInfo.Text = status;
                AddDownloadLog(status);
                lblDownloadsInfo.Refresh();
            });

            var installer = new WindowsFeaturesInstaller();
            await installer.InstallVisualCppRuntimesAsync(progress);

            MessageBox.Show("Visual C++ Runtimes installation process finished.\nIf the console window is closed, it's done.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error installing Visual C++ Runtimes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnInstallVisualCpp.Enabled = true;
            btnInstallVisualCpp.Text = originalText;
            lblDownloadsInfo.Text = originalInfo;
        }
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
        rtbNetworkLog.SelectionStart = rtbNetworkLog.Text.Length;
        rtbNetworkLog.ScrollToCaret();
    }

    private async Task CheckForUpdatesAsync()
    {
        try
        {
            var result = await _versionCheckService.CheckForUpdatesAsync();

            if (result.IsSuccess && result.IsNewVersionAvailable)
            {
                // Show update notification in the title bar or footer
                var updateMessage = $"🔔 Nova versão disponível: v{result.LatestVersion} (Você está usando: v{result.CurrentVersion})";
                
                // Create a notification link in footer
                var lblUpdate = new LinkLabel
                {
                    Text = $"🔔 Atualização disponível: v{result.LatestVersion}",
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                    LinkColor = Color.LightGreen,
                    ActiveLinkColor = Color.White,
                    VisitedLinkColor = Color.LightGreen,
                    LinkBehavior = LinkBehavior.HoverUnderline,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(10, 0, 0, 5),
                    Cursor = Cursors.Hand,
                    Dock = DockStyle.Top,
                    Height = 25
                };

                lblUpdate.LinkClicked += (s, e) =>
                {
                    var dialogResult = MessageBox.Show(
                        $"Uma nova versão está disponível!\n\n" +
                        $"Versão Atual: v{result.CurrentVersion}\n" +
                        $"Última Versão: v{result.LatestVersion}\n\n" +
                        $"Deseja abrir a página de download?",
                        "Atualização Disponível",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information
                    );

                    if (dialogResult == DialogResult.Yes)
                    {
                        OpenUrl(result.DownloadUrl);
                    }
                };

                // Add to footer at the top (above donate link)
                if (footer.InvokeRequired)
                {
                    footer.Invoke(() => footer.Controls.Add(lblUpdate));
                }
                else
                {
                    footer.Controls.Add(lblUpdate);
                }

                // Resize footer to accommodate update notification
                footer.Height = 85;
            }
        }
        catch (Exception)
        {
            // Silently fail - don't bother user with update check errors
        }
    }

    private void ShowDonateForm()
    {
        using var donateForm = new DonateForm();
        donateForm.ShowDialog(this);
    }

    private void OpenUrl(string url)
    {
        try
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Could not open link: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
