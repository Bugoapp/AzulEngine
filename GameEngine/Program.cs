#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace AzulEngine
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        private static Game1 game;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if DEBUG
            // Add this; Change the Locales(En-US): Done.
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
#endif
            game = new Game1();
            game.Run();
        }
    }
}
