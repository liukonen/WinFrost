# WinFrost: Site-Specific Web Application Container
**Executive Summary:** This application provides a dedicated, constrained browsing environment built on CEFSharp/Chromium, abstracting the full web browser experience into a focused, site-specific application container.

---

## Technical Stack

### Runtime & Languages
*   .NET (Targeting x86 or x64 compilation via `Winfrost.sln`)
*   Managed Extensibility Framework (MEF)
*   C# (Primary implementation language)

### Infrastructure
*   CEFSharp (.NET controls wrapping Chromium engine)
*   GeckFX (.NET controls for Gecko engine fallback)
*   NuGet (Dependency management)

### Architectural Patterns
*   **Facade:** The primary application interface abstracts complex underlying browser rendering engines (Chromium/Gecko) into a unified, simple application launch mechanism.
*   **Adapter:** Implied pattern for interfacing disparate browser controls (CEFSharp, GeckFX) under a common application model.
*   **Configuration Management:** Utilizes dedicated routines for persistence of settings and global parameters.

---

## The Mission (Design History)
This project originated as a functional exploration following work on Peppermint Linux, aiming to encapsulate arbitrary web content into a dedicated, highly controlled Web Application instance. The initial intent was to move beyond general-purpose browsing by enforcing site-specific boundaries. The current implementation solidifies this concept by providing a mechanism to launch the application with or without initial parameters, maintaining usability while isolating the web context. *This remains a specialized, reference implementation.*

## Architecture & Decisions
**Goal:** Achieve sandboxing and specialized UX for web content viewing.

**Trade-offs:**
*   **Engine Dependency:** Dependence on third-party, evolving browser engines (Chromium/Gecko) forces the application to operate in a constrained maintenance mode. Zero-day exposure is a known risk requiring user acceptance.
*   **Internal Complexity vs. Stability:** The use of MEF and multiple underlying controls (CEFSharp/GeckFX) introduces internal complexity. This complexity is accepted to ensure that the application can present a stable, high-level API regardless of which underlying engine is compiled and linked.
*   **Execution Model:** The dual-mode execution (Settings Form vs. Direct Browser Launch) dictates that the startup sequence must perform environment introspection, adding branching logic complexity.

## Impact & ROI
*   **Risk Mitigation Blueprint:** Provides a tangible architectural blueprint for isolating potentially insecure web code execution into a manageable process boundary.
*   **Time Savings:** Eliminates the overhead of launching a full, general-purpose browser for single-site viewing needs.
*   **Modularity:** The reliance on MEF suggests a pathway for incorporating custom, site-specific plugins without modifying the core executable logic.

## The Golden Path
1.  `dotnet build --configuration Release` (Assumes necessary solution setup)
2.  `Winfrost.exe` (To launch the settings/setup wizard)
3.  `Winfrost.exe "http://example.com"` (To launch the container directly)

---

**DISCLAIMER:** This application relies heavily on external, rapidly evolving browser engines. Users acknowledge that running this software involves inherent security risks and is not intended for general, mission-critical browsing.

