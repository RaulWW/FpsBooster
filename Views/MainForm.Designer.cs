namespace FpsBooster.Views;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.sidebar = new Panel();
        this.mainContent = new Panel();
        
        // Panels for views
        this.panelBoost = new Panel();
        this.panelCS2 = new Panel();
        
        // Navigation buttons
        this.btnMenuBoost = new MenuButton();
        this.btnMenuCS2 = new MenuButton();
        
        // Boost tab controls
        this.lblTitle = new Label();
        this.btnBoost = new ModernButton();
        this.progressBar = new ModernProgressBar();
        this.rtbLog = new RichTextBox();
        this.lblIcon = new Label();
        
        // CS2 tab controls
        this.lblCS2Title = new Label();
        this.rtbCS2Config = new RichTextBox();
        this.btnSaveCS2 = new ModernButton();
        this.lblCS2Info = new Label();
        
        // Form Configuration
        this.BackColor = Theme.Background;
        this.ForeColor = Theme.Text;
        this.Size = new Size(1100, 700);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "FPS BOOSTER - PREMIUM ELITE";
        this.AutoScaleMode = AutoScaleMode.Dpi;

        // Sidebar Setup
        this.sidebar.Dock = DockStyle.Left;
        this.sidebar.Width = 240;
        this.sidebar.BackColor = Theme.Sidebar;
        
        // Sidebar Content
        this.lblIcon.Text = Theme.IconRocket;
        this.lblIcon.Font = new Font("Segoe MDL2 Assets", 48F);
        this.lblIcon.ForeColor = Theme.Accent;
        this.lblIcon.Dock = DockStyle.Top;
        this.lblIcon.Height = 150;
        this.lblIcon.TextAlign = ContentAlignment.MiddleCenter;
        
        this.btnMenuCS2.Text = "   CONFIG CS2";
        this.btnMenuCS2.Icon = Theme.IconGame;
        this.btnMenuCS2.Dock = DockStyle.Top;
        
        this.btnMenuBoost.Text = "   ULTIMATE BOOST";
        this.btnMenuBoost.IsActive = true;
        this.btnMenuBoost.Icon = Theme.IconRocket;
        this.btnMenuBoost.Dock = DockStyle.Top;

        this.sidebar.Controls.Add(this.btnMenuCS2);
        this.sidebar.Controls.Add(this.btnMenuBoost);
        this.sidebar.Controls.Add(this.lblIcon);

        // Main Content Setup
        this.mainContent.Dock = DockStyle.Fill;
        this.mainContent.BackColor = Theme.Background;

        // Panel Boost Setup
        this.panelBoost.Dock = DockStyle.Fill;
        this.panelBoost.Padding = new Padding(40);
        
        this.lblTitle.Text = "ULTIMATE BOOST";
        this.lblTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
        this.lblTitle.AutoSize = true;
        this.lblTitle.Location = new Point(40, 40);
        this.lblTitle.ForeColor = Color.White;
        
        this.progressBar.Location = new Point(45, 120);
        this.progressBar.Width = 720;
        this.progressBar.Height = 8;
        
        this.btnBoost.Text = "APPLY PERFORMANCE CFG";
        this.btnBoost.Location = new Point(45, 150);
        this.btnBoost.Size = new Size(320, 55);
        this.btnBoost.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        
        this.rtbLog.Location = new Point(45, 230);
        this.rtbLog.Size = new Size(720, 360);
        this.rtbLog.BackColor = Color.FromArgb(15, 15, 15);
        this.rtbLog.ForeColor = Theme.TextDim;
        this.rtbLog.BorderStyle = BorderStyle.None;
        this.rtbLog.Font = new Font("Consolas", 10F);
        this.rtbLog.ReadOnly = true;
        
        this.panelBoost.Controls.Add(this.rtbLog);
        this.panelBoost.Controls.Add(this.btnBoost);
        this.panelBoost.Controls.Add(this.progressBar);
        this.panelBoost.Controls.Add(this.lblTitle);

        // Panel CS2 Setup
        this.panelCS2.Dock = DockStyle.Fill;
        this.panelCS2.Padding = new Padding(40);
        this.panelCS2.Visible = false;

        this.lblCS2Title.Text = "CONFIG CS2";
        this.lblCS2Title.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
        this.lblCS2Title.AutoSize = true;
        this.lblCS2Title.Location = new Point(40, 40);
        this.lblCS2Title.ForeColor = Color.White;

        this.lblCS2Info.Text = "Edit your autoexec.cfg below. Syntax highlighting applies to commands.";
        this.lblCS2Info.ForeColor = Theme.TextDim;
        this.lblCS2Info.Location = new Point(45, 100);
        this.lblCS2Info.AutoSize = true;

        this.rtbCS2Config.Location = new Point(45, 130);
        this.rtbCS2Config.Size = new Size(720, 400);
        this.rtbCS2Config.BackColor = Color.FromArgb(15, 15, 15);
        this.rtbCS2Config.ForeColor = Theme.Text;
        this.rtbCS2Config.BorderStyle = BorderStyle.None;
        this.rtbCS2Config.Font = new Font("Consolas", 11F);
        this.rtbCS2Config.AcceptsTab = true;

        this.btnSaveCS2.Text = "  SAVE AUTOEXEC.CFG";
        this.btnSaveCS2.Location = new Point(45, 550);
        this.btnSaveCS2.Size = new Size(250, 45);
        
        this.panelCS2.Controls.Add(this.btnSaveCS2);
        this.panelCS2.Controls.Add(this.rtbCS2Config);
        this.panelCS2.Controls.Add(this.lblCS2Info);
        this.panelCS2.Controls.Add(this.lblCS2Title);

        this.mainContent.Controls.Add(this.panelBoost);
        this.mainContent.Controls.Add(this.panelCS2);
        
        this.Controls.Add(this.mainContent);
        this.Controls.Add(this.sidebar);
    }

    private Panel sidebar;
    private Panel mainContent;
    private Panel panelBoost;
    private Panel panelCS2;
    private MenuButton btnMenuBoost;
    private MenuButton btnMenuCS2;
    private Label lblTitle;
    private Label lblIcon;
    private ModernButton btnBoost;
    private ModernProgressBar progressBar;
    private RichTextBox rtbLog;

    // CS2 Controls
    private Label lblCS2Title;
    private Label lblCS2Info;
    private RichTextBox rtbCS2Config;
    private ModernButton btnSaveCS2;

    #endregion
}
