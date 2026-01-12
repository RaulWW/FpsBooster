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
        this.lblTitle = new Label();
        this.btnBoost = new ModernButton();
        this.progressBar = new ModernProgressBar();
        this.rtbLog = new RichTextBox();
        this.lblIcon = new Label();
        
        // Form Configuration
        this.BackColor = Theme.Background;
        this.ForeColor = Theme.Text;
        this.Size = new Size(1000, 650);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "FPS BOOSTER - PREMIUM";
        this.AutoScaleMode = AutoScaleMode.Dpi; // Support for high DPI

        // Sidebar Setup
        this.sidebar.Dock = DockStyle.Left;
        this.sidebar.Width = 240;
        this.sidebar.BackColor = Theme.Sidebar;
        this.sidebar.Padding = new Padding(0, 50, 0, 0);

        // Sidebar Icon
        this.lblIcon.Text = Theme.IconRocket;
        this.lblIcon.Font = new Font("Segoe MDL2 Assets", 48F);
        this.lblIcon.ForeColor = Theme.Accent;
        this.lblIcon.Dock = DockStyle.Top;
        this.lblIcon.Height = 100;
        this.lblIcon.TextAlign = ContentAlignment.MiddleCenter;
        this.sidebar.Controls.Add(this.lblIcon);

        // Main Content Setup
        this.mainContent.Dock = DockStyle.Fill;
        this.mainContent.Padding = new Padding(40);
        this.mainContent.BackColor = Theme.Background;

        // Title
        this.lblTitle.Text = "ULTIMATE BOOST";
        this.lblTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
        this.lblTitle.AutoSize = true;
        this.lblTitle.Location = new Point(40, 40);
        this.lblTitle.ForeColor = Color.White;
        this.mainContent.Controls.Add(this.lblTitle);

        // Progress Bar
        this.progressBar.Location = new Point(45, 120);
        this.progressBar.Width = 650;
        this.progressBar.Height = 8;
        this.mainContent.Controls.Add(this.progressBar);

        // Boost Button
        this.btnBoost.Text = "APPLY PERFORMANCE CFG";
        this.btnBoost.Location = new Point(45, 150);
        this.btnBoost.Size = new Size(320, 55);
        this.btnBoost.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        this.mainContent.Controls.Add(this.btnBoost);

        // Log Console (Faceit Terminal style)
        this.rtbLog.Location = new Point(45, 230);
        this.rtbLog.Size = new Size(650, 320);
        this.rtbLog.BackColor = Color.FromArgb(15, 15, 15);
        this.rtbLog.ForeColor = Theme.TextDim;
        this.rtbLog.BorderStyle = BorderStyle.None;
        this.rtbLog.Font = new Font("Consolas", 10F);
        this.rtbLog.ReadOnly = true;
        this.rtbLog.Padding = new Padding(10);
        this.mainContent.Controls.Add(this.rtbLog);

        // CRITICAL: Add mainContent FIRST, then sidebar for correct docking order
        this.Controls.Add(this.mainContent);
        this.Controls.Add(this.sidebar);
    }

    private Panel sidebar;
    private Panel mainContent;
    private Label lblTitle;
    private Label lblIcon;
    private ModernButton btnBoost;
    private ModernProgressBar progressBar;
    private RichTextBox rtbLog;

    #endregion
}
