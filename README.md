# ⚡ ULTRA FPS BOOSTER | Gaming Performance Suite

**ULTRA FPS BOOSTER** is a high-performance system optimization tool designed specifically for gamers and power users. It simplifies complex system tweaks into a single-click experience, ensuring your Windows environment is primed for maximum gaming performance, especially optimized for **Counter-Strike 2 (CS2)**.

![License](https://img.shields.io/badge/license-MIT-blue.svg)
![Platform](https://img.shields.io/badge/platform-Windows%2010%2F11-blue.svg)
![Version](https://img.shields.io/badge/version-v2.1-orange.svg)
![Views](https://hits.dwyl.com/RaulWW/FpsBooster.svg)
![Downloads](https://img.shields.io/github/downloads/RaulWW/FpsBooster/total?color=brightgreen)

## 🚀 Key Features

### 🛠️ One-Click Ultimate Boost

- **Ultimate Performance Plan**: Automatically unlocks and activates the hidden Windows "Ultimate Performance" power scheme.
- **Deep System Cleanup**: Silent removal of Temp files, Prefetch, and potentially unwanted cached data (`%TEMP%`, `C:\Windows\Temp`).
- **Telemetry Disabling**: Comprehensive blocking of Windows Telemetry, Data Collection, and User Activity tracking via Registry and Scheduled Tasks.
- **Registry & BCD Optimization**: Fine-tuned settings for lower system latency, including disabling "Fullscreen Optimizations" and "GameDVR".
- **Network Tweaks**: Disables Teredo tunneling to improve connectivity stability.

### 🎮 Game Specific Tweaks (CS2)

- **Autoexec Editor**: Integrated syntax-highlighted editor for your `autoexec.cfg`.
- **Latency Reduction**: Specific network and system tweaks to minimize input lag and maximize FPS.

### 🌐 Network Diagnostics

- **Real-time Monitoring**: Integrated tool to test Ping, Jitter, and Packet Loss against customizable targets (including Faceit IPs).
- **Quality Indicators**: Dynamic color-coded feedback on your connection health.

### 💎 Premium User Experience

- **Modern UI**: Dark-themed, glassmorphism-inspired interface built with custom controls.
- **Roboto Typography**: Modern and professional global font for enhanced readability.
- **Borderless Window**: Custom-built title bar for a sleek, modern desktop presence.
- **Informative Logging**: Real-time feedback with colored tags (`[INFO]`, `[WARNING]`, `[ERROR]`).

### 📥 Downloads & Extras

- **Feature Installer**: Integrated tab to easily install essentials like **.NET Framework (2.0, 3.0, 3.5)**.

## ✅ Verification & Tests

To verify the optimizations applied by this tool:

1. **Telemetry**:

   - Check Registry: `HKLM\SOFTWARE\Policies\Microsoft\Windows\DataCollection` -> `AllowTelemetry` should be `0`.
   - Check Task Scheduler: `\Microsoft\Windows\Application Experience\Microsoft Compatibility Appraiser` should be **Disabled**.

2. **Network**:

   - Run `netsh interface teredo show state` in PowerShell. Status should be **disabled**.

3. **Registry**:

   - Verify `HKCU:\System\GameConfigStore` -> `GameDVR_DXGIHonorFSEWindowsCompatible` is `1` (Fullscreen Optimizations Disabled).

4. **Temp Files**:
   - Check `%TEMP%` and `C:\Windows\Temp` folders; they should be cleared of safe-to-delete files.

## 📖 How It Works

For detailed information on what each optimization step does, check the **Documentation** section within the application sidebar. It explains everything from Power Plan configurations to Telemetry blocking.

## 🛠️ Built With

- **C# / .NET 10**: Modern, fast, and robust application core.
- **WinForms (Custom Styles)**: High-performance UI without the overhead of heavy frameworks.
- **PowerShell Engine**: Leverages native Windows scripting for safe and effective system modifications.
- **Roboto Font**: Ensuring a premium and modern aesthetic.

## 📥 Installation

1. Download the latest release from the [Releases](https://github.com/RaulWW/FpsBooster/releases) page.
2. Run `FPS_Booster_Setup.exe`.
3. Launch the app and click **APPLY PERFORMANCE CFG**.

## 🤝 Contributing

This project is open-source and contributions are welcome! Feel free to:

- Open an Issue for bugs or feature requests.
- Submit a Pull Request with improvements.

---

_Developed with ⚡ by **Raul W.** | Focused on Performance._
