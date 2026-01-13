param (
    [string]$Version = ""
)

$IsccPath = "C:\Program Files (x86)\Inno Setup 6\iscc.exe"
$IssPath = "c:\Users\Raul\Desktop\Github\FpsBooster\setup.iss"

# 1. Update version in .iss if provided
if ($Version -ne "") {
    Write-Host "Updating version to $Version in setup.iss..." -ForegroundColor Cyan
    (Get-Content $IssPath) -replace 'AppVersion=.*', "AppVersion=$Version" | Set-Content $IssPath
} else {
    # Extract version from file if not provided
    $VersionLine = Get-Content $IssPath | Select-String "AppVersion="
    $Version = $VersionLine.ToString().Split('=')[1].Trim()
}

Write-Host "Starting Build for Version $Version..." -ForegroundColor Green

# 2. Dotnet Publish
Write-Host "Executing dotnet publish..." -ForegroundColor Gray
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishReadyToRun=true -o publish FpsBooster.csproj

if ($LastExitCode -ne 0) {
    Write-Error "Build failed!"
    exit $LastExitCode
}

# 3. Inno Setup Compilation
Write-Host "Compiling Installer..." -ForegroundColor Gray
& $IsccPath $IssPath

if ($LastExitCode -ne 0) {
    Write-Error "Installer compilation failed!"
    exit $LastExitCode
}

# 4. Git Commit and Tag
Write-Host "Committing changes to Git..." -ForegroundColor Yellow
git add .
git commit -m "Build: automated release v$Version"
git tag -f "v$Version" -m "Release version $Version"

# 5. Push to GitHub
Write-Host "Pushing to GitHub..." -ForegroundColor Yellow
git push origin main
git push origin --tags -f

Write-Host "`nSuccessfully released v$Version!" -ForegroundColor Green
Write-Host "Remember to manually upload FPS_Booster_Setup.exe to GitHub if you don't have GH CLI installed." -ForegroundColor Magenta
