# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade all projects in `WmsGfxSpriteEditor.sln` from .NET 8 to .NET 10.
**Scope**: Six SDK-style projects, including one Windows Forms application and five class libraries, with 59 code files and assessment findings concentrated in WinForms and System.Drawing APIs.

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: Six projects, all already on modern .NET, with a dependency graph no deeper than three tiers.

## Tasks

### 01-prerequisites: Verify the .NET 10 toolchain

Verify that the .NET 10 SDK is installed and that any repository SDK selection is compatible with the target framework. Review repository-level build configuration before the upgrade so the solution can be restored and built using the intended SDK.

**Done when**: The required .NET 10 SDK is available, repository SDK configuration is compatible, and the solution is ready for the framework update.

---

### 02-upgrade-projects: Upgrade all projects and resolve compatibility changes

Update all six SDK-style projects to their appropriate .NET 10 target frameworks, preserving the Windows-specific target for the WinForms application. Apply the assessment-recommended `System.Drawing.Common` update where appropriate, restore dependencies, and resolve the source-incompatible API findings inline across the application and Sprites library. Research the affected WinForms and GDI+ call sites before changing behavior, and preserve existing project references and build settings.

**Done when**: Every project targets .NET 10, package references are compatible with the new target, restore succeeds, and the complete solution builds without errors or warnings.

---

### 03-final-validation: Validate the upgraded solution

Run the full solution build and available test suite after the atomic upgrade task completes. Review generated outputs and runtime-sensitive WinForms/System.Drawing changes for regressions, and document any non-blocking follow-up recommendations in the task record.

**Done when**: The solution build is clean, all available tests pass, and validation results are recorded with no unresolved upgrade errors.
