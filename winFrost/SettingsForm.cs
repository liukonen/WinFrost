using System;
using System.Drawing;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using System.IO;

namespace winFrost
{
    public partial class SettingsForm : Form
    {
        winFrostInterface.WinFrostBrowserInterface Main;

        public SettingsForm(winFrostInterface.WinFrostBrowserInterface MainI)
        {
            Main = MainI;
            InitializeComponent();
        }

        private Icon ActiveBitmap;

        private void exitToolStripMenuItem2_Click(object sender, EventArgs e) { this.Close();}

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            this.Text = "WinFrost -" + Main.Name;
            miBrowserSettings.DropDownItems.AddRange(Main.BrowserSettings);
            SetCacheDisplay(global::WinFrostShared.Shared.CacheEnabled);
        }


        private void SetCacheDisplay(Boolean Active)
        {
            enableToolStripMenuItem1.Checked = Active;
            disableToolStripMenuItem1.Checked = !Active;
            locationToolStripMenuItem.Enabled = Active;
        }

        private void enableToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            global::WinFrostShared.Shared.CacheEnabled = true;
            SetCacheDisplay(true);
        }

        private void locationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    global::WinFrostShared.Shared.CacheLocation = fbd.SelectedPath;
                }
            }
        }

        private void disableToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            global::WinFrostShared.Shared.CacheEnabled = false;
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
                string IconUrl = global::WinFrostShared.Shared.GetIconUrl(response, textBox1.Text);
                ActiveBitmap = global::WinFrostShared.Shared.GetUrlIcon(IconUrl);
                pictureBox1.Image = ActiveBitmap.ToBitmap();
                textBox2.Text = global::WinFrostShared.Shared.GetTitle(response);

            }
            catch (Exception x) { MessageBox.Show(x.Message); }
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
                using (FileStream s = new FileStream(global::WinFrostShared.Shared.GetIconLocation(textBox1.Text), FileMode.OpenOrCreate, FileAccess.Write))
                {
                        ActiveBitmap.Save(s);
                }
                }
                string ILocation = (System.IO.File.Exists(global::WinFrostShared.Shared.GetIconLocation(textBox1.Text))) ? global::WinFrostShared.Shared.GetIconLocation(textBox1.Text) : string.Empty;
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

        private void cEFSharpEngineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Main.License);
        }

        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Properties.Resources.license);
        }

    }
}
