using System.Linq;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Services
{
    /// <summary>
    /// The windows speech synthesis service.
    /// </summary>
    public class WindowsSpeechSynthesisService : ISpeechSynthesisService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsSpeechSynthesisService"/> class.
        /// </summary>
        public WindowsSpeechSynthesisService()
        {
            using var synthesizer = this.GetSynth();
            var voices = synthesizer.GetInstalledVoices();
            this.MaleVoices = voices.Where(x => x.VoiceInfo.Gender == VoiceGender.Male).Select(x => x.VoiceInfo.Name).ToArray();
            this.FemaleVoices = voices.Where(x => x.VoiceInfo.Gender == VoiceGender.Female).Select(x => x.VoiceInfo.Name).ToArray();
        }

        /// <summary>
        /// Gets the file extension.
        /// </summary>
        public string Extension => ".wav";

        /// <summary>
        /// Gets the female voices.
        /// </summary>
        public string[] FemaleVoices { get; }

        /// <summary>
        /// Gets the male voices.
        /// </summary>
        public string[] MaleVoices { get; }

        /// <summary>
        /// Gets the sound.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="voice">The voice to use.</param>
        /// <param name="text">The text.</param>
        /// <returns>Awaitable task returning string.</returns>
        public async Task<string> GetSound(string path, string voice, string text)
        {
            await Task.Run(() =>
            {
                using var synthesizer = this.GetSynth();
                synthesizer.SetOutputToWaveFile(path + this.Extension,
                  new SpeechAudioFormatInfo(32000, AudioBitsPerSample.Sixteen, AudioChannel.Mono));
                synthesizer.SelectVoice(voice);
                synthesizer.Speak(text);
            });
            return path + this.Extension;
        }

        /// <summary>
        /// Gets the speech synth.
        /// </summary>
        /// <returns>A fresh instance of a speech synthesizer.</returns>
        private SpeechSynthesizer GetSynth()
        {
            var synth = new SpeechSynthesizer();
            synth.Rate = 2;
            return synth;
        }
    }
}