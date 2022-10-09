using RedditTextToSpeech.Logic.Services;
using Windows.Media.SpeechSynthesis;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using Windows.Storage.Streams;
using RedditTextToSpeech.Logic.Containers;
using RedditTextToSpeech.UI.Containers;

namespace RedditTextToSpeech.UI.Services
{
    public class OfflineSpeechSynthesisService : ISpeechSynthesisService
    {
        private SpeechSynthesizer sythesizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="OfflineSpeechSynthesisService"/> class.
        /// </summary>
        public OfflineSpeechSynthesisService()
        {
            this.Voices = SpeechSynthesizer.AllVoices.Select(x => new OfflineVoice(x)).ToArray();
        }

        /// <summary>
        /// Gets the file extension.
        /// </summary>
        public string Extension => ".wav";

        /// <summary>
        /// Gets the voices.
        /// </summary>
        public IVoice[] Voices { get; }

        /// <summary>
        /// Gets the sound.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="voice">The voice to use.</param>
        /// <param name="text">The text.</param>
        /// <returns>Awaitable task returning path.</returns>
        public async Task<string> GetSound(string path, string voice, string text)
        {
            this.sythesizer = this.GetSynth();
            var offlineVoice = this.Voices.FirstOrDefault(x => x.Name == voice) as OfflineVoice;
            if (offlineVoice == null) throw new ArgumentException("Voice does not exist");
            this.sythesizer.Voice = offlineVoice.Voice;
            var folder = StorageFolder.GetFolderFromPathAsync(Path.GetDirectoryName(path));
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(Path.GetFileName(path), CreationCollisionOption.ReplaceExisting);
            var stream = await this.sythesizer.SynthesizeSsmlToStreamAsync(text);
            using (var reader = new DataReader(stream))
            {
                await reader.LoadAsync((uint)stream.Size);
                IBuffer buffer = reader.ReadBuffer((uint)stream.Size);
                await FileIO.WriteBufferAsync(file, buffer);
            }
            return file.Path;
        }

        /// <summary>
        /// Gets the speech synth.
        /// </summary>
        /// <returns>A fresh instance of a speech synthesizer.</returns>
        private SpeechSynthesizer GetSynth()
        {
            var synth = new SpeechSynthesizer();
            synth.Options.SpeakingRate = 2;
            return synth;
        }
    }
}
