using System;
using System.Collections.Generic;
using System.Text;

namespace RedditTextToSpeech.Core
{
    public class AudioImagePair
    {
        /// <summary>
        /// Creates an instance of audio image pair, used to collate audio and image paths.
        /// </summary>
        /// <param name="audioPath">The path to the audio file.</param>
        /// <param name="imagePath">The path to the image file.</param>
        public AudioImagePair(string audioPath, string imagePath)
        {
            this.AudioPath = audioPath;
            this.ImagePath = imagePath;
        }

        /// <summary>
        /// The path to the audio file.
        /// </summary>
        public string AudioPath { get; }

        /// <summary>
        /// The path to the image file.
        /// </summary>
        public string ImagePath { get; }
    }
}
