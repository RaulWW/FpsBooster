using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FpsBooster.Views
{
    public static class Theme
    {
        public static Color Background = Color.FromArgb(12, 12, 18);     // Deep Dark Blue-Black
        public static Color Sidebar = Color.FromArgb(18, 18, 25);        // Rich Dark Sidebar
        public static Color Surface = Color.FromArgb(25, 25, 35);        // Elevated Surface
        public static Color Accent = Color.FromArgb(255, 85, 0);         // Vibrant Electric Orange
        public static Color AccentHover = Color.FromArgb(255, 120, 40);  // Bright Orange Hover
        public static Color AccentGlow = Color.FromArgb(255, 60, 0);     // Deep Orange Glow
        public static Color Text = Color.FromArgb(250, 250, 255);        // Pure White with slight blue
        public static Color TextDim = Color.FromArgb(160, 165, 180);     // Cool Gray
        public static Color Success = Color.FromArgb(0, 255, 100);       // Neon Green

        // Segoe MDL2 Assets Icons
        public static string IconPower = "\uE7E8"; // Lightning
        public static string IconLog = "\uE756";   // Console
        public static string IconSettings = "\uE713"; 
        public static string IconCheck = "\uE73E";
        public static string IconRocket = "\uE99A";
        public static string IconFlash = "\uE945";
        public static string IconGame = "\uE7BE"; // Game Controller
        
        public static string AppVersion = "v1.2.0 ELITE";
        public static string Developer = "⚡ Developed by Raul W. | Premium Edition";
    }

    public class ModernButton : Button
    {
        public ModernButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Theme.Accent;
            ForeColor = Color.White;
            Font = new Font("Segoe UI Semibold", 10F);
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            // ModernButton doesn't need IsActive logic for now, just stylized
        }
    }

    public class MenuButton : Button
    {
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool IsActive { get; set; }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string Icon { get; set; } = "";

        public MenuButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Color.Transparent;
            ForeColor = Theme.TextDim;
            Font = new Font("Segoe UI Semibold", 10F);
            Height = 50;
            Cursor = Cursors.Hand;
            TextAlign = ContentAlignment.MiddleLeft;
            Padding = new Padding(50, 0, 0, 0); // Leave space for icon
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            
            if (IsActive)
            {
                using (var brush = new SolidBrush(Theme.Accent))
                {
                    pevent.Graphics.FillRectangle(brush, 0, 10, 4, Height - 20);
                }
                ForeColor = Theme.Text;
            }
            else
            {
                ForeColor = Theme.TextDim;
            }

            if (!string.IsNullOrEmpty(Icon))
            {
                using (var font = new Font("Segoe MDL2 Assets", 14F))
                using (var brush = new SolidBrush(ForeColor))
                {
                    // Draw icon
                    pevent.Graphics.DrawString(Icon, font, brush, 15, (Height - 18) / 2);
                }
            }
        }
    }

    public class ModernProgressBar : Control
    {
        private int _value = 0;
        
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int Value
        {
            get => _value;
            set { _value = Math.Min(100, Math.Max(0, value)); Invalidate(); }
        }

        public ModernProgressBar()
        {
            DoubleBuffered = true;
            Height = 10;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Theme.Sidebar);
            int width = (Width * _value) / 100;
            if (width > 0)
            {
                using (var brush = new SolidBrush(Theme.Accent))
                {
                    e.Graphics.FillRectangle(brush, 0, 0, width, Height);
                }
            }
        }
    }

    public class ModernLabel : Label
    {
        public ModernLabel()
        {
            ForeColor = Theme.Text;
            Font = new Font("Segoe UI", 10F);
        }
    }
}
