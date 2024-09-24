using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncFolders.Classes
{
    internal class SyncFunctions
    {
        public string _sourceFolder { get; set; }
        public string _replicaFolder { get; set; }
        private Logger _logger;
        public SyncFunctions(string _sourceFolder, string _replicaFolder, Logger logger)
        {
            this._sourceFolder = _sourceFolder;
            this._replicaFolder = _replicaFolder;
            this._logger = logger;
        }

        public void Synchronize()
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

        /// <summary>
        /// Get all the sub directories of the _path and then save the sub directories in the _listFiles list if not exist in the list
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_directoryList"></param>
        private void GetDirectories(string _path, List<string> _directoryList)
        {
            var _directories = Directory.GetDirectories(_path);
            foreach (var _item in _directories)
            {
                if (!_directoryList.Exists(x => x.Equals(_item)))
                {
                    _directoryList.Add(_item);
                }
                //Check if the directory have sub directories until there are no more sub directories
                var _subDirectories = Directory.GetDirectories(_item);
                if (_subDirectories.Any())
                {
                    GetDirectories(_item, _directoryList);
                }
            }
        }

        /// <summary>
        /// Search every directory in the provided directory list and execute the function to save all the files inside the directories 
        /// to the List of files provided
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_pathDirectoryList"></param>
        /// <param name="_listFiles"></param>
        private void GetFilesList(string _path, List<string> _pathDirectoryList, List<SyncFile> _listFiles)
        {
            //First save the files of the main folder because it is not included into the _pathDirectoryList
            SaveFileToList(_path, _listFiles);

            //Save the files of each directory in the list
            foreach (var _dir in _pathDirectoryList)
            {
                SaveFileToList(_dir, _listFiles);
            }
        }

        /// <summary>
        /// Get all the files of the suppied _path and then save the file path in the provided list of files if not exist in the list
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_listFiles"></param>
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

        /// <summary>
        /// Method to check the directories of the source folder and create the ones that not exist in the replica folder
        /// and theck the methods that don't exist anymore in source to delete it from the replica folder
        /// </summary>
        /// <param name="_sourceDirectories"></param>
        /// <param name="_replicaDirectories"></param>
        private void SyncDirectories(List<string> _sourceDirectories, List<string> _replicaDirectories)
        {
            //Check the directories from source folder and create it on replica folder
            foreach (var _dir in _sourceDirectories)
            {
                if (!_replicaDirectories.Exists(x => x.Equals(_dir.Replace(_sourceFolder, _replicaFolder))))
                {
                    var _result = Directory.CreateDirectory(_dir.Replace(_sourceFolder, _replicaFolder));
                    _replicaDirectories.Add(_dir.Replace(_sourceFolder, _replicaFolder));
                    _logger.Log(string.Format("Folder Created: {0}", _result.FullName));
                }
            }

            //Check the directories that not exist in source folder to delete it from replica folder
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
                            _logger.Log(string.Format("Folder Deleted: {0}", _dirInfo.FullName));
                        }
                        catch (IOException _ex)
                        {
                            Console.WriteLine(_ex.Message);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Method that check all the files of the _sourceFiles List to create the ones that not exist in _replicaFiles and check
        /// the files of the _replicaFiles List to delete the ones that not exist in _sourceFiles
        /// </summary>
        /// <param name="_sourceFiles"></param>
        /// <param name="_replicaFiles"></param>
        private void SyncFiles(List<SyncFile> _sourceFiles, List<SyncFile> _replicaFiles)
        {
            foreach (var _file in _sourceFiles)
            {
                //Verify and copy the file to the replica folder if not exist
                if (!_replicaFiles.Exists(x => x.FullName.Equals(_file.FullName.Replace(_sourceFolder, _replicaFolder))))
                {
                    var _result = _file.SyncFileInfo.CopyTo(_file.FullName.Replace(_sourceFolder, _replicaFolder));
                    _logger.Log(string.Format("File '{0}' created in: {1}", _result.Name, _result.FullName));
                }
                else
                {
                    //Verify if the file was modified then copy the new version to the replica folder
                    var _existingFile = _replicaFiles.FirstOrDefault(x => x.FullName.Equals(_file.FullName.Replace(_sourceFolder, _replicaFolder)));
                    if (_file.LastWritenTime > _existingFile.LastWritenTime)
                    {
                        var _result = _file.SyncFileInfo.CopyTo(_file.FullName.Replace(_sourceFolder, _replicaFolder), true);
                        _logger.Log(string.Format("New version of '{0}' copied to: {1}", _result.Name, _result.FullName));
                    }
                }
            }

            //Verify if the file in the replica folder don't exist anymore in the source folder and delete it from the replica
            foreach (var _file in _replicaFiles)
            {
                if (!_sourceFiles.Exists(x => x.FullName.Equals(_file.FullName.Replace(_replicaFolder, _sourceFolder))))
                {
                    try
                    {
                        _file.SyncFileInfo.Delete();
                        _logger.Log(string.Format("File deleted from: {0}", _file.FullName));
                    }
                    catch (IOException _ex)
                    {
                        Console.WriteLine(_ex.Message);
                    }
                }
            }
        }

    }
}
