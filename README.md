<div align="center">

# ⚡ ULTRA FPS BOOSTER

### Suite de Otimização de Performance para Windows 10/11

![Última Release](https://img.shields.io/github/v/release/RaulWW/FpsBooster?style=for-the-badge&logo=github&label=ÚLTIMA%20VERSÃO&color=FF6B35)
![Total de Downloads](https://img.shields.io/github/downloads/RaulWW/FpsBooster/total?style=for-the-badge&logo=github&label=DOWNLOADS&color=4CAF50)
![Licença](https://img.shields.io/badge/LICENÇA-MIT-blue?style=for-the-badge)

![GitHub Stars](https://img.shields.io/github/stars/RaulWW/FpsBooster?style=for-the-badge&logo=github&label=STARS&color=FFD700)
![Visualizações](https://komarev.com/ghpvc/?username=RaulWW&repo=FpsBooster&style=for-the-badge&label=VISUALIZAÇÕES&color=blueviolet)
![Plataforma](https://img.shields.io/badge/PLATAFORMA-WINDOWS%2010%2F11-0078D6?style=for-the-badge&logo=windows)

[![DOCUMENTAÇÃO](https://img.shields.io/badge/📖_DOCUMENTAÇÃO-LER_AGORA-4FC3F7?style=for-the-badge)](#-o-que-é-otimizado)
[![BAIXAR AGORA](https://img.shields.io/badge/📥_BAIXAR-ÚLTIMA_VERSÃO-FF6B35?style=for-the-badge)](https://github.com/RaulWW/FpsBooster/releases/latest)

---

**ULTRA FPS BOOSTER** é uma ferramenta de otimização de sistema de alto desempenho projetada especificamente para **gamers** e **power users**. Simplifica ajustes complexos do sistema em uma experiência de um clique, garantindo que seu ambiente Windows esteja pronto para máxima performance em jogos.

Especialmente otimizado para **Counter-Strike 2 (CS2)** e jogos competitivos.

</div>

---

## 🚀 Principais Recursos

### ⚡ Ultimate Boost com Um Clique

- **Plano de Energia Ultimate Performance**: Desbloqueia e ativa automaticamente o esquema de energia "Ultimate Performance" oculto do Windows
- **Limpeza Profunda do Sistema**: Remove arquivos temporários, prefetch e cache de `%TEMP%`, `C:\Windows\Temp` e caches de aplicativos
- **Desativação Agressiva de Telemetria**: Bloqueia coleta de dados do Windows via Registro e desativa 11+ tarefas agendadas de rastreamento
- **Otimização de Registro & BCD**: Desativa Otimizações de Tela Cheia, GameDVR e otimiza timers do sistema
- **Ajustes de Rede**: Desativa Teredo tunneling, otimiza processos svchost e para serviços pesados

### 🎮 Ajustes para CS2

- **Editor de Autoexec**: Editor integrado com syntax highlighting para seu `autoexec.cfg`
- **Redução de Latência**: Ajustes de rede e sistema para minimizar input lag e maximizar FPS
- **Salvar com um clique** diretamente na pasta de config do CS2

### 🌐 Diagnósticos de Rede

- **Monitoramento em Tempo Real**: Teste Ping, Jitter e Perda de Pacotes para qualquer IP/hostname
- **Servidores Pré-configurados**: IPs prontos para Faceit e Gamers Club
- **Indicadores de Qualidade**: Feedback dinâmico com cores (Verde/Amarelo/Vermelho)

### 💎 Experiência Premium

- **UI Moderna Dark**: Interface inspirada em glassmorphism com controles WinForms customizados
- **Logging em Tempo Real**: Progresso de instalação com timestamps e tags coloridas
- **Janela Sem Bordas**: Barra de título customizada para visual moderno e clean
- **Tipografia Roboto**: Fonte profissional para melhor legibilidade

### 📥 Downloads & Dependências

- **Instalador .NET Framework**: Instalação com um clique do .NET 2.0, 3.0, 3.5, 4.x
- **Visual C++ All-in-One**: Pacote completo de Visual C++ Redistributables (essencial para 95% dos jogos modernos)
- **Logs de Instalação em Tempo Real**: Visibilidade opcional do que está acontecendo durante as instalações

---

## 🛠️ O Que É Otimizado

<details>
<summary><b>1️⃣ Gerenciamento de Energia</b></summary>

- Ativa plano de energia Ultimate Performance (desativa core parking e C-States da CPU)
- Mantém clock máximo da CPU o tempo todo
- Elimina micro-stutters causados por mudanças de frequência
</details>

<details>
<summary><b>2️⃣ Limpeza do Sistema</b></summary>

- Limpa Windows/User Temp, Prefetch, Event Logs
- Remove caches de browsers (Chrome, Edge), Discord, Spotify, Steam, VS Code
- Limpa Delivery Optimization, logs do Windows Defender e bancos de dados de miniaturas
- **Libera GBs de espaço** e reduz overhead de I/O
</details>

<details>
<summary><b>3️⃣ Registro & Configuração de Boot</b></summary>

- Desativa GameDVR e Otimizações de Tela Cheia
- Ativa "Finalizar Tarefa" na barra de tarefas
- Remove Objetos 3D do Explorer
- Define bcdedit bootmenupolicy como Legacy
</details>

<details>
<summary><b>4️⃣ Telemetria & Privacidade</b></summary>

- Define `AllowTelemetry = 0` no Registro
- Desativa publicação de atividades do usuário (Timeline)
- Bloqueia CloudContent e AdvertisingInfo
- Desativa 11+ tarefas agendadas de coleta de dados
</details>

<details>
<summary><b>5️⃣ Serviços & Rede</b></summary>

- Otimiza separação de processos svchost
- Desativa SysMain (Superfetch) e WSearch (indexação do Windows Search)
- Desativa túnel IPv6 Teredo
- Bloqueia serviços de diagnóstico AutoLogger
</details>

<details>
<summary><b>6️⃣ Processos em Segundo Plano</b></summary>

- Encerra Adobe Genuine Service
- Bloqueia IPs de rastreamento da Adobe no arquivo HOSTS
</details>

---

## 📥 Instalação

1. **Baixe** a versão mais recente: [**FBooster_v2.1.exe**](https://github.com/RaulWW/FpsBooster/releases/latest)
2. **Execute como Administrador** (necessário para modificações do sistema)
3. Clique em **APPLY PERFORMANCE CFG** para otimizar seu sistema
4. **(Opcional)** Configure autoexec do CS2 e diagnósticos de rede

> **Requisitos**: Windows 10/11 com .NET 10 Runtime (instalado automaticamente pelo setup se ausente)

---

## ✅ Verificação

Verifique se as otimizações foram aplicadas:

```powershell
# Verificar Plano de Energia
powercfg /list
# → "Ultimate Performance" deve estar ativo (*)

# Verificar Telemetria
# Registro: HKLM\SOFTWARE\Policies\Microsoft\Windows\DataCollection
# → AllowTelemetry = 0

# Verificar Teredo
netsh interface teredo show state
# → Status: disabled

# Verificar GameDVR
# Registro: HKCU\System\GameConfigStore
# → GameDVR_DXGIHonorFSEWindowsCompatible = 1
```

---

## 🤝 Contribuindo

Este projeto é **open-source** sob a licença MIT. Contribuições são bem-vindas!

- ⭐ **Dê uma star** se esta ferramenta melhorou sua performance em jogos
- 🐛 **Reporte bugs** via [Issues](https://github.com/RaulWW/FpsBooster/issues)
- 💡 **Sugira funcionalidades** ou envie Pull Requests

---

## 📚 Stack Tecnológica

- **C# / .NET 10.0** - Framework de última geração
- **WinForms** - Controles customizados e UI premium
- **PowerShell Engine** - Otimização nativa do sistema Windows
- **Syntax Highlighting** - Editor de código baseado em RichTextBox

---

<div align="center">

**Desenvolvido com ⚡ por [Raul W.](https://github.com/RaulWW)**

_Focado em Performance. Feito para Gamers._

[![GitHub](https://img.shields.io/badge/GitHub-RaulWW-181717?style=for-the-badge&logo=github)](https://github.com/RaulWW)

</div>
