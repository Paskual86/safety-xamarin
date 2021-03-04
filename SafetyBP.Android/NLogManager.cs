using NLog;
using NLog.Config;
using NLog.Targets;
using SafetyBP.Core.Interfaces;
using SafetyBP.Droid;
using System;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(NLogManager))]
namespace SafetyBP.Droid
{
    public class NLogManager : ILogManager
    {
        public NLogManager()
        {
            var config = new LoggingConfiguration();

            var consoleTarget = new ConsoleTarget();
            config.AddTarget("console", consoleTarget);

            var consoleRule = new LoggingRule("*", LogLevel.Trace, consoleTarget);
            config.LoggingRules.Add(consoleRule);

            var fileTarget = new FileTarget();
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            fileTarget.FileName = Path.Combine(folder, "Log.txt");
            config.AddTarget("file", fileTarget);

            var fileRule = new LoggingRule("*", LogLevel.Warn, fileTarget);
            config.LoggingRules.Add(fileRule);

            LogManager.Configuration = config;
        }

        public Core.Interfaces.ILogger GetLog([System.Runtime.CompilerServices.CallerFilePath] string callerFilePath = "")
        {
            string fileName = callerFilePath;

            if (fileName.Contains("/"))
            {
                fileName = fileName.Substring(fileName.LastIndexOf("/", StringComparison.CurrentCultureIgnoreCase) + 1);
            }

            var logger = LogManager.GetLogger(fileName);
            return new NLogLogger(logger);
        }

    }
}