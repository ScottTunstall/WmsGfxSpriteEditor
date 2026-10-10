# 01-prerequisites: Verify the .NET 10 toolchain

Verify that the .NET 10 SDK is installed and that any repository SDK selection is compatible with the target framework. Review repository-level build configuration before the upgrade so the solution can be restored and built using the intended SDK.

## Research Findings

- `validate_dotnet_sdk_installation` confirmed a compatible .NET 10 SDK.
- `dotnet --list-sdks` reports SDK `10.0.401` at `C:\Program Files\dotnet\sdk`.
- No `global.json`, `Directory.Build.props`, or `Directory.Build.targets` files were found in the workspace search, so no repository SDK pin or centralized target framework override needs adjustment.
- The solution consists of SDK-style projects, so the planned validation tool is `dotnet build`; the WinForms application may require Visual Studio MSBuild if resource generation errors occur during the upgrade.

**Done when**: The required .NET 10 SDK is available, repository SDK configuration is compatible, and the solution is ready for the framework update.
