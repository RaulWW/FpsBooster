using FpsBooster.Views.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace FpsBooster.Views.Factories
{
    public static class UIBuilder
    {
        // Panels
        public static Panel CreatePanel(DockStyle dock = DockStyle.None, Color? backColor = null, Padding? padding = null, bool visible = true)
        {
            return new BufferedPanel
            {
                Dock = dock,
                BackColor = backColor ?? Color.Transparent,
                Padding = padding ?? Padding.Empty,
                Visible = visible
            };
        }

        // Helper class for flickering
        private class BufferedPanel : Panel
        {
            public BufferedPanel()
            {
                this.DoubleBuffered = true;
                this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
                this.UpdateStyles();
            }
        }

        // Labels
        public static Label CreateLabel(string text, Font font, Color color, Point? location = null, bool autoSize = true, ContentAlignment? textAlign = null)
        {
            var lbl = new Label
            {
                Text = text,
                Font = font,
                ForeColor = color,
                AutoSize = autoSize
            };
            
            if (location.HasValue) lbl.Location = location.Value;
            if (textAlign.HasValue) lbl.TextAlign = textAlign.Value;

            return lbl;
        }

        public static Label CreateIconLabel(string iconCode, int size, Color color, DockStyle dock = DockStyle.None)
        {
            return new Label
            {
                Text = iconCode,
                Font = new Font("Segoe MDL2 Assets", size),
                ForeColor = color,
                Dock = dock,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false
            };
        }

        // Buttons
        public static ModernButton CreateButton(string text, Point? location = null, Size? size = null, EventHandler? onClick = null)
        {
            var btn = new ModernButton
            {
                Text = text,
                Font = new Font("Segoe UI Semibold", 10F)
            };

            if (location.HasValue) btn.Location = location.Value;
            if (size.HasValue) btn.Size = size.Value;
            if (onClick != null) btn.Click += onClick;

            return btn;
        }

        public static MenuButton CreateMenuButton(string text, string icon, DockStyle dock = DockStyle.Top, EventHandler? onClick = null)
        {
            var btn = new MenuButton
            {
                Text = text,
                Icon = icon,
                Dock = dock
            };

            if (onClick != null) btn.Click += onClick;
            return btn;
        }

        // Controls
        public static CheckBox CreateCheckbox(string text, Point? location = null, Font? font = null, Color? color = null)
        {
            var chk = new CheckBox
            {
                Text = text,
                Font = font ?? new Font("Segoe UI", 11F),
                ForeColor = color ?? Theme.Text,
                AutoSize = true,
                Cursor = Cursors.Hand
            };

            if (location.HasValue) chk.Location = location.Value;
            return chk;
        }

        public static RichTextBox CreateRichTextBox(Point? location = null, Size? size = null, Color? backColor = null, Color? foreColor = null, Font? font = null, bool readOnly = true)
        {
            var rtb = new RichTextBox
            {
                BackColor = backColor ?? Color.FromArgb(15, 15, 15),
                ForeColor = foreColor ?? Theme.TextDim,
                BorderStyle = BorderStyle.None,
                Font = font ?? new Font("Consolas", 10F),
                ReadOnly = readOnly
            };

            if (location.HasValue) rtb.Location = location.Value;
            if (size.HasValue) rtb.Size = size.Value;

            return rtb;
        }

        public static TextBox CreateTextBox(string text, Point? location = null, Size? size = null)
        {
            var txt = new TextBox
            {
                Text = text,
                BackColor = Theme.BgCard,
                ForeColor = Theme.Text,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10F)
            };

            if (location.HasValue) txt.Location = location.Value;
            if (size.HasValue) txt.Size = size.Value;

            return txt;
        }

        public static ModernProgressBar CreateProgressBar(Point location, int width)
        {
            return new ModernProgressBar
            {
                Location = location,
                Width = width,
                Height = 8
            };
        }

        public static FlowLayoutPanel CreateFlowLayoutPanel(DockStyle dock = DockStyle.None, FlowDirection direction = FlowDirection.LeftToRight, bool wrapContents = true, bool autoSize = true)
        {
            return new FlowLayoutPanel
            {
                Dock = dock,
                FlowDirection = direction,
                WrapContents = wrapContents,
                AutoSize = autoSize,
                BackColor = Color.Transparent
            };
        }

        public static Panel CreateContainer(DockStyle dock = DockStyle.None, Padding? padding = null, bool autoSize = false)
        {
            return new BufferedPanel
            {
                Dock = dock,
                Padding = padding ?? Padding.Empty,
                AutoSize = autoSize,
                BackColor = Color.Transparent
            };
        }
    }
}
