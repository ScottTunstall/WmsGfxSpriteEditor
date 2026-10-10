# 02-upgrade-projects: Upgrade all projects and resolve compatibility changes

Update all six SDK-style projects to their appropriate .NET 10 target frameworks, preserving the Windows-specific target for the WinForms application. Apply the assessment-recommended `System.Drawing.Common` update where appropriate, restore dependencies, and resolve the source-incompatible API findings inline across the application and Sprites library. Research the affected WinForms and GDI+ call sites before changing behavior, and preserve existing project references and build settings.

**Done when**: Every project targets .NET 10, package references are compatible with the new target, restore succeeds, and the complete solution builds without errors or warnings.
