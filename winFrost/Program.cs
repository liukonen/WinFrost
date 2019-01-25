using System;
using System.Linq;
using System.Windows.Forms;
using winFrost.winFrostInterface;

namespace winFrost
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            System.Reflection.Assembly browser = System.Reflection.Assembly.Load(System.IO.File.ReadAllBytes(WinFrostShared.Shared.BrowserDll));
            Type WinFrostType = (from xx in browser.GetExportedTypes() where xx.GetInterface("WinFrostBrowserInterface") != null select xx).First();
            dynamic WinItem = Activator.CreateInstance(WinFrostType);
            WinFrostBrowserInterface I = (WinFrostBrowserInterface)WinItem;
            if (args.Length > 0) { I.LoadUrl = args[0]; Application.Run(I.Browser); }
            else { Application.Run(new SettingsForm(I)); }
        }
    }
}
