using System;
using System.Collections.Generic;
using System.Text;

namespace RedditTextToSpeech.Core
{
    public class FilePath
    {
        /// <summary>
        /// Basic filepath container.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public FilePath(string path)
        {
            Path = path;
        }

        /// <summary>
        /// The path to the file.
        /// </summary>
        public string Path { get; }
    }
}
