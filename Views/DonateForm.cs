using System.Runtime.InteropServices;

namespace FpsBooster.Views;

/// <summary>
/// Minimalist form to display PIX QR Code for donations
/// </summary>
public class DonateForm : Form
{
    private PictureBox? pictureBox;
    private Panel? panelTitleBar;
    private Panel? panelBottom;
    private Button? btnClose;
    private Label? lblTitle;
    private Label? lblInstruction;

    public DonateForm()
    {
        InitializeComponent();
        InitializeCustomTitleBar();
        LoadPixQrCode();
    }

    private void InitializeComponent()
    {
        this.Text = "Apoiar o Projeto";
        this.Size = new Size(400, 540);
        this.FormBorderStyle = FormBorderStyle.None;
        this.StartPosition = FormStartPosition.CenterParent;
        this.BackColor = Theme.BgDark;
        this.DoubleBuffered = true;

        // Custom Title Bar
        panelTitleBar = new Panel
        {
            Dock = DockStyle.Top,
            Height = 40,
            BackColor = Theme.BgCard
        };

        lblTitle = new Label
        {
            Text = "❤ Apoiar o Projeto",
            ForeColor = Theme.TextPrimary,
            Font = new Font("Segoe UI", 11F, FontStyle.Bold),
            AutoSize = false,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(15, 0, 0, 0)
        };

        btnClose = new Button
        {
            Text = "✕",
            Size = new Size(40, 40),
            Dock = DockStyle.Right,
            FlatStyle = FlatStyle.Flat,
            BackColor = Theme.BgCard,
            ForeColor = Theme.TextPrimary,
            Font = new Font("Segoe UI", 12F),
            Cursor = Cursors.Hand,
            TabStop = false
        };
        btnClose.FlatAppearance.BorderSize = 0;
        btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 50, 50);

        panelTitleBar.Controls.Add(lblTitle);
        panelTitleBar.Controls.Add(btnClose);

        // Bottom Panel with Blue Background
        panelBottom = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 60,
            BackColor = Theme.AccentBlue
        };

        lblInstruction = new Label
        {
            Text = "Escaneie o QRCode PIX para doar ❤",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 11F, FontStyle.Bold),
            AutoSize = false,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };

        panelBottom.Controls.Add(lblInstruction);

        // Main Content Panel (Dark) - Controls the overall size/margin of the QR code block
        var qrContainer = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Theme.BgDark,
            Padding = new Padding(60) // Increase this to make the QR code smaller
        };

        // White Card Panel - Provides the required White "Quiet Zone" for the QR Code
        var whiteCard = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            Padding = new Padding(20) // The "Escape" / Quiet Zone margin
        };

        // PictureBox for QR Code
        pictureBox = new PictureBox
        {
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.Zoom,
            BackColor = Color.White
        };

        whiteCard.Controls.Add(pictureBox);
        qrContainer.Controls.Add(whiteCard);

        // Add controls
        this.Controls.Add(qrContainer);
        this.Controls.Add(panelBottom);
        this.Controls.Add(panelTitleBar);
    }

    private void InitializeCustomTitleBar()
    {
        if (btnClose != null)
            btnClose.Click += (s, e) => this.Close();

        if (panelTitleBar != null)
            panelTitleBar.MouseDown += TitleBar_MouseDown;
    }

    private void LoadPixQrCode()
    {
        // Try multiple paths to find the QR code
        string[] possiblePaths = {
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "pix_qrcode.png"),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pix_qrcode.png"),
            // Fallback for development (checking parent directories)
            Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.FullName ?? "", "Assets", "pix_qrcode.png")
        };

        try
        {
            string foundPath = "";
            foreach (var path in possiblePaths)
            {
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    foundPath = path;
                    break;
                }
            }

            if (!string.IsNullOrEmpty(foundPath) && pictureBox != null)
            {
                pictureBox.Image = Image.FromFile(foundPath);
            }
            else if (pictureBox != null)
            {
                // Display placeholder message if image not found
                Label lblPlaceholder = new Label
                {
                    Text = "Por favor, adicione a imagem do QR Code PIX em:\n" + string.Join("\nou\n", possiblePaths),
                    ForeColor = Theme.TextPrimary,
                    Font = new Font("Segoe UI", 9F),
                    AutoSize = false,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Padding = new Padding(20),
                    BackColor = Color.Transparent
                };
                pictureBox.Controls.Add(lblPlaceholder);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao carregar QR Code: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // Window Dragging
    [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
    private extern static void ReleaseCapture();
    [DllImport("user32.DLL", EntryPoint = "SendMessage")]
    private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

    private void TitleBar_MouseDown(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
