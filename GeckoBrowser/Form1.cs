using System;
using System.Windows.Forms;
using Gecko;
using System.IO;
using WinFrostShared;
using winFrost;
namespace GeckoBrowser
{

    public partial class Form1 : Form
    {
        string masterUrl;
        public Form1(string url)
        {
            masterUrl = url;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string Url = masterUrl;
            Xpcom.Initialize("Firefox64");
            var geckoWebBrowser = new GeckoWebBrowser { Dock = DockStyle.Fill };
            try
            {
                if (File.Exists(Shared.GetIconLocation(Url))) { this.Icon = Shared.GetUrlIcon(Shared.GetIconLocation(Url)); }
                else { this.Icon = Shared.GetUrlIcon(string.Format("https://www.google.com/s2/favicons?domain={0}", Shared.GetBaseUrlName(Url))); }
            }
            catch { }
            BrowserDock.Controls.Add(geckoWebBrowser);

            geckoWebBrowser.DocumentTitleChanged += new EventHandler(OnBrowserTitleChanged);
            geckoWebBrowser.Navigate(Url);


        }
       

        private void OnBrowserTitleChanged(object sender, EventArgs args)
        {
            this.Invoke((MethodInvoker)delegate { this.Text = ((GeckoWebBrowser)sender).DocumentTitle; });
        }




    }
}
