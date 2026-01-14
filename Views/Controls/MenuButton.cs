using System.Drawing;
using System.Windows.Forms;

namespace FpsBooster.Views.Controls
{
    public class MenuButton : Button
    {
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        [System.ComponentModel.Browsable(false)]
        public bool IsActive { get; set; }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        [System.ComponentModel.Browsable(false)]
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
            // Fill background with Sidebar color to prevent white flash/selection
            using (var bgBrush = new SolidBrush(Theme.Sidebar))
            {
                pevent.Graphics.FillRectangle(bgBrush, ClientRectangle);
            }

            if (IsActive)
            {
                // Subtle blue background for active state to match theme
                using (var activeBrush = new SolidBrush(Color.FromArgb(40, 59, 130, 246)))
                {
                    pevent.Graphics.FillRectangle(activeBrush, ClientRectangle);
                }

                ForeColor = Color.White;
            }
            else if (_isHovered)
            {
                // Subtle blue tint for hover
                using (var hoverBrush = new SolidBrush(Color.FromArgb(30, 59, 130, 246)))
                {
                    pevent.Graphics.FillRectangle(hoverBrush, ClientRectangle);
                }
                ForeColor = Color.White;
            }
            else
            {
                ForeColor = Theme.TextDim;
            }

            if (!string.IsNullOrEmpty(Icon))
            {
                using (var font = new Font("Segoe MDL2 Assets", 14F))
                {
                    // Use distinct colors for icon visibility
                    Color iconColor = IsActive ? Theme.Accent : (_isHovered ? Color.White : Color.FromArgb(148, 163, 184));
                    using (var iconBrush = new SolidBrush(iconColor))
                    {
                        pevent.Graphics.DrawString(Icon, font, iconBrush, 15, (Height - 18) / 2);
                    }
                }
            }

            // Draw text manually to better control appearance and avoid base button artifacts
            TextRenderer.DrawText(pevent.Graphics, Text, Font, 
                new Rectangle(50, 0, Width - 50, Height), ForeColor, 
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }
    }
}
