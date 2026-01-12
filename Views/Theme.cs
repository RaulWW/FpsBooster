using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FpsBooster.Views
{
    public static class Theme
    {
        public static Color Background = Color.FromArgb(20, 20, 20); // Deep Black/Gray
        public static Color Sidebar = Color.FromArgb(30, 30, 30);    // Slightly Lighter Gray
        public static Color Surface = Color.FromArgb(40, 40, 40);
        public static Color Accent = Color.FromArgb(255, 102, 0);    // Faceit Orange
        public static Color AccentHover = Color.FromArgb(255, 133, 51);
        public static Color Text = Color.FromArgb(255, 255, 255);
        public static Color TextDim = Color.FromArgb(180, 180, 180);
        public static Color Success = Color.FromArgb(0, 255, 100);

        // Segoe MDL2 Assets Icons
        public static string IconPower = "\uE7E8"; // Lightning
        public static string IconLog = "\uE756";   // Console
        public static string IconSettings = "\uE713"; 
        public static string IconCheck = "\uE73E";
        public static string IconRocket = "\uE99A";
        public static string IconFlash = "\uE945";
        public static string IconGame = "\uE7BE"; // Game Controller
        
        public static string AppVersion = "v1.0.0";
        public static string Developer = "developed by Raul W. minimalist";
    }

    public class ModernButton : Button
    {
        public ModernButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Theme.Accent;
            ForeColor = Color.White;
            Cursor = Cursors.Hand;
            Font = new Font("Segoe UI Semibold", 9.5F);
        }
    }

    public class MenuButton : Button
    {
        public bool IsActive { get; set; } = false;
        public string Icon { get; set; } = "";

        public MenuButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Color.Transparent;
            ForeColor = Theme.TextDim;
            Cursor = Cursors.Hand;
            Font = new Font("Segoe UI Semibold", 10F);
            TextAlign = ContentAlignment.MiddleLeft;
            ImageAlign = ContentAlignment.MiddleLeft;
            Height = 42;
            Dock = DockStyle.Top;
            Padding = new Padding(20, 0, 0, 0);
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
