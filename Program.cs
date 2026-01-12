using System.Diagnostics;
using System.Security.Principal;
using FpsBooster.Views;

namespace FpsBooster;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        if (!IsRunAsAdmin())
        {
            RestartAsAdmin();
            return;
        }

        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }

    private static bool IsRunAsAdmin()
    {
        var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    private static void RestartAsAdmin()
    {
        var processInfo = new ProcessStartInfo
        {
            FileName = Process.GetCurrentProcess().MainModule?.FileName,
            UseShellExecute = true,
            Verb = "runas"
        };

        try
        {
            Process.Start(processInfo);
        }
        catch (Exception)
        {
            // User cancelled elevation or error occurred
        }
    }
}
