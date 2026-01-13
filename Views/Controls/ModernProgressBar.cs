using System;
using System.Drawing;
using System.Windows.Forms;

namespace FpsBooster.Views.Controls
{
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
}
