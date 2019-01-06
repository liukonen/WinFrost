using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using System.IO;

namespace winFrost
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private Icon ActiveBitmap;

        private void exitToolStripMenuItem2_Click(object sender, EventArgs e) { this.Close();}

        private void enableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shared.PluginsAllowed = true;
            SetPluginsDisplay(true);
        }

        private void disableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shared.PluginsAllowed = false;
            SetPluginsDisplay(false);
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            SetPluginsDisplay(Shared.PluginsAllowed);
            SetCacheDisplay(Shared.CacheEnabled);
        }



        private void SetPluginsDisplay(Boolean Active)
        {
            enableToolStripMenuItem.Checked = Active;
            disableToolStripMenuItem.Checked = !Active;
            showDevToolsOnLaunchToolStripMenuItem.Checked = Shared.DevToolsOn;
        }

        private void SetCacheDisplay(Boolean Active)
        {
            enableToolStripMenuItem1.Checked = Active;
            disableToolStripMenuItem1.Checked = !Active;
            locationToolStripMenuItem.Enabled = Active;
        }

        private void enableToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Shared.CacheEnabled = true;
            SetCacheDisplay(true);
        }

        private void locationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    Shared.CacheLocation = fbd.SelectedPath;
                }
            }
        }

        private void disableToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Shared.CacheEnabled = false;
            SetCacheDisplay(false);
        }

        private void Auto_Click(object sender, EventArgs e)
        {
            try
            {
                string response = string.Empty;
                System.Net.WebClient client = new System.Net.WebClient();
                System.IO.Stream stream = client.OpenRead(textBox1.Text);
                using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                {
                    response = reader.ReadToEnd();
                }
                string IconUrl = Shared.GetIconUrl(response, textBox1.Text);
                ActiveBitmap = Shared.GetUrlIcon(IconUrl);
                pictureBox1.Image = ActiveBitmap.ToBitmap();
                textBox2.Text = Shared.GetTitle(response);

            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        }

        private void showDevToolsOnLaunchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shared.DevToolsOn = !Shared.DevToolsOn;
            showDevToolsOnLaunchToolStripMenuItem.Checked = Shared.DevToolsOn;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = "c:\\",
                Filter = "jpeg file (*.jpg)|*.jpg|Portable Network Graphics (*.png)|*.png",
                RestoreDirectory = true
            })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ActiveBitmap = Icon.FromHandle((new Bitmap(openFileDialog.FileName)).GetHicon());
                    pictureBox1.Image = ActiveBitmap.ToBitmap();
                }
            
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox1.Text))
            { MessageBox.Show("Warning: not all values were filled out, please complete form and submit again."); }
            else
            {
                if (!checkBox2.Checked) { 
                using (FileStream s = new FileStream(Shared.GetIconLocation(textBox1.Text), FileMode.OpenOrCreate, FileAccess.Write))
                {
                    ActiveBitmap.Save(s);
                }
                }
                string ILocation = (System.IO.File.Exists(Shared.GetIconLocation(textBox1.Text))) ? Shared.GetIconLocation(textBox1.Text) : string.Empty;
                CreateIcon(textBox1.Text, textBox2.Text, ILocation);

            }




   
   
        }

        private void CreateIcon(string url, string name, string Iconlocation)
        {            
            var shell = new WshShell();

            var shortCutLinkFilePath = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), string.Format(@"\{0}.lnk", name));
            var windowsApplicationShortcut = (IWshShortcut)shell.CreateShortcut(shortCutLinkFilePath);
            //windowsApplicationShortcut.Description = "How to create short for application example";
            windowsApplicationShortcut.WorkingDirectory = Application.StartupPath;
            windowsApplicationShortcut.TargetPath = Application.ExecutablePath;
            windowsApplicationShortcut.Arguments = url;
            if (!string.IsNullOrWhiteSpace(Iconlocation)) { windowsApplicationShortcut.IconLocation = Iconlocation; }
            windowsApplicationShortcut.Save();
        }


    }
}
