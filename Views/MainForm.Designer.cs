using FpsBooster.Views.Controls;
using FpsBooster.Views.Factories;

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


        
        // Panels for views




        this.lblLinkFooter = new LinkLabel();
        

        
        // Boost tab controls

        
        // CS2 tab controls

        
        // Network tab controls

        



        // Downloads Tab

        
        // Form Configuration
        this.FormBorderStyle = FormBorderStyle.None;
        this.BackColor = Theme.Background;
        this.ForeColor = Theme.Text;
        this.Size = new Size(1100, 700);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "⚡ ULTRA FPS BOOSTER | Gaming Performance Suite";
        this.AutoScaleMode = AutoScaleMode.Dpi;
  
        // Title Bar Setup
        this.panelTitleBar = UIBuilder.CreatePanel(DockStyle.Top, Theme.Sidebar);
        this.panelTitleBar.Height = 40;

        this.btnClose = UIBuilder.CreateIconLabel(Theme.IconClose, 10, Color.White, DockStyle.Right);
        this.btnClose.Size = new Size(45, 40);
        this.btnClose.Cursor = Cursors.Hand;
        this.btnClose.MouseEnter += (s, e) => btnClose.BackColor = Color.Red;
        this.btnClose.MouseLeave += (s, e) => btnClose.BackColor = Color.Transparent;

        this.btnMinimize = UIBuilder.CreateIconLabel(Theme.IconMinimize, 10, Color.White, DockStyle.Right);
        this.btnMinimize.Size = new Size(45, 40);
        this.btnMinimize.Cursor = Cursors.Hand;
        this.btnMinimize.MouseEnter += (s, e) => btnMinimize.BackColor = Color.FromArgb(60, 60, 60);
        this.btnMinimize.MouseLeave += (s, e) => btnMinimize.BackColor = Color.Transparent;

        this.lblAppTitle = UIBuilder.CreateLabel("  ⚡ ULTRA FPS BOOSTER", new Font(Theme.MainFont, 9F, FontStyle.Bold), Theme.TextDim, null, true, ContentAlignment.MiddleLeft);
        this.lblAppTitle.Dock = DockStyle.Left;
        this.lblAppTitle.Padding = new Padding(30, 15, 0, 0);

        this.panelTitleBar.Controls.Add(this.lblAppTitle);
        this.panelTitleBar.Controls.Add(this.btnMinimize);
        this.panelTitleBar.Controls.Add(this.btnClose);

        this.Controls.Add(this.panelTitleBar);

        // Sidebar Setup
        this.sidebar = UIBuilder.CreatePanel(DockStyle.Left, Theme.Sidebar);
        this.sidebar.Width = 240;
        
        // Main Content Setup
        this.mainContent = UIBuilder.CreatePanel(DockStyle.Fill, Theme.Background);
        
        // Sidebar Content
        this.btnMenuBoost = UIBuilder.CreateMenuButton("   ULTIMATE BOOST", Theme.IconRocket);
        this.btnMenuBoost.IsActive = true;

        this.sidebarLogo = new PictureBox
        {
            SizeMode = PictureBoxSizeMode.Zoom,
            Height = 80,
            Dock = DockStyle.Top,
            Padding = new Padding(20, 30, 20, 10)
        };
        try { this.sidebarLogo.Image = Image.FromFile(@"imgs\IcoLogo512px.ico"); } catch { }

        this.btnMenuDocs = UIBuilder.CreateMenuButton("   DOCUMENTAÇÃO", Theme.IconDocs);
        this.btnMenuCS2 = UIBuilder.CreateMenuButton("   CONFIG CS2", Theme.IconGame);
        this.btnMenuNetwork = UIBuilder.CreateMenuButton("   REDE / DIAG.", Theme.IconNetwork);

        this.btnMenuDownloads = UIBuilder.CreateMenuButton("   DOWNLOADS", Theme.IconSettings);
        try { this.btnMenuDownloads.Icon = ""; } catch {}

        this.sidebar.Controls.Add(this.btnMenuNetwork);
        this.sidebar.Controls.Add(this.btnMenuCS2);
        this.sidebar.Controls.Add(this.btnMenuDocs);
        this.sidebar.Controls.Add(this.btnMenuDownloads);
        this.sidebar.Controls.Add(this.btnMenuBoost);
        this.sidebar.Controls.Add(this.sidebarLogo);
        this.sidebar.Controls.Add(this.footer);

        // Footer Setup
        this.footer = UIBuilder.CreatePanel(DockStyle.Bottom);
        this.footer.Height = 40;
        this.footer.Controls.Add(this.lblLinkFooter);
        
        this.lblLinkFooter = new LinkLabel();
        this.lblLinkFooter.Dock = DockStyle.Fill;
        this.lblLinkFooter.ForeColor = Theme.TextDim;
        this.lblLinkFooter.LinkColor = Theme.TextDim;
        this.lblLinkFooter.ActiveLinkColor = Theme.Accent;
        this.lblLinkFooter.VisitedLinkColor = Theme.TextDim;
        this.lblLinkFooter.LinkBehavior = LinkBehavior.HoverUnderline;
        this.lblLinkFooter.Font = new Font("Segoe UI", 8F);
        this.lblLinkFooter.Text = $"{Theme.Developer} | {Theme.AppVersion}";
        this.lblLinkFooter.TextAlign = ContentAlignment.MiddleCenter;
        this.lblLinkFooter.LinkClicked += (s, e) => {
            try {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo {
                    FileName = "https://github.com/RaulWW",
                    UseShellExecute = true
                });
            } catch { }
        };

        // Main Content Setup
        this.mainContent.Dock = DockStyle.Fill;
        this.mainContent.BackColor = Theme.Background;

        // Panel Boost Setup
        this.panelBoost = UIBuilder.CreatePanel(DockStyle.Fill, null, new Padding(40));
        
        this.lblTitle = UIBuilder.CreateLabel("ULTIMATE BOOST", new Font("Segoe UI", 22F, FontStyle.Bold), Color.White, new Point(40, 40));
        this.lblTitle.Padding = new Padding(0, 15, 0, 0);
        
        this.progressBar = UIBuilder.CreateProgressBar(new Point(45, 120), 720);
        
        this.btnBoost = UIBuilder.CreateButton("APPLY PERFORMANCE CFG", new Point(45, 150), new Size(280, 45));
        
        this.rtbLog = UIBuilder.CreateRichTextBox(new Point(45, 230), new Size(720, 360));
        
        this.panelBoost.Controls.Add(this.rtbLog);
        this.panelBoost.Controls.Add(this.btnBoost);
        this.panelBoost.Controls.Add(this.progressBar);
        this.panelBoost.Controls.Add(this.lblTitle);

        // Panel CS2 Setup
        this.panelCS2 = UIBuilder.CreatePanel(DockStyle.Fill, null, new Padding(40), false);

        this.lblCS2Title = UIBuilder.CreateLabel("CONFIG CS2", new Font("Segoe UI", 22F, FontStyle.Bold), Color.White, new Point(40, 40));
        this.lblCS2Title.Padding = new Padding(0, 15, 0, 0);

        this.lblCS2Info = UIBuilder.CreateLabel("Edit your autoexec.cfg below. Syntax highlighting applies to commands.", new Font("Segoe UI", 10F), Theme.TextDim, new Point(45, 100), true);

        this.rtbCS2Config = UIBuilder.CreateRichTextBox(new Point(45, 130), new Size(720, 400), null, Theme.Text, new Font("Consolas", 11F), false);
        this.rtbCS2Config.AcceptsTab = true;

        this.btnSaveCS2 = UIBuilder.CreateButton("  SAVE AUTOEXEC.CFG", new Point(45, 550), new Size(220, 40));
        
        this.panelCS2.Controls.Add(this.btnSaveCS2);
        this.panelCS2.Controls.Add(this.rtbCS2Config);
        this.panelCS2.Controls.Add(this.lblCS2Info);
        this.panelCS2.Controls.Add(this.lblCS2Title);

        // Panel Network Setup
        this.panelNetwork = UIBuilder.CreatePanel(DockStyle.Fill, null, null, false);

        // Panel Network Control
        this.lblNetworkTitle = UIBuilder.CreateLabel("NETWORK DIAGNOSTICS", new Font("Segoe UI", 22F, FontStyle.Bold), Color.White, new Point(40, 40));
        this.lblNetworkTitle.Padding = new Padding(0, 15, 0, 0);

        this.lblNetworkInfo = UIBuilder.CreateLabel("Enter an IP or Hostname to test your connection quality.", new Font("Segoe UI", 10F), Theme.TextDim, new Point(45, 75));

        this.txtTargetIp = UIBuilder.CreateTextBox("8.8.8.8", new Point(45, 110), new Size(300, 28));

        this.btnStartNetworkTest = UIBuilder.CreateButton("  START TEST", new Point(360, 110), new Size(160, 28));
        this.btnStartNetworkTest.Font = new Font("Segoe UI Semibold", 9F);

        this.btnLoadFaceit = UIBuilder.CreateButton("  FACEIT", new Point(530, 110), new Size(110, 28));
        this.btnLoadFaceit.BackColor = Color.Black; 
        this.btnLoadFaceit.ForeColor = Color.FromArgb(255, 85, 0); // Orange text
        this.btnLoadFaceit.Font = new Font("Segoe UI Semibold", 9F);
        try { this.btnLoadFaceit.ButtonIcon = Image.FromFile(@"imgs\faceit_logo.png"); } catch { }

        this.btnLoadGC = UIBuilder.CreateButton("  GC IP", new Point(650, 110), new Size(110, 28));
        this.btnLoadGC.BackColor = Theme.BgCard;
        this.btnLoadGC.Font = new Font("Segoe UI Semibold", 9F);
        try { this.btnLoadGC.ButtonIcon = Image.FromFile(@"imgs\GC.png"); } catch { }

        this.lblPingResult = UIBuilder.CreateLabel("PING: -- ms", new Font("Segoe UI Semibold", 11F), Theme.AccentGreen, new Point(45, 160));

        this.lblJitterResult = UIBuilder.CreateLabel("JITTER: -- ms", new Font("Segoe UI Semibold", 11F), Theme.AccentBlue, new Point(220, 160));

        this.lblLossResult = UIBuilder.CreateLabel("LOSS: -- %", new Font("Segoe UI Semibold", 11F), Theme.AccentAmber, new Point(400, 160));

        this.rtbNetworkLog = UIBuilder.CreateRichTextBox(new Point(45, 200), new Size(720, 330), Theme.BgCard, Theme.TextDim, new Font("Consolas", 9F));

        this.panelNetwork.Controls.Clear();
        this.panelNetwork.Controls.Add(this.lblNetworkTitle);
        this.panelNetwork.Controls.Add(this.lblNetworkInfo);
        this.panelNetwork.Controls.Add(this.txtTargetIp);
        this.panelNetwork.Controls.Add(this.btnStartNetworkTest);
        this.panelNetwork.Controls.Add(this.btnLoadFaceit);
        this.panelNetwork.Controls.Add(this.btnLoadGC);
        this.panelNetwork.Controls.Add(this.lblPingResult);
        this.panelNetwork.Controls.Add(this.lblJitterResult);
        this.panelNetwork.Controls.Add(this.lblLossResult);
        this.panelNetwork.Controls.Add(this.rtbNetworkLog);
        // Panel Docs Setup
        this.panelDocs = UIBuilder.CreatePanel(DockStyle.Fill, null, new Padding(40), false);
        
        this.lblDocsTitle = UIBuilder.CreateLabel("DOCUMENTAÇÃO", new Font(Theme.MainFont, 22F, FontStyle.Bold), Color.White, new Point(40, 40));
        this.lblDocsTitle.Padding = new Padding(0, 15, 0, 0);

        var docsText = @"🚀 ULTRA FPS BOOSTER - DOCUMENTAÇÃO TÉCNICA COMPLETA

═══════════════════════════════════════════════════════════════
⚡ MODO: ULTIMATE BOOST (Aba Principal)
═══════════════════════════════════════════════════════════════

O botão 'APPLY PERFORMANCE CFG' executa uma série de otimizações automáticas. Veja o que acontece:

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
1️⃣ PLANO DE ENERGIA: DESEMPENHO MÁXIMO
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
   O que faz:
   - Ativa o 'Ultimate Performance Power Plan' oculto do Windows
   - Se não existir, duplica e ativa automaticamente
   
   Benefícios Técnicos:
   • Desativa C-States (estados de economia de energia da CPU)
   • Elimina Core Parking (núcleos inativos são mantidos sempre ativos)
   • Remove throttling de CPU em jogos (clock sempre no máximo)
   • Reduz micro-stutters causados por mudanças de frequência

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
2️⃣ LIMPEZA PROFUNDA DO SISTEMA
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
   O que faz:
   - Limpa TODOS os arquivos temporários (%TEMP%, C:\Windows\Temp)
   - Remove Prefetch (índices de inicialização rápida obsoletos)
   - Limpa Event Logs e Logs do Windows Defender
   - Remove cache de browsers, Discord, Spotify, Steam, VS Code
   - Esvazia Delivery Optimization Cache da Microsoft
   - Limpa thumbnails e ícones cacheados do Explorer
   
   Benefícios:
   • Libera GB de espaço no SSD/HDD
   • Reduz tempo de busca em diretórios superlotados (menos I/O)
   • Melhora responsividade do sistema
   • Reduz stuttering causado por buscas em disco

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
3️⃣ OTIMIZAÇÕES DE REGISTRO & BCD
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
   O que faz:
   - Desativa 'Fullscreen Optimizations' (GameDVR fix)
   - Remove objetos 3D do Windows Explorer (menos overhead de UI)
   - Ativa 'End Task' direto do taskbar (facilita fechar apps travados)
   - Configura bcdedit bootmenupolicy Legacy (mais controle avançado)
   
   Benefícios:
   • Elimina o DVR do Xbox Game Bar (reduz latência de renderização)
   • Melhora compatibilidade com jogos antigos e modernos
   • Acesso mais rápido a controles avançados do sistema

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
4️⃣ DESATIVAÇÃO AGRESSIVA DE TELEMETRIA
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
   O que faz:
   - Define AllowTelemetry = 0 (desativa coleta de dados da Microsoft)
   - Desativa publicação de atividades do usuário (Timeline)
   - Remove conexões automáticas ao Windows Update
   - Desativa tarefas agendadas de coleta de dados (11+ tasks)
   - Bloqueia CloudContent e AdvertisingInfo
   
   Tasks Desativadas:
   • Microsoft Compatibility Appraiser, Consolidator, UsbCeip
   • DiskDiagnostic, Siuf/DmClient, QueueReporting, MapsUpdate
   
   Benefícios:
   • Reduz uso de CPU em background (menos threads)
   • Libera largura de banda da rede (sem uploads de logs)
   • Aumenta privacidade e controle sobre o sistema

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
5️⃣ OTIMIZAÇÃO DE SERVIÇOS & REDE
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
   O que faz:
   - Ajusta SvcHostSplitThreshold (separa serviços em processos próprios)
   - Desativa SysMain (Superfetch/Prefetch) e WSearch (indexação)
   - Desativa Teredo (IPv6 Tunneling desnecessário em jogos)
   - Bloqueia AutoLogger de diagnóstico da Microsoft
   - Define Windows Defender para não enviar amostras
   
   Benefícios:
   • Evita picos de CPU causados por svchost.exe compartilhado
   • Elimina lags causados por indexação de arquivos
   • Melhora estabilidade de conexão (Teredo causa packet loss)
   • Reduz overhead de rede em jogos competitivos

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
6️⃣ BLOQUEIO DE PROCESSOS ADOBE EM SEGUNDO PLANO
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
   O que faz:
   - Encerra Adobe Genuine Service (verificação de licenças)
   - Bloqueia IPs da Adobe no arquivo HOSTS
   
   Benefícios:
   • Reduz uso de CPU e RAM por processos desnecessários
   • Libera largura de banda de rede

═══════════════════════════════════════════════════════════════
🎮 CONFIG CS2 (Aba CONFIG CS2)
═══════════════════════════════════════════════════════════════

Editor integrado com syntax highlighting para editar seu autoexec.cfg.
   - Salva direto em: C:\Program Files (x86)\Steam\steamapps\
     common\Counter-Strike Global Offensive\game\csgo\cfg\
   - Use launch options: +exec autoexec.cfg -refresh 240

═══════════════════════════════════════════════════════════════
🌐 NETWORK DIAGNOSTICS (Aba REDE / DIAG.)
═══════════════════════════════════════════════════════════════

Teste em tempo real de qualidade de conexão:
   - Ping, Jitter e Packet Loss para qualquer IP/hostname
   - Presets pré-configurados: Faceit e Gamers Club
   - Atualização a cada 2s com cores dinâmicas:
     • Verde: qualidade excelente
     • Amarelo: qualidade média
     • Vermelho: problemas sérios de rede

═══════════════════════════════════════════════════════════════
📥 DOWNLOADS (Aba DOWNLOADS)
═══════════════════════════════════════════════════════════════

Instaladores de dependências essenciais para jogos:
   - .NET Framework (2.0, 3.0, 3.5, 4.x): necessário para muitos jogos
   - Visual C++ Redistributables All-in-One: pacote completo de runtimes
   - Log em tempo real (opcional): veja o progresso da instalação

═══════════════════════════════════════════════════════════════
✅ VERIFICAÇÃO DAS OTIMIZAÇÕES
═══════════════════════════════════════════════════════════════

Para confirmar que as otimizações foram aplicadas:

1. Verifique o Plano de Energia:
   powercfg /list
   → Deve mostrar 'Ultimate Performance' como ativo (*)

2. Verifique Telemetria:
   → Abra Registry Editor e vá em:
     HKLM\SOFTWARE\Policies\Microsoft\Windows\DataCollection
     → AllowTelemetry deve ser 0

3. Verifique Teredo:
   netsh interface teredo show state
   → Status deve ser 'disabled'

4. Verifique GameDVR:
   → Registry: HKCU\System\GameConfigStore
     → GameDVR_DXGIHonorFSEWindowsCompatible = 1

5. Verifique Serviços Desativados:
   → Task Manager → Services
     → SysMain e WSearch devem estar Stopped

═══════════════════════════════════════════════════════════════
⚠️ AVISOS IMPORTANTES
═══════════════════════════════════════════════════════════════

• Execute o app COMO ADMINISTRADOR para garantir permissões
• Algumas mudanças requerem reinicialização do Windows
• Em caso de problemas, crie um ponto de restauração antes
• Estas otimizações são seguras mas agressivas (foco em PERFORMANCE)

═══════════════════════════════════════════════════════════════
📚 TECNOLOGIAS UTILIZADAS
═══════════════════════════════════════════════════════════════

• C# / .NET 10.0 (última geração)
• WinForms customizado (controles premium)
• PowerShell Engine integrado (otimizações nativas do Windows)
• RichTextBox com syntax highlighting (editor de código)

Desenvolvido com ⚡ por Raul W. | github.com/RaulWW
";

        this.rtbDocsContent = UIBuilder.CreateRichTextBox(new Point(45, 120), new Size(720, 450), Theme.Background, Theme.Text, new Font(Theme.MainFont, 11F));
        this.rtbDocsContent.Text = docsText;

        this.panelDocs.Controls.Add(this.rtbDocsContent);
        this.panelDocs.Controls.Add(this.lblDocsTitle);

        // Panel Downloads Setup
        this.panelDownloads = UIBuilder.CreatePanel(DockStyle.Fill, null, new Padding(40), false);
        
        this.lblDownloadsTitle = UIBuilder.CreateLabel("DOWNLOADS & FEATURES", new Font(Theme.MainFont, 22F, FontStyle.Bold), Color.White, new Point(40, 40));
        this.lblDownloadsTitle.Padding = new Padding(0, 15, 0, 0);

        this.lblDownloadsInfo = UIBuilder.CreateLabel("Install additional Windows features and runtimes.", null, Theme.TextDim, new Point(45, 100));

        // Options Container
        var optionsPanel = UIBuilder.CreateFlowLayoutPanel(DockStyle.Top, FlowDirection.TopDown, false, true);
        optionsPanel.Padding = new Padding(45, 140, 0, 0);
        
        this.chkDotNet = UIBuilder.CreateCheckbox(" .NET Framework (2.0, 3.0, 3.5, 4.x)", null);
        this.chkVisualCpp = UIBuilder.CreateCheckbox(" Visual C++ Redistributables (All-in-One)", null);
        this.chkLog = UIBuilder.CreateCheckbox(" Show Installation Log", null);
        
        optionsPanel.Controls.Add(this.chkDotNet);
        optionsPanel.Controls.Add(this.chkVisualCpp);
        optionsPanel.Controls.Add(this.chkLog);

        // Center Buttons using FlowLayoutPanel
        this.downloadButtonsPanel = UIBuilder.CreateFlowLayoutPanel(DockStyle.Top);
        this.downloadButtonsPanel.Padding = new Padding(0, 20, 0, 0);
        this.downloadButtonsPanel.Height = 80;

        // Container to help centering
        var centerContainer = UIBuilder.CreateFlowLayoutPanel(DockStyle.Fill, FlowDirection.LeftToRight, false, true);
        centerContainer.WrapContents = false;
        centerContainer.Height = 45;
        
        this.btnInstallFeatures = UIBuilder.CreateButton("  INSTALL SELECTED", new Point(0, 0), new Size(220, 40));
        this.btnInstallVisualCpp = UIBuilder.CreateButton("  INSTALL VISUAL C++", new Point(0, 0), new Size(220, 40));
        
        centerContainer.Controls.Add(this.btnInstallFeatures);
        centerContainer.Controls.Add(this.btnInstallVisualCpp);
        
        // Manual centering hack for WinForms Flow
        this.downloadButtonsPanel.Controls.Add(new Panel { Width = 45, Height = 1, BackColor = Color.Transparent }); // Left margin
        this.downloadButtonsPanel.Controls.Add(centerContainer);

        this.rtbDownloadsLog = UIBuilder.CreateRichTextBox(new Point(45, 420), new Size(720, 200), Theme.BgCard, Theme.TextDim, new Font("Consolas", 9F));
        this.rtbDownloadsLog.Visible = false;

        this.panelDownloads.Controls.Add(this.rtbDownloadsLog);
        this.panelDownloads.Controls.Add(this.downloadButtonsPanel);
        this.panelDownloads.Controls.Add(optionsPanel);
        this.panelDownloads.Controls.Add(this.lblDownloadsInfo);
        this.panelDownloads.Controls.Add(this.lblDownloadsTitle);

        this.mainContent.Controls.Add(this.panelBoost);
        this.mainContent.Controls.Add(this.panelCS2);
        this.mainContent.Controls.Add(this.panelNetwork);
        this.mainContent.Controls.Add(this.panelDownloads);
        this.mainContent.Controls.Add(this.panelDocs);
        
        this.Controls.Add(this.mainContent);
        this.Controls.Add(this.sidebar);
    }

    private Panel sidebar;
    private Panel footer;
    private LinkLabel lblLinkFooter;
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
    private ModernButton btnLoadGC;

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
    private RichTextBox rtbDocsContent;

    // Downloads/Custom Controls
    private Panel panelDownloads;
    private MenuButton btnMenuDownloads;
    private Label lblDownloadsTitle;
    private Label lblDownloadsInfo;
    private CheckBox chkDotNet;
    private CheckBox chkVisualCpp;
    private CheckBox chkLog;
    private FlowLayoutPanel downloadButtonsPanel;
    private ModernButton btnInstallFeatures;
    private ModernButton btnInstallVisualCpp;
    private RichTextBox rtbDownloadsLog;
    private PictureBox sidebarLogo;


    #endregion
}
