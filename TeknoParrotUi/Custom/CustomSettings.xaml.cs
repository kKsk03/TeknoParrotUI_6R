using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TeknoParrotUi.Helpers;

namespace TeknoParrotUi.Custom
{
    /// <summary>
    /// Interaction logic for CustomSettings.xaml
    /// </summary>
    public partial class CustomSettings : Window
    {
        public static void SaveSettings(Settings s)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(s, typeof(Settings), null);
            File.WriteAllText("CustomSettings.json", json);
        }

        public static Settings LoadSettings()
        {
            var f = File.ReadAllText("CustomSettings.json");
            var s = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(f);
            return s;
        }

        public CustomSettings()
        {
            Settings settings;

            var currentDir = Environment.CurrentDirectory;
            var curdirJoin = System.IO.Path.Combine(currentDir, "OpenParrotLoader");
            if (!File.Exists("CustomSettings.json"))
            {
                settings = new Settings
                {
                    OpenParrotPath = curdirJoin,
                };
                SaveSettings(settings);
            } else
            {
                settings = LoadSettings();
            }

            InitializeComponent();

            OpenParrotDirectory.Text = settings.OpenParrotPath;
        }

        // "Save settings"
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var settings = new Settings
            {
                OpenParrotPath = OpenParrotDirectory.Text,
            };
            SaveSettings(settings);
            MessageBoxHelper.InfoOK("Settings have been saved. (Ensure it is a valid path.)");

            Close();
        }
    }
}
