using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pim_Desktop
{
    class AppTheme
    {
        public static void ChangeTheme(Uri themeUri)
        {
            ResourceDictionary newTheme = new ResourceDictionary() { Source = themeUri };

            var oldTheme = App.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Theme/"));

            if (oldTheme != null)
            {
                App.Current.Resources.MergedDictionaries.Remove(oldTheme);
            }

            App.Current.Resources.MergedDictionaries.Add(newTheme);
        }
    }
}

