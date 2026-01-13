using System.Drawing;

namespace FpsBooster.Views
{
    public static class Theme
    {
        // Colors - Updated Color Scheme
        public static readonly Color BgDark = Color.FromArgb(11, 17, 32);         // #0B1120
        public static readonly Color BgCard = Color.FromArgb(21, 30, 50);         // #151E32
        public static readonly Color Background = Color.FromArgb(11, 17, 32);     // Same as BgDark
        public static readonly Color Sidebar = Color.FromArgb(21, 30, 50);        // Same as BgCard
        public static readonly Color Surface = Color.FromArgb(25, 25, 35);        // Elevated Surface
        public static readonly Color Accent = Color.FromArgb(255, 85, 0);         // Vibrant Electric Orange
        public static readonly Color AccentHover = Color.FromArgb(255, 120, 40);  // Bright Orange Hover
        public static readonly Color AccentGlow = Color.FromArgb(255, 60, 0);     // Deep Orange Glow
        
        // Text Colors
        public static readonly Color TextPrimary = Color.FromArgb(255, 255, 255); // #FFFFFF
        public static readonly Color TextSecondary = Color.FromArgb(148, 163, 184); // #94A3B8
        public static readonly Color Text = Color.FromArgb(255, 255, 255);        // Same as TextPrimary
        public static readonly Color TextDim = Color.FromArgb(148, 163, 184);     // Same as TextSecondary
        
        // Accent Colors
        public static readonly Color AccentBlue = Color.FromArgb(59, 130, 246);   // #3B82F6
        public static readonly Color AccentGreen = Color.FromArgb(16, 185, 129);  // #10B981
        public static readonly Color AccentAmber = Color.FromArgb(245, 158, 11);  // #F59E0B
        public static readonly Color AccentPurple = Color.FromArgb(139, 92, 246); // #8B5CF6
        
        // Legacy Color Mappings
        public static readonly Color Success = Color.FromArgb(16, 185, 129);      // AccentGreen
        public static readonly Color Warning = Color.FromArgb(245, 158, 11);      // AccentAmber
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
        public const string AppVersion = "v2.0 ELITE";
        public const string Developer = "⚡ Developed by Raul W. | Premium Edition";
    }
}
