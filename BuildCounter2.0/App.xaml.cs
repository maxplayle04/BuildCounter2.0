using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BuildCounter2._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// This method is called when the application starts. We're using the bog-standard C# method rather than using a StartupUri 
        /// incase we want to add any additional thread code or anything later down the line.
        /// </summary>
        private void OnApplicationStart(object sender, StartupEventArgs e)
        {

            MainWindow mw = new MainWindow();
            mw.Show();

            Current.MainWindow = mw;
        }
    }
}
