using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;
using SyncFolders.Classes;

namespace SyncFolders
{
    public partial class Main : Form
    {
        private string _sourceFolder = string.Empty;
        private string _replicaFolder = string.Empty;
        
        private Timer _timer;
        public Main()
        {
            InitializeComponent();
            _sourceFolder = Properties.Settings.Default.SourcePath;
            _replicaFolder = Properties.Settings.Default.ReplicaPath;
            textbox_SourcePath.Text = _sourceFolder;
            textbox_ReplicaPath.Text = _replicaFolder;
            _timer = new Timer();
            _timer.AutoReset = true;
            _timer.Interval = 5000;
            _timer.Elapsed += (sender, e) => Synchronize();
        }

        private void Synchronize()
        {
            List<string> _sourceDirectories = new List<string>();
            List<string> _replicaDirectories = new List<string>();
            List<SyncFile> _sourceFiles = new List<SyncFile>();
            List<SyncFile> _replicaFiles = new List<SyncFile>();

            GetDirectories(_sourceFolder, _sourceDirectories);
            GetDirectories(_replicaFolder, _replicaDirectories);
            GetFilesList(_sourceFolder, _sourceDirectories, _sourceFiles);
            GetFilesList(_replicaFolder, _replicaDirectories, _replicaFiles);
            SyncDirectories(_sourceDirectories, _replicaDirectories);
            SyncFiles(_sourceFiles, _replicaFiles);
        }

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

        private void GetDirectories(string _path, List<string> _directoryList)
        {
            var _directories = Directory.GetDirectories(_path);
            foreach (var _item in _directories)
            {
                if (!_directoryList.Exists(x => x.Equals(_item)))
                {
                    _directoryList.Add(_item);
                }
                var _subDirectories = Directory.GetDirectories(_item);
                if (_subDirectories.Any())
                {
                    GetDirectories(_item, _directoryList);
                }
            }
        }

        private void GetFilesList(string _path, List<string> _pathDirectoryList, List<SyncFile> _listFiles)
        {
            SaveFileToList(_path, _listFiles);

            foreach (var _dir in _pathDirectoryList)
            {
                SaveFileToList(_dir, _listFiles);
            }
        }

        private void SaveFileToList(string _path, List<SyncFile> _listFiles)
        {
            var _files = Directory.GetFiles(_path);
            foreach (var _file in _files)
            {
                FileInfo _fileInfo = new FileInfo(_file);
                if (!_listFiles.Exists(x => x.FullName.Equals(_file)))
                {
                    _listFiles.Add(new SyncFile() { FullName = _file, LastWritenTime = _fileInfo.LastWriteTime, SyncFileInfo = _fileInfo });
                }
            }
        }

        private void SyncDirectories(List<string> _sourceDirectories, List<string> _replicaDirectories)
        {
            foreach (var _dir in _sourceDirectories)
            {
                if (!_replicaDirectories.Exists(x => x.Equals(_dir)))
                {
                    Directory.CreateDirectory(_dir.Replace(_sourceFolder, _replicaFolder));
                    _replicaDirectories.Add(_dir.Replace(_sourceFolder, _replicaFolder));
                }
            }

            foreach (var _dir in _replicaDirectories)
            {
                if (!_sourceDirectories.Exists(x => x.Equals(_dir.Replace(_replicaFolder, _sourceFolder))))
                {
                    if (Directory.Exists(_dir))
                    {
                        try
                        {
                            var _dirInfo = new DirectoryInfo(_dir);
                            _dirInfo.Delete(true);
                        }
                        catch (IOException _ex)
                        {
                            Console.WriteLine(_ex.Message);
                        }
                    }
                }
            }
        }

        private void SyncFiles(List<SyncFile> _sourceFiles, List<SyncFile> _replicaFiles)
        {
            foreach (var _file in _sourceFiles)
            {
                if (!_replicaFiles.Exists(x => x.FullName.Equals(_file.FullName.Replace(_sourceFolder, _replicaFolder))))
                {
                    _file.SyncFileInfo.CopyTo(_file.FullName.Replace(_sourceFolder, _replicaFolder));
                } else
                {
                    var _existingFile = _replicaFiles.FirstOrDefault(x => x.FullName.Equals(_file.FullName.Replace(_sourceFolder, _replicaFolder)));
                    if (_file.LastWritenTime > _existingFile.LastWritenTime)
                    {
                        _file.SyncFileInfo.CopyTo(_file.FullName.Replace(_sourceFolder, _replicaFolder), true);
                    }
                }
            }

            foreach (var _file in _replicaFiles)
            {
                if (!_sourceFiles.Exists(x => x.FullName.Equals(_file.FullName.Replace(_replicaFolder, _sourceFolder))))
                {
                    try
                    {
                        _file.SyncFileInfo.Delete();
                    }
                    catch (IOException _ex)
                    {
                        Console.WriteLine(_ex.Message);
                    }
                }
            }
        }

        private void WriteToLog(string _message)
        {

        }

        /// <summary>
        /// NOT FINISHED
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_StartSync_Click(object sender, EventArgs e)
        {
            HideShowForm();
            notifyIcon.ShowBalloonTip(500);
            _timer.Start();
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
            Properties.Settings.Default.Save();
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
    }
}
