using System.Drawing;

namespace FpsBooster.Views
{
    public static class Theme
    {
        // Colors
        public static readonly Color Background = Color.FromArgb(12, 12, 18);     // Deep Dark Blue-Black
        public static readonly Color Sidebar = Color.FromArgb(18, 18, 25);        // Rich Dark Sidebar
        public static readonly Color Surface = Color.FromArgb(25, 25, 35);        // Elevated Surface
        public static readonly Color Accent = Color.FromArgb(255, 85, 0);         // Vibrant Electric Orange
        public static readonly Color AccentHover = Color.FromArgb(255, 120, 40);  // Bright Orange Hover
        public static readonly Color AccentGlow = Color.FromArgb(255, 60, 0);     // Deep Orange Glow
        public static readonly Color Text = Color.FromArgb(250, 250, 255);        // Pure White with slight blue
        public static readonly Color TextDim = Color.FromArgb(160, 165, 180);     // Cool Gray
        public static readonly Color Success = Color.FromArgb(0, 255, 100);       // Neon Green
        public static readonly Color Warning = Color.FromArgb(255, 200, 0);       // Warning Gold
        public static readonly Color Error = Color.FromArgb(255, 80, 80);         // Error Red

        // Segoe MDL2 Assets Icons
        public static readonly string IconPower = "\uE7E8"; // Lightning
        public static readonly string IconLog = "\uE756";   // Console
        public static readonly string IconSettings = "\uE713"; 
        public static readonly string IconCheck = "\uE73E";
        public static readonly string IconRocket = "\uE99A";
        public static readonly string IconFlash = "\uE945";
        public static readonly string IconGame = "\uE7BE";    // Game Controller
        public static readonly string IconNetwork = "\uE12B"; // Globe
        public static readonly string IconDocs = "\uE8A5";    // Documentation/Book
        public static readonly string IconClose = "\uE8BB";   // Chrome Close
        public static readonly string IconMinimize = "\uE921"; // Chrome Minimize

        // Fonts
        public const string MainFont = "Roboto";
        public const string FallbackFont = "Segoe UI";
        
        // Metadata
        public const string AppVersion = "v1.2.0 ELITE";
        public const string Developer = "⚡ Developed by Raul W. | Premium Edition";
    }
}
