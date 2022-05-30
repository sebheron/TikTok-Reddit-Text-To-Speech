using RedditTextToSpeech.Core;
using System.Globalization;
using System.Linq;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Threading.Tasks;

namespace RedditTextToSpeech.Logic.Services
{
    public class BuiltInSpeechSynthesisService : ISpeechSynthesisService
    {
        private SpeechSynthesizer synthesizer;

        public BuiltInSpeechSynthesisService()
        {
            this.synthesizer = new SpeechSynthesizer();
            var voices = this.synthesizer.GetInstalledVoices();
            this.MaleVoices = voices.Where(x => x.VoiceInfo.Gender == VoiceGender.Male).Select(x => x.VoiceInfo.Name).ToArray();
            this.FemaleVoices = voices.Where(x => x.VoiceInfo.Gender == VoiceGender.Female).Select(x => x.VoiceInfo.Name).ToArray();
        }

        public string Extension => ".wav";

        public string[] MaleVoices { get; }

        public string[] FemaleVoices { get; }

        public async Task<string> GetSound(string path, string voice, string text)
        {
            await Task.Run(() => {
                synthesizer.SetOutputToWaveFile(path + this.Extension,
                  new SpeechAudioFormatInfo(32000, AudioBitsPerSample.Sixteen, AudioChannel.Mono));
                synthesizer.SelectVoice(voice);
                synthesizer.Speak(text);
            });
            return path + this.Extension;
        }
    }
}
