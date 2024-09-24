using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SyncFolders.Classes
{
    internal class Logger
    {
        private string _logFile;
        public Logger(string _path) 
        {
            //Check if the Path is empty or is valid if not then use the Local Application Data Folder by default
            if (!string.IsNullOrEmpty(_path) && Path.IsPathFullyQualified(_path))
            {
                _logFile = Path.Combine(_path, "log.txt");
            }
            else
            {
                _logFile = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "SyncFolders","log.txt");
            }
        }

        /// <summary>
        /// Method to write the log message to the Log.txt File and to the Console
        /// </summary>
        /// <param name="logMessage"></param>
        public void Log(string logMessage) 
        {
            using (StreamWriter w = File.AppendText(_logFile))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                w.WriteLine("  :");
                w.WriteLine($"  :{logMessage}");
                w.WriteLine("-------------------------------");
            }
            Console.WriteLine(string.Format("\r\nLog Entry {0}: {1}", DateTime.Now.ToLongTimeString(), logMessage));
        }
    }
}
