# 02-upgrade-projects: Upgrade all projects and resolve compatibility changes

Update all six SDK-style projects to their appropriate .NET 10 target frameworks, preserving the Windows-specific target for the WinForms application. Apply the assessment-recommended `System.Drawing.Common` update where appropriate, restore dependencies, and resolve the source-incompatible API findings inline across the application and Sprites library. Research the affected WinForms and GDI+ call sites before changing behavior, and preserve existing project references and build settings.

## Research Findings

### Projects and dependency structure

- `WmsGfxSpriteEditor\WmsGfxSpriteEditor.csproj`: WinForms entry point; `net8.0-windows` -> `net10.0-windows`; references Sprites, Robotron2084 ROMs, and ROMs. Assessment: 2,018 issues, primarily WinForms API compatibility findings.
- `WmsGfxSpriteEditor.Sprites\WmsGfxSpriteEditor.Sprites.csproj`: class library; `net8.0` -> `net10.0`; references History and ROMs. Assessment: 9 System.Drawing source-compatibility findings and one package recommendation.
- `WmsGfxSpriteEditor.History\WmsGfxSpriteEditor.History.csproj`: class library; framework-only change, no API issues.
- `WmsGfxSpriteEditor.Palettes\WmsGfxSpriteEditor.Palettes.csproj`: class library; framework-only change, no API issues.
- `WmsGfxSpriteEditor.Roms\WmsGfxSpriteEditor.Roms.csproj`: class library; framework-only change, no API issues.
- `WmsGfxSpriteEditor.Roms.Robotron2084\WmsGfxSpriteEditor.Roms.Robotron2084.csproj`: class library; framework-only change, references Palettes, ROMs, and Sprites.

### Package actions

- No Directory.Packages.props or Directory.Build.props was found; package versions are defined directly in project files.
- Update `System.Drawing.Common` from `8.0.17` to the assessment-recommended `10.0.12` in `WmsGfxSpriteEditor.Sprites.csproj`.
- Preserve compatible `StyleCop.Analyzers` 1.1.118 references and transitive `Microsoft.Win32.SystemEvents` dependencies unless restore/build identifies a concrete compatibility issue.

### API areas to validate

- The Sprites library intentionally exposes `System.Drawing.Bitmap`, `Color`, and pixel operations in `ISprite.cs`, `Sprite4Bpp.cs`, and `Commands\SetSpritePixelsFromBitmapCommand.cs`.
- The WinForms application intentionally uses Windows Forms controls, dialogs, clipboard, and GDI+ drawing throughout the UI. The assessment's large binary-incompatibility count is expected to be revalidated by the .NET 10 compiler; do not mechanically rewrite APIs that remain supported.
- No `// STUB:` markers were found in the source tree.
- All projects are SDK-style and no conversion or multi-targeting is required. Use `dotnet restore/build` by default; switch to Visual Studio MSBuild only if WinForms resource generation produces the documented resource errors.

**Done when**: Every project targets .NET 10, package references are compatible with the new target, restore succeeds, and the complete solution builds without errors or warnings.
