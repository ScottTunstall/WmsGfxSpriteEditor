# Upgrade Options — WmsGfxSpriteEditor

Assessment: 6 SDK-style projects on .NET 8; one WinForms application, 2,033 detected issues, and 79 source-incompatible API findings.

## Strategy

### Upgrade Strategy
The solution is a small modern-to-modern upgrade with six projects and a dependency depth of three; an atomic upgrade avoids temporary cross-targeting incompatibilities.

| Value | Description |
|-------|-------------|
| **All-at-Once** (selected) | Upgrade all projects simultaneously in a single atomic pass and validate the full solution afterward. |
| Top-Down | Upgrade entry-point applications first, temporarily multi-targeting shared libraries so the solution remains buildable throughout. |

## Compatibility

### Unsupported API Handling
The assessment identified source-incompatible APIs in the WinForms application and Sprites library; the default is to resolve them inline rather than defer work behind stubs.

| Value | Description |
|-------|-------------|
| **Fix Inline** (selected) | Resolve every API change in the upgrade tasks, including complex changes, leaving no deferred stubs. |
| Defer Complex Changes | Apply simple replacements inline and create compilable stubs plus follow-up tasks for complex API changes. |
