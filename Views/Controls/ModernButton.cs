using System.Drawing;
using System.Windows.Forms;

namespace FpsBooster.Views.Controls
{
    public class ModernButton : Button
    {
        public ModernButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Theme.Accent;
            ForeColor = Color.White;
            Font = new Font(Theme.MainFont, 10F, FontStyle.Bold);
            if (Font.Name != Theme.MainFont) Font = new Font(Theme.FallbackFont, 10F, FontStyle.Bold);
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            // Additional styling can be added here
        }
    }
}
