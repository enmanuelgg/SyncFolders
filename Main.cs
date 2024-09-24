using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;
using SyncFolders.Classes;

namespace SyncFolders
{
    public partial class Main : Form
    {
        Logger _logger;
        SyncFunctions _syncFunctions;

        private Timer _timer;
        public Main(string sourceFolder, string replicaFolder, string logFolder, int interval)
        {
            InitializeComponent();

            textbox_SourcePath.Text = CheckPath(sourceFolder);
            textbox_ReplicaPath.Text = CheckPath(replicaFolder);

            //Create Timer with specified interval in args
            _timer = new Timer();
            _timer.AutoReset = true;
            _timer.Interval = interval;
            _timer.Elapsed += (sender, e) => TimeElapsed();

            //Instanciate Logger
            _logger = new Logger(logFolder);
            //Instanciate Synchronization functions class
            _syncFunctions = new SyncFunctions(sourceFolder, replicaFolder, _logger);
        }

        /// <summary>
        /// This method is fired every time defined in the interval and synchronize the replica folder to match the source folder
        /// </summary>
        private void TimeElapsed()
        {
            _syncFunctions.Synchronize();
        }

        /// <summary>
        /// Hide the form to the system tray
        /// </summary>
        private void HideShowForm()
        {
            if (!this.Visible)
            {
                this.Show();
            }
            else if (this.Visible)
            {
                this.Hide();
            }
        }

        /// <summary>
        /// Verify if the string is a valid Path
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        private string CheckPath(string folderPath)
        {
            return !string.IsNullOrEmpty(folderPath) && Path.IsPathFullyQualified(folderPath)
                            ? folderPath
                            : string.Empty;
        }

        /// <summary>
        /// Method to set the source or replica path to the SyncFunction class and the textbox
        /// </summary>
        /// <param name="_source"></param>
        private void SetPath(bool _source)
        {
            using (var _FolderBrowserDialog = new FolderBrowserDialog())
            {
                //Open a dialog to chose the source or replica folder
                DialogResult _result = _FolderBrowserDialog.ShowDialog();

                //If the result is OK and the selected path if nor null or empty set the syncFunctions and the textbox path
                if (_result == DialogResult.OK && !string.IsNullOrEmpty(_FolderBrowserDialog.SelectedPath))
                {
                    if (_source)
                    {
                        _syncFunctions._sourceFolder = _FolderBrowserDialog.SelectedPath;
                        textbox_SourcePath.Text = _FolderBrowserDialog.SelectedPath;
                    }
                    else
                    {
                        _syncFunctions._replicaFolder = _FolderBrowserDialog.SelectedPath;
                        textbox_ReplicaPath.Text = _FolderBrowserDialog.SelectedPath;
                    }
                }
            }
        }

        private void button_StartSync_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_syncFunctions._sourceFolder) && !string.IsNullOrEmpty(_syncFunctions._replicaFolder))
            {
                HideShowForm();
                notifyIcon.ShowBalloonTip(500);
                _timer.Start();
            }
            else
            {
                MessageBox.Show("Please select a source and replica folder!");
            }

        }

        private void button_StopSync_Click(object sender, EventArgs e)
        {
            _timer.Stop();
        }

        private void treToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideShowForm();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HideShowForm();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                HideShowForm();
            }
            _timer.Stop();
            _timer.Dispose();
        }

        private void button_SelectSource_Click(object sender, EventArgs e)
        {
            SetPath(true);
        }

        private void button_SelectReplica_Click(object sender, EventArgs e)
        {
            SetPath(false);
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_syncFunctions._sourceFolder) && !string.IsNullOrEmpty(_syncFunctions._replicaFolder))
            {
                HideShowForm();
                notifyIcon.ShowBalloonTip(500);
                _timer.Start();
            }
        }
    }
}
