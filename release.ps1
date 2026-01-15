param (
    [Parameter(Mandatory=$true)]
    [string]$Version
)

$ErrorActionPreference = "Stop"

$IsccPath = "C:\Program Files (x86)\Inno Setup 6\iscc.exe"
$ProjectRoot = "C:\Users\Raul\Desktop\Github\FpsBooster"
$IssPath = "$ProjectRoot\setup.iss"
$VersionRcPath = "$ProjectRoot\VERSION.RC"
$ThemePath = "$ProjectRoot\Views\Theme.cs"

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "   FULL DEPLOYMENT - FPS Booster" -ForegroundColor Cyan
Write-Host "   Version: $Version" -ForegroundColor Cyan
Write-Host "========================================`n" -ForegroundColor Cyan

function Update-VersionRC {
    param([string]$NewVersion)
    
    Write-Host "[1/7] Updating VERSION.RC..." -ForegroundColor Yellow
    
    $versionParts = $NewVersion.Split('.')
    while ($versionParts.Length -lt 4) {
        $versionParts += "0"
    }
    $fileVersion = $versionParts -join ","
    $fileVersionDot = $versionParts -join "."
    
    $content = Get-Content $VersionRcPath -Raw
    $content = $content -replace "FILEVERSION \d+,\d+,\d+,\d+", "FILEVERSION $fileVersion"
    $content = $content -replace "PRODUCTVERSION \d+,\d+,\d+,\d+", "PRODUCTVERSION $fileVersion"
    $content = $content -replace 'VALUE "FileVersion", "[\d\.]+"', "VALUE `"FileVersion`", `"$fileVersionDot`""
    $content = $content -replace 'VALUE "ProductVersion", "[\d\.]+"', "VALUE `"ProductVersion`", `"$fileVersionDot`""
    
    Set-Content $VersionRcPath -Value $content -NoNewline
    Write-Host "  ✓ VERSION.RC updated to $fileVersion" -ForegroundColor Green
}

function Update-ThemeVersion {
    param([string]$NewVersion)
    
    Write-Host "[2/7] Updating Theme.cs..." -ForegroundColor Yellow
    
    $content = Get-Content $ThemePath -Raw
    $content = $content -replace 'public const string AppVersion = "v[\d\.]+";', "public const string AppVersion = `"v$NewVersion`";"
    
    Set-Content $ThemePath -Value $content -NoNewline
    Write-Host "  ✓ Theme.cs updated to v$NewVersion" -ForegroundColor Green
}

function Update-InnoSetup {
    param([string]$NewVersion)
    
    Write-Host "[3/7] Updating setup.iss..." -ForegroundColor Yellow
    
    $content = Get-Content $IssPath -Raw
    $content = $content -replace 'AppVersion=[\d\.]+', "AppVersion=$NewVersion"
    
    Set-Content $IssPath -Value $content -NoNewline
    Write-Host "  ✓ setup.iss updated to $NewVersion" -ForegroundColor Green
}

Update-VersionRC -NewVersion $Version
Update-ThemeVersion -NewVersion $Version
Update-InnoSetup -NewVersion $Version

Write-Host "`n[4/7] Building project..." -ForegroundColor Yellow
Set-Location $ProjectRoot
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishReadyToRun=true -o publish FpsBooster.csproj

if ($LASTEXITCODE -ne 0) {
    Write-Error "Build failed!"
    exit 1
}
Write-Host "  ✓ Build completed successfully" -ForegroundColor Green

Write-Host "`n[5/7] Compiling installer..." -ForegroundColor Yellow
$InstallerName = "FBooster_v$Version"
& $IsccPath "/F$InstallerName" $IssPath

if ($LASTEXITCODE -ne 0) {
    Write-Error "Installer compilation failed!"
    exit 1
}
Write-Host "  ✓ Installer created: $InstallerName.exe" -ForegroundColor Green

Write-Host "`n[6/7] Committing to Git..." -ForegroundColor Yellow
git add .
git commit -m "Release: v$Version"
git tag -f "v$Version" -m "Release version $Version"
git push origin main
git push origin --tags -f
Write-Host "  ✓ Changes pushed to GitHub" -ForegroundColor Green

Write-Host "`n[7/7] Creating GitHub Release..." -ForegroundColor Yellow
$InstallerPath = "$ProjectRoot\$InstallerName.exe"

$ghInstalled = Get-Command gh -ErrorAction SilentlyContinue
if ($ghInstalled) {
    $releaseNotes = @"
## 🚀 FPS Booster v$Version

### Download
- **Installer:** ``$InstallerName.exe``

### Changes
- Automated release build
- All optimizations included
- Self-contained .NET 10.0 runtime

### Installation
1. Download ``$InstallerName.exe``
2. Run as Administrator
3. Follow the installation wizard

---
⚡ Developed by Raul W.
"@
    
    gh release create "v$Version" `
        "$InstallerPath" `
        --title "FPS Booster v$Version" `
        --notes "$releaseNotes" `
        --repo RaulWW/FpsBooster
    
    Write-Host "  ✓ GitHub Release created with installer uploaded!" -ForegroundColor Green
} else {
    Write-Host "  ⚠ GitHub CLI (gh) not found. Please install it:" -ForegroundColor Yellow
    Write-Host "    winget install GitHub.cli" -ForegroundColor Gray
    Write-Host "`n  Manual upload required:" -ForegroundColor Yellow
    Write-Host "    1. Go to: https://github.com/RaulWW/FpsBooster/releases/new" -ForegroundColor Gray
    Write-Host "    2. Tag: v$Version" -ForegroundColor Gray
    Write-Host "    3. Upload: $InstallerPath" -ForegroundColor Gray
}

Write-Host "`n========================================" -ForegroundColor Green
Write-Host "   ✓ DEPLOYMENT COMPLETED!" -ForegroundColor Green
Write-Host "   Version $Version released successfully" -ForegroundColor Green
Write-Host "========================================`n" -ForegroundColor Green

Write-Host "Next steps:" -ForegroundColor Cyan
Write-Host "  • Check release: https://github.com/RaulWW/FpsBooster/releases/tag/v$Version" -ForegroundColor Gray
Write-Host "  • Test installer: $InstallerPath`n" -ForegroundColor Gray
