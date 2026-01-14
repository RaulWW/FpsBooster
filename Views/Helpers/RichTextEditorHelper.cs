using System.Drawing;
using System.Windows.Forms;

namespace FpsBooster.Views.Helpers
{
    public static class RichTextEditorHelper
    {
        public static void ApplyCs2SyntaxHighlighting(RichTextBox rtb, Color defaultColor)
        {
            int selectionStart = rtb.SelectionStart;
            int selectionLength = rtb.SelectionLength;
            
            rtb.SuspendLayout();
            rtb.SelectAll();
            rtb.SelectionColor = defaultColor;

            string[] lines = rtb.Lines;
            int currentPos = 0;
            
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) 
                {
                    currentPos += line.Length + 1;
                    continue;
                }

                if (line.StartsWith("//"))
                {
                    rtb.Select(currentPos, line.Length);
                    rtb.SelectionColor = Color.Gray;
                }
                else
                {
                    int firstSpace = line.IndexOf(' ');
                    int highlightLength = firstSpace > 0 ? firstSpace : line.Length;
                    
                    rtb.Select(currentPos, highlightLength);
                    rtb.SelectionColor = Color.LightBlue;
                }
                currentPos += line.Length + 1;
            }

            rtb.Select(selectionStart, selectionLength);
            rtb.SelectionColor = defaultColor;
            rtb.ResumeLayout();
        }

        public static void HighlightLogTags(RichTextBox rtb, int startOffset, string text, Color defaultHighlightColor)
        {
            var tagColors = new Dictionary<string, Color>
            {
                { "[INFO]", Color.FromArgb(16, 185, 129) },     // Green
                { "[WARNING]", Color.FromArgb(245, 158, 11) },  // Amber
                { "[ERROR]", Color.FromArgb(255, 80, 80) },     // Red
                { "SUCCESS", Color.FromArgb(16, 185, 129) },    // Green
                { "FATAL", Color.FromArgb(255, 80, 80) }        // Red
            };

            foreach (var tag in tagColors)
            {
                int index = text.IndexOf(tag.Key);
                while (index != -1)
                {
                    rtb.Select(startOffset + index, tag.Key.Length);
                    rtb.SelectionColor = tag.Value;
                    rtb.SelectionFont = new Font(rtb.Font, FontStyle.Bold);
                    index = text.IndexOf(tag.Key, index + tag.Key.Length);
                }
            }
        }
    }
}
