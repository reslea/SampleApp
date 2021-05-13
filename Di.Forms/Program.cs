using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Di.Container;
using Library.Data;

namespace Di.Forms
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(GetMainForm());
        }

        static DiContainer InitializeContainer()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<LibraryContext>();
            serviceCollection.AddTransient<MainForm>();

            return serviceCollection.GenerateContainer();
        }

        static MainForm GetMainForm()
        {
            return InitializeContainer().Get<MainForm>();
        }
    }
}
