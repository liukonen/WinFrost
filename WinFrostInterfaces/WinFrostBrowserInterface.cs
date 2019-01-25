using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winFrost.winFrostInterface
{
   public interface WinFrostBrowserInterface
    {
        string LoadUrl{ set; }
        System.Windows.Forms.ToolStripItem[] BrowserSettings { get; }
        System.Windows.Forms.Form Browser { get; }
        string License { get; }
        string Name { get; }
        string version { get; }
    }

}
