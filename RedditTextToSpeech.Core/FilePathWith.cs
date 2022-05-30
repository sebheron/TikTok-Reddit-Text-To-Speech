using System;
using System.Collections.Generic;
using System.Text;

namespace RedditTextToSpeech.Core
{
    public class FilePathWith<T> : FilePath where T : IComparable
    {
        /// <summary>
        /// File path container with generic object.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="value">The additional value.</param>
        public FilePathWith(string path, T value) : base(path)
        {
            this.Value = value;
        }

        /// <summary>
        /// Additional value stored.
        /// </summary>
        public T Value { get; }
    }
}
