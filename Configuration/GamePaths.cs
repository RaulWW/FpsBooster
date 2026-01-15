namespace FpsBooster.Configuration
{
    public static class GamePaths
    {
        public static string GetCounterStrike2ConfigPath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                "Steam",
                "steamapps",
                "common",
                "Counter-Strike Global Offensive",
                "game",
                "csgo",
                "cfg",
                "autoexec.cfg"
            );
        }

        public static string GetFallbackConfigPath()
        {
            return "autoexec.cfg";
        }
    }
}
