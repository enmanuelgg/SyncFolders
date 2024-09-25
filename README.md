# SyncFolders

**SyncFolders** is a C# program designed to synchronize the contents of two folders: a source folder and a replica folder. Synchronization is performed in one direction, ensuring that the replica folder's contents are modified to match the source folder exactly.

####   **Key Features**
- **One-way synchronization:** The replica folder is updated to mirror the source folder.
- **Customizable through command-line arguments:** You can provide source and replica paths, synchronization intervals, and logging directories.
- **Logging:** All actions (create/copy/remove) are logged in a text file for tracking changes.

####   **Command-Line Parameters**
You can pass the following arguments when executing the program via the command line, using key-value pairs:

- `source="Source Folder Path"`: The folder that serves as the source.

- `replica="Replica Folder Path"`: The folder that will be synchronized with the source.

- `log="Log Folder Path"`: The directory where the log file will be saved.

- `interval="Interval of synchronization in milliseconds""`: The time interval between synchronizations, specified in milliseconds.

#####     **Example Usage:**
`SyncFolders.exe source="C:\SourceFolder" replica="C:\ReplicaFolder" log="C:\LogFolder" interval=60000`

####   **User Interface**
If no command-line parameters are provided, you can define the settings via the program's user interface:

![MainForm](https://github.com/user-attachments/assets/2ef9c759-74d7-45e6-b0b8-24047b9563e5)

####   **Logging**
The program logs all operations (creation, copying, and removal) in a log.txt file at the specified log path. If no log path is provided via the command line, the default log location is within the Local Application Data Folder.

####   **System Tray Icon**
To close the application, use the system tray icon for easy access and control:

![systray](https://github.com/user-attachments/assets/e42a1d8c-4564-4af1-8762-ee681ee61a24)


####   **Contact**
José Enmanuel Gonçalves - https://www.linkedin.com/in/enmanuelgg/ - 1enmanueljose@gmail.com
