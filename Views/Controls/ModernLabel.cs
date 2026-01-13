using System.Drawing;
using System.Windows.Forms;

namespace FpsBooster.Views.Controls
{
    public class ModernLabel : Label
    {
        public ModernLabel()
        {
            ForeColor = Theme.Text;
            Font = new Font(Theme.MainFont, 10F);
            if (Font.Name != Theme.MainFont) Font = new Font(Theme.FallbackFont, 10F);
        }
    }
}
