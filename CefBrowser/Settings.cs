using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using winFrost.winFrostInterface;

namespace CefBrowser
{
    public class CefBrowser : WinFrostBrowserInterface
    {
        string url;
        public string License { get { return Properties.Resources.license; } }
        public string Name { get { return string.Format("CefSharp- {1} - {0}", CefSharp.Cef.CefVersion, CefSharp.Cef.ChromiumVersion); } }
        public string version { get { return CefSharp.Cef.ChromiumVersion; } }
        string WinFrostBrowserInterface.LoadUrl { set { url = value; } }
        ToolStripItem[] WinFrostBrowserInterface.BrowserSettings { get { return Items(); } }
        Form WinFrostBrowserInterface.Browser { get { return new Form1(url); } }



        private ToolStripItem[] Items()
        {
            List<ToolStripItem> items = new List<ToolStripItem>();
            ToolStripItem PluginSupport = new ToolStripMenuItem("Plugin Support") { Checked = WinFrostShared.Shared.PluginsAllowed, Size = new System.Drawing.Size(200, 22) };
            PluginSupport.Click += PluginSupport_Click;
            items.Add(PluginSupport);
            ToolStripItem DevTools = new ToolStripMenuItem("Show DevTools on Launch") { Checked = WinFrostShared.Shared.DevToolsOn, Size = new System.Drawing.Size(213, 22) };
            DevTools.Click += ShowDevTools_click;
            items.Add(DevTools);
            return items.ToArray();


        }

        private void PluginSupport_Click(object sender, EventArgs e)
        {
            Boolean Checked = WinFrostShared.Shared.PluginsAllowed;
            WinFrostShared.Shared.PluginsAllowed = (!Checked);
            ((ToolStripMenuItem)sender).Checked = (!Checked);
        }

        private void ShowDevTools_click(object sender, EventArgs e)
        {
            Boolean Checked = WinFrostShared.Shared.DevToolsOn;
            WinFrostShared.Shared.DevToolsOn = (!Checked);
            ((ToolStripMenuItem)sender).Checked = (!Checked);
        }

    }
}
