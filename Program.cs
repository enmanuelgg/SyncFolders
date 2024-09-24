namespace SyncFolders
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            string _sourceFolder = string.Empty;
            string _replicaFolder = string.Empty;
            string _logFolder = string.Empty;
            int _interval = 10000; //Timer interval by default 10 seconds
            foreach (string _arg in args)
            {
                string[] _parts = _arg.Split('=');
                if (_parts.Length == 2)
                {

                    switch (_parts[0])
                    {
                        case "source":
                            _sourceFolder = _parts[1];
                            break;
                        case "replica":
                            _replicaFolder = _parts[1];
                            break;
                        case "log":
                            _logFolder = _parts[1];
                            break;
                        case "interval":
                            int.TryParse(_parts[1], out _interval);
                            break;
                        default:
                            break;
                    }
                }
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new Main(_sourceFolder, _replicaFolder, _logFolder, _interval));
        }
    }
}