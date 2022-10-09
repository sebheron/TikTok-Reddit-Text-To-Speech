using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTextToSpeech.UI.Services
{
    public class FileService : IFileService
    {
        private readonly string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RedditTextToSpeech");

        public string FolderPath
        {
            get
            {
                if (!Directory.Exists(this.folderPath)) Directory.CreateDirectory(this.folderPath);
                return this.folderPath;
            }
        }
    }
}
