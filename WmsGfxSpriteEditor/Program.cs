namespace WmsGfxSpriteEditor
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
#pragma warning disable SA1400 // Access modifier should be declared
#pragma warning disable IDE0040 // Add accessibility modifiers
        static void Main()
#pragma warning restore IDE0040 // Add accessibility modifiers
#pragma warning restore SA1400 // Access modifier should be declared
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.Run(new MainForm());
        }
    }
}