using System.Windows.Forms;

namespace FpsBooster.Views.Helpers
{
    public static class ThreadingHelper
    {
        public static void SafeInvoke(Control control, Action action)
        {
            if (control.InvokeRequired)
                control.Invoke(action);
            else
                action();
        }

        public static void SafeInvoke<T>(Control control, Action<T> action, T parameter)
        {
            if (control.InvokeRequired)
                control.Invoke(action, parameter);
            else
                action(parameter);
        }
    }
}
