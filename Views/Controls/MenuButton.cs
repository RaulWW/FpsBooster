using System.Drawing;
using System.Windows.Forms;

namespace FpsBooster.Views.Controls
{
    public class MenuButton : Button
    {
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool IsActive { get; set; }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string Icon { get; set; } = "";

        private bool _isHovered;

        public MenuButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Color.Transparent;
            ForeColor = Theme.TextDim;
            Font = new Font(Theme.MainFont, 10F, FontStyle.Bold);
            if (Font.Name != Theme.MainFont) Font = new Font(Theme.FallbackFont, 10F, FontStyle.Bold);
            Height = 50;
            Cursor = Cursors.Hand;
            TextAlign = ContentAlignment.MiddleLeft;
            Padding = new Padding(50, 0, 0, 0); // Leave space for icon

            MouseEnter += (s, e) => { _isHovered = true; Invalidate(); };
            MouseLeave += (s, e) => { _isHovered = false; Invalidate(); };
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            if (_isHovered && !IsActive)
            {
                // Use a darker, semi-transparent color for better contrast
                using (var brush = new SolidBrush(Color.FromArgb(40, 255, 255, 255)))
                {
                    pevent.Graphics.FillRectangle(brush, ClientRectangle);
                }
            }

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
                ForeColor = _isHovered ? Color.White : Theme.TextDim;
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
}
