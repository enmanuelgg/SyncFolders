using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncFolders.Classes
{
    internal class SyncFile
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime LastWritenTime { get; set; }
        public FileInfo? SyncFileInfo { get; set; }
    }
}
