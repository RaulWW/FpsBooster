# 🚀 FPS Booster - Release Script

Script completo de deploy automatizado.

## Uso

```powershell
.\release.ps1 -Version "2.3"
```

## O que o script faz

1. ✅ Atualiza **VERSION.RC** (FILEVERSION, PRODUCTVERSION, FileVersion, ProductVersion)
2. ✅ Atualiza **Theme.cs** (AppVersion)
3. ✅ Atualiza **setup.iss** (AppVersion)
4. ✅ Build do projeto (dotnet publish)
5. ✅ Compila instalador (Inno Setup)
6. ✅ Commit e tag no Git
7. ✅ **Upload automático do instalador para GitHub Release** (se GitHub CLI estiver instalado)

## Pré-requisitos

### Obrigatórios:

- .NET SDK
- Inno Setup 6
- Git configurado

### Opcional (para upload automático):

```powershell
winget install GitHub.cli
gh auth login
```

## Exemplo Completo

```powershell
# Release versão 2.3
.\release.ps1 -Version "2.3"
```

### Output esperado:

```
========================================
   FULL DEPLOYMENT - FPS Booster
   Version: 2.3
========================================

[1/7] Updating VERSION.RC...
  ✓ VERSION.RC updated to 2,3,0,0

[2/7] Updating Theme.cs...
  ✓ Theme.cs updated to v2.3

[3/7] Updating setup.iss...
  ✓ setup.iss updated to 2.3

[4/7] Building project...
  ✓ Build completed successfully

[5/7] Compiling installer...
  ✓ Installer created: FBooster_v2.3.exe

[6/7] Committing to Git...
  ✓ Changes pushed to GitHub

[7/7] Creating GitHub Release...
  ✓ GitHub Release created with installer uploaded!

========================================
   ✓ DEPLOYMENT COMPLETED!
   Version 2.3 released successfully
========================================
```

## Sem GitHub CLI

Se você não tiver o GitHub CLI instalado, o script vai:

- ✅ Fazer tudo até o passo 6
- ⚠️ Avisar que precisa fazer upload manual
- 📋 Mostrar instruções de como fazer

## Arquivos Atualizados

- `VERSION.RC` → Versão do executável
- `Views/Theme.cs` → Versão exibida no app
- `setup.iss` → Versão do instalador
- Tag git `v2.3`
- Release no GitHub com instalador anexado
