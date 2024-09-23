using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace SyncFolders
{
    public partial class Main : Form
    {
        private string _sourceFolder = string.Empty;
        private string _replicaFolder = string.Empty;
        public Main()
        {
            InitializeComponent();
            _sourceFolder = Properties.Settings.Default.SourcePath;
            _replicaFolder = Properties.Settings.Default.ReplicaPath;
            textbox_SourcePath.Text = _sourceFolder;
            textbox_ReplicaPath.Text = _replicaFolder;
        }

        private void ToSystemTray()
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

        private void SetPath(bool _source)
        {
            using (var _FolderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult _result = _FolderBrowserDialog.ShowDialog();

                if (_result == DialogResult.OK && !string.IsNullOrEmpty(_FolderBrowserDialog.SelectedPath))
                {
                    if (_source)
                    {
                        textbox_SourcePath.Text = _FolderBrowserDialog.SelectedPath;
                        _sourceFolder = _FolderBrowserDialog.SelectedPath;
                        Properties.Settings.Default.SourcePath = _FolderBrowserDialog.SelectedPath;
                    }
                    else
                    {
                        textbox_ReplicaPath.Text = _FolderBrowserDialog.SelectedPath;
                        _replicaFolder = _FolderBrowserDialog.SelectedPath;
                        Properties.Settings.Default.ReplicaPath = _FolderBrowserDialog.SelectedPath;
                    }
                }
            }
        }

        /// <summary>
        /// NOT FINISHED
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_StartSync_Click(object sender, EventArgs e)
        {
            ToSystemTray();
            notifyIcon.ShowBalloonTip(500);
            var _sourceFiles = Directory.GetFiles(_sourceFolder);
            var _sourceDirectories = Directory.GetDirectories(_sourceFolder);
            foreach (var _fileName in _sourceDirectories)
            {
                MessageBox.Show(_fileName);
            }
        }

        private void button_StopSync_Click(object sender, EventArgs e)
        {

        }

        private void treToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToSystemTray();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ToSystemTray();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                ToSystemTray();
            }
            Properties.Settings.Default.Save();
        }

        private void button_SelectSource_Click(object sender, EventArgs e)
        {
            SetPath(true);

        }

        private void button_SelectReplica_Click(object sender, EventArgs e)
        {
            SetPath(false);
        }
    }
}
