
namespace Virus3spread;

internal static class Program
{

    // Object holds global App Settings
    // public static AppSettings Conf = new();
    
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]

    static void Main()
    {

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.            
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}