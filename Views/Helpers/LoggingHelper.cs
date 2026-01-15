using System.Windows.Forms;

namespace FpsBooster.Views.Helpers
{
    public static class LoggingHelper
    {
        public static void AppendToRichTextBox(
            RichTextBox richTextBox,
            string message,
            bool includeTimestamp = true,
            bool applyHighlighting = false,
            Color? highlightColor = null)
        {
            ThreadingHelper.SafeInvoke(richTextBox, () =>
            {
                string formattedMessage = includeTimestamp
                    ? $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}"
                    : $"{message}{Environment.NewLine}";

                int startPosition = richTextBox.TextLength;
                
                richTextBox.SelectionStart = startPosition;
                richTextBox.SelectionLength = 0;
                richTextBox.SelectionColor = Theme.TextDim;
                
                richTextBox.AppendText(formattedMessage);

                if (applyHighlighting)
                {
                    RichTextEditorHelper.HighlightLogTags(
                        richTextBox,
                        startPosition,
                        formattedMessage,
                        highlightColor ?? Theme.Success);
                }

                richTextBox.SelectionStart = richTextBox.TextLength;
                richTextBox.ScrollToCaret();
            });
        }
    }
}
