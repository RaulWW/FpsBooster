using FpsBooster.Views.Controls;

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
        this.panelNetwork = new Panel();
        this.footer = new Panel();
        this.lblFooter = new Label();
        
        // Navigation buttons
        this.btnMenuBoost = new MenuButton();
        this.btnMenuCS2 = new MenuButton();
        this.btnMenuNetwork = new MenuButton();
        
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
        
        // Network tab controls
        this.lblNetworkTitle = new Label();
        this.lblNetworkInfo = new Label();
        this.txtTargetIp = new TextBox();
        this.btnStartNetworkTest = new ModernButton();
        this.btnLoadFaceit = new ModernButton();
        this.lblPingResult = new Label();
        this.lblJitterResult = new Label();
        this.lblLossResult = new Label();
        this.rtbNetworkLog = new RichTextBox();
        
        // Custom Title Bar
        this.panelTitleBar = new Panel();
        this.btnClose = new Label();
        this.btnMinimize = new Label();
        this.lblAppTitle = new Label();

        // Documentation Tab
        this.panelDocs = new Panel();
        this.btnMenuDocs = new MenuButton();
        this.lblDocsTitle = new Label();
        this.lblDocsContent = new Label();
        
        // Form Configuration
        this.FormBorderStyle = FormBorderStyle.None;
        this.BackColor = Theme.Background;
        this.ForeColor = Theme.Text;
        this.Size = new Size(1100, 700);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "⚡ ULTRA FPS BOOSTER | Gaming Performance Suite";
        this.AutoScaleMode = AutoScaleMode.Dpi;

        // Title Bar Setup
        this.panelTitleBar.Dock = DockStyle.Top;
        this.panelTitleBar.Height = 32;
        this.panelTitleBar.BackColor = Theme.Sidebar;

        this.btnClose.Text = Theme.IconClose;
        this.btnClose.Font = new Font("Segoe MDL2 Assets", 10F);
        this.btnClose.ForeColor = Color.White;
        this.btnClose.Size = new Size(45, 32);
        this.btnClose.TextAlign = ContentAlignment.MiddleCenter;
        this.btnClose.Dock = DockStyle.Right;
        this.btnClose.Cursor = Cursors.Hand;
        this.btnClose.MouseEnter += (s, e) => btnClose.BackColor = Color.Red;
        this.btnClose.MouseLeave += (s, e) => btnClose.BackColor = Color.Transparent;

        this.btnMinimize.Text = Theme.IconMinimize;
        this.btnMinimize.Font = new Font("Segoe MDL2 Assets", 10F);
        this.btnMinimize.ForeColor = Color.White;
        this.btnMinimize.Size = new Size(45, 32);
        this.btnMinimize.TextAlign = ContentAlignment.MiddleCenter;
        this.btnMinimize.Dock = DockStyle.Right;
        this.btnMinimize.Cursor = Cursors.Hand;
        this.btnMinimize.MouseEnter += (s, e) => btnMinimize.BackColor = Color.FromArgb(60, 60, 60);
        this.btnMinimize.MouseLeave += (s, e) => btnMinimize.BackColor = Color.Transparent;

        this.lblAppTitle.Text = "  ⚡ ULTRA FPS BOOSTER";
        this.lblAppTitle.Font = new Font(Theme.MainFont, 9F, FontStyle.Bold);
        this.lblAppTitle.ForeColor = Theme.TextDim;
        this.lblAppTitle.Dock = DockStyle.Left;
        this.lblAppTitle.TextAlign = ContentAlignment.MiddleLeft;
        this.lblAppTitle.AutoSize = true;

        this.panelTitleBar.Controls.Add(this.lblAppTitle);
        this.panelTitleBar.Controls.Add(this.btnMinimize);
        this.panelTitleBar.Controls.Add(this.btnClose);

        this.Controls.Add(this.panelTitleBar);

        // Sidebar Setup
        this.sidebar.Dock = DockStyle.Left;
        this.sidebar.Width = 240;
        this.sidebar.BackColor = Theme.Sidebar;
        
        // Sidebar Content
        this.lblIcon.Text = Theme.IconRocket;
        this.lblIcon.Font = new Font("Segoe MDL2 Assets", 32F);
        this.lblIcon.ForeColor = Theme.Accent;
        this.lblIcon.Dock = DockStyle.Top;
        this.lblIcon.Height = 100;
        this.lblIcon.TextAlign = ContentAlignment.MiddleCenter;
        
        this.btnMenuNetwork.Text = "   NETWORK TEST";
        this.btnMenuNetwork.Icon = Theme.IconNetwork;
        this.btnMenuNetwork.Dock = DockStyle.Top;
        
        this.btnMenuCS2.Text = "   CONFIG CS2";
        this.btnMenuCS2.Icon = Theme.IconGame;
        this.btnMenuCS2.Dock = DockStyle.Top;
        
        this.btnMenuBoost.Text = "   ULTIMATE BOOST";
        this.btnMenuBoost.IsActive = true;
        this.btnMenuBoost.Icon = Theme.IconRocket;
        this.btnMenuBoost.Dock = DockStyle.Top;

        this.btnMenuDocs.Text = "   DOCUMENTAÇÃO";
        this.btnMenuDocs.Icon = Theme.IconDocs;
        this.btnMenuDocs.Dock = DockStyle.Top;

        this.sidebar.Controls.Add(this.btnMenuNetwork);
        this.sidebar.Controls.Add(this.btnMenuCS2);
        this.sidebar.Controls.Add(this.btnMenuDocs);
        this.sidebar.Controls.Add(this.btnMenuBoost);
        this.sidebar.Controls.Add(this.lblIcon);
        this.sidebar.Controls.Add(this.footer);

        // Footer Setup
        this.footer.Dock = DockStyle.Bottom;
        this.footer.Height = 40;
        this.footer.Controls.Add(this.lblFooter);
        
        this.lblFooter.Dock = DockStyle.Fill;
        this.lblFooter.ForeColor = Theme.TextDim;
        this.lblFooter.Font = new Font("Segoe UI", 8F);
        this.lblFooter.Text = $"{Theme.Developer} | {Theme.AppVersion}";
        this.lblFooter.TextAlign = ContentAlignment.MiddleCenter;

        // Main Content Setup
        this.mainContent.Dock = DockStyle.Fill;
        this.mainContent.BackColor = Theme.Background;

        // Panel Boost Setup
        this.panelBoost.Dock = DockStyle.Fill;
        this.panelBoost.Padding = new Padding(40);
        
        this.lblTitle.Text = "ULTIMATE BOOST";
        this.lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        this.lblTitle.AutoSize = true;
        this.lblTitle.Location = new Point(40, 40);
        this.lblTitle.ForeColor = Color.White;
        
        this.progressBar.Location = new Point(45, 120);
        this.progressBar.Width = 720;
        this.progressBar.Height = 8;
        
        this.btnBoost.Text = "APPLY PERFORMANCE CFG";
        this.btnBoost.Location = new Point(45, 150);
        this.btnBoost.Size = new Size(280, 45);
        this.btnBoost.Font = new Font("Segoe UI Semibold", 10F);
        
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
        this.lblCS2Title.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
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
        this.btnSaveCS2.Size = new Size(220, 40);
        
        this.panelCS2.Controls.Add(this.btnSaveCS2);
        this.panelCS2.Controls.Add(this.rtbCS2Config);
        this.panelCS2.Controls.Add(this.lblCS2Info);
        this.panelCS2.Controls.Add(this.lblCS2Title);

        // Panel Network Setup
        this.panelNetwork.Dock = DockStyle.Fill;
        this.panelNetwork.Padding = new Padding(40);
        this.panelNetwork.Visible = false;

        this.lblNetworkTitle.Text = "NETWORK DIAGNOSTICS";
        this.lblNetworkTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        this.lblNetworkTitle.AutoSize = true;
        this.lblNetworkTitle.Location = new Point(40, 40);
        this.lblNetworkTitle.ForeColor = Color.White;

        this.lblNetworkInfo.Text = "Enter an IP or Hostname to test your connection quality.";
        this.lblNetworkInfo.ForeColor = Theme.TextDim;
        this.lblNetworkInfo.Location = new Point(45, 100);
        this.lblNetworkInfo.AutoSize = true;

        this.txtTargetIp.Location = new Point(45, 130);
        this.txtTargetIp.Size = new Size(300, 30);
        this.txtTargetIp.BackColor = Color.FromArgb(30, 30, 40);
        this.txtTargetIp.ForeColor = Theme.Text;
        this.txtTargetIp.BorderStyle = BorderStyle.FixedSingle;
        this.txtTargetIp.Text = "8.8.8.8";
        this.txtTargetIp.Font = new Font("Segoe UI", 12F);

        this.btnStartNetworkTest.Text = "START DIAGNOSTICS";
        this.btnStartNetworkTest.Location = new Point(360, 130);
        this.btnStartNetworkTest.Size = new Size(180, 32);

        this.btnLoadFaceit.Text = "FACEIT IP";
        this.btnLoadFaceit.Location = new Point(550, 130);
        this.btnLoadFaceit.Size = new Size(100, 32);
        this.btnLoadFaceit.BackColor = Color.FromArgb(40, 40, 50);

        this.lblPingResult.Text = "PING: -- ms";
        this.lblPingResult.Font = new Font("Segoe UI Semibold", 14F);
        this.lblPingResult.ForeColor = Theme.Accent;
        this.lblPingResult.Location = new Point(45, 180);
        this.lblPingResult.AutoSize = true;

        this.lblJitterResult.Text = "JITTER: -- ms";
        this.lblJitterResult.Font = new Font("Segoe UI Semibold", 14F);
        this.lblJitterResult.ForeColor = Color.Cyan;
        this.lblJitterResult.Location = new Point(220, 180);
        this.lblJitterResult.AutoSize = true;

        this.lblLossResult.Text = "LOSS: -- %";
        this.lblLossResult.Font = new Font("Segoe UI Semibold", 14F);
        this.lblLossResult.ForeColor = Color.Red;
        this.lblLossResult.Location = new Point(400, 180);
        this.lblLossResult.AutoSize = true;

        this.rtbNetworkLog.Location = new Point(45, 230);
        this.rtbNetworkLog.Size = new Size(720, 300);
        this.rtbNetworkLog.BackColor = Color.FromArgb(15, 15, 15);
        this.rtbNetworkLog.ForeColor = Theme.TextDim;
        this.rtbNetworkLog.BorderStyle = BorderStyle.None;
        this.rtbNetworkLog.Font = new Font("Consolas", 10F);
        this.rtbNetworkLog.ReadOnly = true;

        this.panelNetwork.Controls.Add(this.btnLoadFaceit);
        this.panelNetwork.Controls.Add(this.rtbNetworkLog);
        this.panelNetwork.Controls.Add(this.lblLossResult);
        this.panelNetwork.Controls.Add(this.lblJitterResult);
        this.panelNetwork.Controls.Add(this.lblPingResult);
        this.panelNetwork.Controls.Add(this.btnStartNetworkTest);
        this.panelNetwork.Controls.Add(this.txtTargetIp);
        this.panelNetwork.Controls.Add(this.lblNetworkInfo);
        this.panelNetwork.Controls.Add(this.lblNetworkTitle);

        // Panel Docs Setup
        this.panelDocs.Dock = DockStyle.Fill;
        this.panelDocs.Padding = new Padding(40);
        this.panelDocs.Visible = false;

        this.lblDocsTitle.Text = "DOCUMENTAÇÃO";
        this.lblDocsTitle.Font = new Font(Theme.MainFont, 22F, FontStyle.Bold);
        this.lblDocsTitle.AutoSize = true;
        this.lblDocsTitle.Location = new Point(40, 40);
        this.lblDocsTitle.ForeColor = Color.White;

        this.lblDocsContent.Text = @"1 - Ativa power plan maximo, O que é?
> Força o processador e o sistema a trabalharem em Desempenho Máximo, removendo limitações de energia.

2 - Limpeza silent completa, o que é?
> Executa a limpeza de pastas temporárias (Windows Temp, User Temp e Prefetch), liberando espaço e removendo arquivos inúteis.

3 - Ajustes de Registro e BCD, o que é?
> Desativa a política de telemetria, otimiza o bcdedit para menor latência e remove objetos 3D desnecessários do Explorer.

4 - Otimização de Serviços e Memória, o que é?
> Melhora a gestão de processos svchost, desativa telemetria de rede e otimiza o Defender para não consumir recursos em excesso.

5 - Bloqueio Adobe e Telemetria, o que é?
> Bloqueia conexões de telemetria via arquivo HOSTS e interrompe serviços de fundo que consomem CPU desnecessariamente.";

        this.lblDocsContent.Font = new Font(Theme.MainFont, 11F);
        this.lblDocsContent.ForeColor = Theme.TextDim;
        this.lblDocsContent.Location = new Point(45, 120);
        this.lblDocsContent.Size = new Size(720, 450);
        
        this.panelDocs.Controls.Add(this.lblDocsContent);
        this.panelDocs.Controls.Add(this.lblDocsTitle);

        this.mainContent.Controls.Add(this.panelBoost);
        this.mainContent.Controls.Add(this.panelCS2);
        this.mainContent.Controls.Add(this.panelNetwork);
        this.mainContent.Controls.Add(this.panelDocs);
        
        this.Controls.Add(this.mainContent);
        this.Controls.Add(this.sidebar);
    }

    private Panel sidebar;
    private Panel footer;
    private Label lblFooter;
    private Panel mainContent;
    private Panel panelBoost;
    private Panel panelCS2;
    private Panel panelNetwork;
    private MenuButton btnMenuBoost;
    private MenuButton btnMenuCS2;
    private MenuButton btnMenuNetwork;
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
    private ModernButton btnLoadFaceit;

    // Network Controls

    // Network Controls
    private Label lblNetworkTitle;
    private Label lblNetworkInfo;
    private TextBox txtTargetIp;
    private ModernButton btnStartNetworkTest;
    private Label lblPingResult;
    private Label lblJitterResult;
    private Label lblLossResult;
    private RichTextBox rtbNetworkLog;

    // Custom Title Bar Controls
    private Panel panelTitleBar;
    private Label btnClose;
    private Label btnMinimize;
    private Label lblAppTitle;

    // Docs Controls
    private Panel panelDocs;
    private MenuButton btnMenuDocs;
    private Label lblDocsTitle;
    private Label lblDocsContent;

    #endregion
}
