using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;

using EventLog = System.Diagnostics.EventLog;

if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    throw new PlatformNotSupportedException("This utility executable only works on Windows operating systems. Contact the developer for more information");

AppDomain.CurrentDomain.UnhandledException += (s, e) =>
{
    Console.WriteLine("--------------------- UNHANDLED EXCEPTION ---------------------");
    Console.WriteLine(e.ExceptionObject.ToString());
    Console.ReadLine();
};

WindowsPrincipal pricipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
bool hasAdministrativeRight = pricipal.IsInRole(WindowsBuiltInRole.Administrator);
if (!hasAdministrativeRight)
{
    // relaunch the application with admin rights
    string fileName = Assembly.GetExecutingAssembly().Location;
    ProcessStartInfo processInfo = new ProcessStartInfo();
    processInfo.Verb = "runas";
    processInfo.FileName = fileName;

    try
    {
        Process.Start(processInfo);
    }
    catch (Win32Exception)
    {
        throw new Exception("This process requires administrator privelages.");
    }

    return;
}

// Create the source
if (!EventLog.SourceExists("BuildCounter2"))
    EventLog.CreateEventSource("BuildCounter2", "Application");

Console.WriteLine("The event source was created successfully.");