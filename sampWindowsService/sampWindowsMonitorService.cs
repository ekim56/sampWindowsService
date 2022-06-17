using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace sampWindowsService
{
    public partial class Service1 : ServiceBase
    {
        FileSystemWatcher fileSystemWatcher;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            fileSystemWatcher = new FileSystemWatcher("C:\\Folder2")
            {

                EnableRaisingEvents = true,
                IncludeSubdirectories = true

            };

            eventHandler();
        }

        private void DirectoryChanged(object sender, FileSystemEventArgs e)
        {
            var message = $"{e.ChangeType} -{e.FullPath}{System.Environment.NewLine}";
            File.AppendAllText(@"C:\sampWinSerLog\log.txt", message);
        }


        private void eventHandler()
        {
            fileSystemWatcher.Created += DirectoryChanged;
            fileSystemWatcher.Deleted += DirectoryChanged;
            fileSystemWatcher.Changed += DirectoryChanged;
            fileSystemWatcher.Renamed += DirectoryChanged;

        }
        protected override void OnStop()
        {

        }
    }
}
