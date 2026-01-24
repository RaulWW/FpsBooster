using System.Runtime.InteropServices;
using System.Text;

namespace FpsBooster.Configuration
{
    public static class SettingsManager
    {
        private static readonly string SettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder retVal, int size, string filePath);

        public static void SaveSetting(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, SettingsPath);
        }

        public static string GetSetting(string section, string key, string defaultValue = "")
        {
            var res = new StringBuilder(255);
            GetPrivateProfileString(section, key, defaultValue, res, 255, SettingsPath);
            return res.ToString();
        }

        public static void InitializeDefaultSettings()
        {
            if (!File.Exists(SettingsPath))
            {
                SaveSetting("CS2", "AutoReload", "True");
                SaveSetting("CS2", "ConfigPath", GamePaths.GetCounterStrike2ConfigPath());
            }
        }
    }
}
