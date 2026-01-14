using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FpsBooster.Views.Controls
{
    public class ModernButton : Button
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Image? ButtonIcon { get; set; }

        public ModernButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Theme.Accent;
            ForeColor = Color.White;
            Font = new Font(Theme.MainFont, 10F, FontStyle.Bold);
            if (Font.Name != Theme.MainFont) Font = new Font(Theme.FallbackFont, 10F, FontStyle.Bold);
            Cursor = Cursors.Hand;
            TextImageRelation = TextImageRelation.ImageBeforeText;
            ImageAlign = ContentAlignment.MiddleLeft;
            TextAlign = ContentAlignment.MiddleCenter;
            Padding = new Padding(10, 0, 10, 0);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            if (ButtonIcon != null)
            {
                int iconWidth = 24;
                int iconHeight = 24;
                int x = 10;
                int y = (Height - iconHeight) / 2;
                pevent.Graphics.DrawImage(ButtonIcon, x, y, iconWidth, iconHeight);
            }
        }
    }
}
