using BuildCounter2._0.Util;
using BuildCounter2._0.Util.SettingsStorage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Windows;

namespace BuildCounter2._0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string TemporaryFileFolder { get; private set; }

        /// <summary>
        /// This method is called when the application starts. We're using the bog-standard C# method rather than using a StartupUri 
        /// incase we want to add any additional thread code or anything later down the line.
        /// </summary>
        private void OnApplicationStart(object sender, StartupEventArgs e)
        {

            RunBeforeInitialWindowCreated();

            MainWindow mw = new MainWindow();
            mw.Show();
            Current.MainWindow = mw;
        }
        
        /// All code in this method is run before any window object is created within the application. 
        /// I suggest you run all initialisation code here.
        /// </summary>
        private void RunBeforeInitialWindowCreated()
        {
            // Priority 1
            HandleAppCrash();

            // Priorty 2
            SetupTemporaryFolder();        
        
            // Priority 3
        }

        private void HandleAppCrash()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var dr = MessageBox.Show("Unfortunately, BuildCounter 2.0 has crashed! Here's some more information that the developers of the software will find useful.\n\n" + e.ExceptionObject.ToString() + "\n\nWould you like to open the link to the bug reporting page?", "Aw, snap!", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (dr == MessageBoxResult.Yes)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "https://github.com/maxplayle04/buildcounter2.0/issues",
                        UseShellExecute = true
                    });
                    var copy = MessageBox.Show("Would you like to copy the information about the error to your clipboard, so you can paste it into a GitHub issue?", "Copy diagnostic information?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (copy == MessageBoxResult.Yes)
                        Clipboard.SetText(e.ExceptionObject.ToString());
                }
            };
        }
        
        
        private void SetupTemporaryFolder()
        {
            var tempFolderName = Utilities.Instance.FileManagement.GetRandomDirectoryName(10, Path.GetTempPath());
            string attemptCreatePath = Path.Join(Path.GetTempPath(), tempFolderName);
            
            Directory.CreateDirectory(attemptCreatePath);

            TemporaryFileFolder = attemptCreatePath;

          
        }
    }
}
