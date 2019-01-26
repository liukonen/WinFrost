using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeckoBrowser
{
    public class GeckoBrowser : winFrost.winFrostInterface.WinFrostBrowserInterface
    {
        const string vsn = "60.0.26";
        private string url;
        public string LoadUrl { set { url = value; } }

        public ToolStripItem[] BrowserSettings { get { return new ToolStripItem[] { }; } }

        public Form Browser {get{return new Form1(url); } }

        public string License { get { return Properties.Resources.License; } }

        public string Name { get { return "Gecko Browser - " + vsn; } }

        public string version { get { return vsn; } }//=> throw new NotImplementedException();
    }
}
