using System;
using System.Windows;

namespace Pim_Desktop
{
    public partial class App : Application
    {
        public string SelectedTheme
        {
            get => Properties["SelectedTheme"] as string ?? "Dark";
            set
            {
                Properties["SelectedTheme"] = value;
                ChangeTheme();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ChangeTheme();
        }

        private void ChangeTheme()
        {
            var themeUri = new Uri($"Theme/{SelectedTheme}.xaml", UriKind.Relative);
            var newTheme = new ResourceDictionary { Source = themeUri };
            Current.Resources.MergedDictionaries.Clear();
            Current.Resources.MergedDictionaries.Add(newTheme);
        }
    }
}





