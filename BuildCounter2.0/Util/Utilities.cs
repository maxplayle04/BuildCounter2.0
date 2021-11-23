using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BuildCounter2._0.Util
{
    internal class Utilities
    {

        static Utilities u;
        internal static Utilities Instance => u ??= new Utilities();

        /// <summary>
        /// Run the code given in <paramref name="a"/> on the WPF GUI thread. (Useful for things where a Background Worker cannot be used, yet GUI changes need to take place)
        /// </summary>
        /// <param name="a">The deletgate/method to run on the GUI thread.</param>
        internal void RunOnGuiThread(Action a) => Application.Current.Dispatcher.Invoke(a);
        /// <summary>
        /// Run the code given in <paramref name="a"/> on the WPF GUI thread without blocking the calling thread. (Useful for things where a Background Worker cannot be used, yet GUI changes need to take place)
        /// </summary>
        /// <param name="a">The deletgate/method to run on the GUI thread.</param>
        internal void RunOnGuiThreadAsync(Action a) => Application.Current.Dispatcher.InvokeAsync(a);

        public ZipFiles ZipFiles = new();
        public FileManagement FileManagement = new();

    }

    internal class FileManagement
    {

        const string possibleChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_.";
        static Random rnd = new();

        public string GetRandomValidFileName(int length)
        {
            string result = "";
            for (int i = 0; i < length; i++)
                result += possibleChars[rnd.Next(possibleChars.Length - 1)];
            return result;
        }

        /// <summary>
        /// Get a unique, completely random valid file name.
        /// </summary>
        /// <param name="noClashDirectory">The directory at this location will be scanned to prevent duplicate file names.</param>
        /// <returns></returns>
        public string GetRandomValidFileName(int length, string noClashDirectory)
        {
            string? attempt;
            while (true)
            {
                // Get a new value for attempt
                attempt = GetRandomValidFileName(length);

                // Create a new directory to check the contents of
                DirectoryInfo dir = new DirectoryInfo(noClashDirectory);

                // Check that directory exists first, if it does
                if (!dir.Exists)
                    throw new ArgumentException(
                        "The directory passed in the noClashDirectory argument does not exist.",
                        nameof(noClashDirectory),
                        new DirectoryNotFoundException($"The directory \"${noClashDirectory}\" could not be found.")
                      );

                // If no file exists within that directory, return the file name.
                if (!dir.GetFiles().Any(f => f.Name == attempt))
                    return attempt;

                // By doing this, C# flags object "dir" for garbage collection.
                dir = null;
            }
        }

        public string GetRandomDirectoryName(int length, string noClashDirectory)
        {
            string? attempt;
            while (true)
            {
                // Get a new value for attempt
                attempt = GetRandomValidFileName(length);

                // Create a new directory to check the contents of
                DirectoryInfo dir = new DirectoryInfo(noClashDirectory);

                // Check that directory exists first, if it does
                if (!dir.Exists)
                    throw new ArgumentException(
                        "The directory passed in the noClashDirectory argument does not exist.",
                        nameof(noClashDirectory),
                        new DirectoryNotFoundException($"The directory \"${noClashDirectory}\" could not be found.")
                      );

                // If no directory exists within that directory, return the directory name.
                if (!dir.GetDirectories().Any(f => f.Name == attempt))
                    return attempt;

                // By doing this, C# flags object "dir" for garbage collection.
                dir = null;
            }
        }
    }

    internal class ZipFiles
    {
        /// <summary>
        /// Create a compressed ZIP folder from the given directory.
        /// </summary>
        /// <param name="outputDirectory">The directory to put the outputted ZIP file within.</param>
        /// <param name="outputFileName">What the name of the outputted ZIP file shoud be.</param>
        /// <param name="directoryPathToZip">The path of the directory you would like to turn into a ZIP folder.</param>
        public void CreateZipFromDirectory(string outputDirectory, string outputFileName, string directoryPathToZip)
        {
            string tempZipLocation = Utilities.Instance.FileManagement.GetRandomValidFileName(10, Path.GetTempPath());

            ZipFile.CreateFromDirectory(directoryPathToZip, tempZipLocation, CompressionLevel.SmallestSize, false);


        }
    }
}
