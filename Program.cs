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
        try
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao iniciar aplicação:\n\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}", 
                "Erro Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
