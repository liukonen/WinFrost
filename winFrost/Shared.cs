using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing;
using System.Net;
using System.IO; 

namespace winFrost
{
    internal static class Shared
    {
        public static string GetTitle(string html)
        {
            if (html.IndexOf("<title>", 0, StringComparison.OrdinalIgnoreCase) > 0)
            {
                html = html.Substring(html.IndexOf("<title>", StringComparison.OrdinalIgnoreCase) + 7);
                html = html.Substring(0, html.IndexOf("<"));
                return html;
            }
            return string.Empty;
        }


        public static string GetIconLocation(string url)
        {
                       
            return IconPath + "\\" + GetBaseUrlName(url) + ".ico";
        }

        public static string GetBaseUrlName(string url)
        {
            string Base = url;
            if (Base.Contains("/"))
            {

                if (Base.StartsWith("http")) { Base = Base.Substring(Base.IndexOf("//") + 2); }
                if (Base.Contains("/"))
                {
                    Base = Base.Substring(0, Base.IndexOf("/"));
                }


            }
            return Base;
        }

        public static string GetIconUrl(string html, string fallbackUrl)
        {
            const string Test = @"""shortcut icon"" href=""";
            const string Test2 = @"link rel=""icon"" href=""";

            string first;
            if (html.IndexOf(Test, StringComparison.OrdinalIgnoreCase) > 0)
            {
                first = html.Substring(html.IndexOf(Test, 0, StringComparison.OrdinalIgnoreCase) + Test.Length);
                return first.Substring(0, first.IndexOf(@""" "));
                
            }

            else if (html.IndexOf(Test2, StringComparison.OrdinalIgnoreCase) > 0)
            {
                first = html.Substring(html.IndexOf(Test2, 0, StringComparison.OrdinalIgnoreCase) + 22);
                return first.Substring(0, first.IndexOf(@""" "));
            }
            //If we can't get it from the HTML, fall back on Googles FavIcon cache
            string Base = GetBaseUrlName(fallbackUrl);
            return string.Format("https://www.google.com/s2/favicons?domain={0}", Base);

        }



        public static Icon GetUrlIcon(string IconLocation)
        {
            if (IconLocation.StartsWith(@"//")) { IconLocation = "https:" + IconLocation; } //gmail support
            WebClient wc = new WebClient();
            return Icon.FromHandle(GetBitmapIcon(IconLocation).GetHicon());
        }

        public static Bitmap GetBitmapIcon(string IconLocation)
        {


            if (IconLocation.StartsWith(@"//")) { IconLocation = "https:" + IconLocation; } //gmail support
            WebClient wc = new WebClient();
            return new Bitmap(Image.FromStream(new MemoryStream(wc.DownloadData(IconLocation))));

        }


        public static Boolean CacheEnabled
        {
            get {
                string Item = ReadSetting("CacheEnabled");
                if (!string.IsNullOrWhiteSpace(Item)){ return Boolean.Parse(Item); }
                return false;
            }
            set {
                AddUpdateAppSettings("CacheEnabled", value.ToString());
            }
        }

        public static Boolean DevToolsOn
        {
            get {
                string Item = ReadSetting("DevToolsEnabled");
                if (!string.IsNullOrWhiteSpace(Item)) { return Boolean.Parse(Item); }
                return false; 
            }
            set { AddUpdateAppSettings("DevToolsEnabled", value.ToString()); }
        }

        public static Boolean PluginsAllowed
        {
            get
            {
                string Item = ReadSetting("PluginsAllowed");
                if (!string.IsNullOrWhiteSpace(Item)) { return Boolean.Parse(Item); }
                return true; //by default, enabled
            }
            set { AddUpdateAppSettings("PluginsAllowed", value.ToString()); }
        }

        public static string CacheLocation
        {
            get
            {
                string Item = ReadSetting("CacheLocation");
                if (string.IsNullOrWhiteSpace(Item)) { return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\winFrost"; }
                return Item;
            }
            set { AddUpdateAppSettings("CacheLocation", value.ToString()); }
        }

        public static string DataPath { get { if (!System.IO.Directory.Exists(CacheLocation + @"\Data")) { System.IO.Directory.CreateDirectory(CacheLocation + @"\Icons"); } return CacheLocation + @"\Data"; } }

        public static string IconPath { get { if (!System.IO.Directory.Exists(CacheLocation + @"\Icons")) { System.IO.Directory.CreateDirectory(CacheLocation + @"\Icons"); }                return CacheLocation + @"\Icons"; } }

        //taken from MSDN https://msdn.microsoft.com/en-us/library/system.configuration.configurationmanager.aspx

        private static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null) { settings.Add(key, value); }
                else { settings[key].Value = value; }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("Error writing app settings", "Error Editing Config", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private static void RemoveAppSetting(string key)

        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings[key] != null)
            {
                settings.Remove(key);
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
        }

        private static string ReadSetting(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("Error reading app settings"); return string.Empty;
            }
        }
    }
}
