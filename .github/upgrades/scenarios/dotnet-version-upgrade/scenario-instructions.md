# .NET Version Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: net10.0
- **Scope**: All projects in the solution

## Source Control
- **Source Branch**: main
- **Working Branch**: upgrade-dotnet-10
- **Commit Strategy**: After Each Task
- **Branch Sync**: Auto (Merge)

## Upgrade Options
**Source**: .github/upgrades/scenarios/dotnet-version-upgrade/upgrade-options.md

### Strategy
- Upgrade Strategy: All-at-Once

### Compatibility
- Unsupported API Handling: Fix Inline

## Strategy
**Selected**: All-at-Once
**Rationale**: Six SDK-style projects already target modern .NET, the dependency graph is at most three tiers deep, and the upgrade is a .NET 8 to .NET 10 migration.

### Execution Constraints
- Upgrade all projects together in one atomic pass; do not introduce tier ordering or multi-targeting.
- Verify the .NET 10 SDK and global.json compatibility before changing project files.
- Update target frameworks and package references, restore dependencies, then build and fix all compilation errors.
- Run the test suite only after the atomic upgrade builds successfully.
