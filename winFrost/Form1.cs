using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using CefSharp.WinForms;
using CefSharp;

using System.Threading.Tasks;

namespace winFrost
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            string[] args = System.Environment.GetCommandLineArgs();
            string Url = args[args.Length - 1];

            CefSettings settings = new CefSettings { CachePath = Shared.CacheLocation, UserDataPath = Shared.DataPath };

            string plugins = (Shared.PluginsAllowed) ? "1" : "0";
            
            settings.CefCommandLineArgs.Add("enable-npapi", plugins);
            settings.CefCommandLineArgs.Add("enable-widevine-cdm", plugins);
            CefSharp.Cef.Initialize(settings);


            ChromiumWebBrowser Browser = new ChromiumWebBrowser(Url) { Dock = DockStyle.Fill };

            if (File.Exists(Shared.GetIconLocation(Url))) { this.Icon = Shared.GetUrlIcon(Shared.GetIconLocation(Url)); }
            else { this.Icon = Shared.GetUrlIcon(string.Format("https://www.google.com/s2/favicons?domain={0}", Shared.GetBaseUrlName(Url))); }


            Browser.IsBrowserInitializedChanged += IsBrowserInitializedChanged;
            Browser.TitleChanged += OnBrowserTitleChanged;
                        Browser.FrameLoadEnd += FrameLoadEndEvent;
            this.Text = Browser.Text;
            BrowserDock.Controls.Add(Browser);
            //Browser.ShowDevTools();


            



        }

        private void FrameLoadEndEvent(object sender, FrameLoadEndEventArgs args)
        {
            const string Test = @"""shortcut icon"" href=""";
            const string Test2 = @"link rel=""icon"" href=""";
            Task<string> value = args.Frame.GetSourceAsync();
            if (args.Frame.IsMain)
            {
                args.Frame.GetSourceAsync().ContinueWith(taskHtml =>
                {
                    var html = taskHtml.Result;
                    string first;
                    if (html.IndexOf(Test, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        first = html.Substring(html.IndexOf(Test, 0, StringComparison.OrdinalIgnoreCase));
                        string value2 = first.Substring(0, first.IndexOf(@""" ") - 1);
                        this.Invoke((MethodInvoker)delegate { this.Icon =  Shared.GetUrlIcon(value2); });
                    }

                    else if (html.IndexOf(Test2, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        first = html.Substring(html.IndexOf(Test2, 0, StringComparison.OrdinalIgnoreCase) + 22);
                        string value2 = first.Substring(0, first.IndexOf(@""" "));
                        this.Invoke((MethodInvoker)delegate { this.Icon = Shared.GetUrlIcon(value2); });
                    }
                });
            }
        }


        private void IsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs args)
        {
            if (args.IsBrowserInitialized == true && Shared.DevToolsOn)
            {
                 ((ChromiumWebBrowser)sender).ShowDevTools();
            }
        }
        private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            this.Invoke((MethodInvoker)delegate { this.Text = args.Title; });
        }




    }
}
