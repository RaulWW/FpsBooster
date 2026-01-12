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

        // Navigation
        btnMenuBoost.Click += (s, e) => SwitchTab(panelBoost, btnMenuBoost);
        btnMenuCS2.Click += (s, e) => {
            SwitchTab(panelCS2, btnMenuCS2);
            LoadCS2Config();
        };

        // Button clicks
        btnBoost.Click += async (s, e) => await RunBoost();
        btnSaveCS2.Click += (s, e) => SaveCS2Config();

        // Syntax highlighting logic
        rtbCS2Config.TextChanged += (s, e) => ApplyCS2SyntaxHighlighting();
    }

    private void SwitchTab(Panel activePanel, MenuButton activeButton)
    {
        panelBoost.Visible = (activePanel == panelBoost);
        panelCS2.Visible = (activePanel == panelCS2);
        
        btnMenuBoost.IsActive = (activeButton == btnMenuBoost);
        btnMenuCS2.IsActive = (activeButton == btnMenuCS2);
        
        sidebar.Invalidate(); // Redraw sidebar for activity indicator
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
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                MessageBox.Show($"Directory not found: {directory}\nDefaulting to local save (autoexec.cfg)", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
