using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RedditTextToSpeech.Logic.Services
{
    public class AzureSpeechSynthesisService : ISpeechSynthesisService
    {
        private IAzureAuthenticationService auth;
        private string? accessToken;

        public AzureSpeechSynthesisService(string server, string key)
        {
            this.auth = new AzureAuthenticationService($"https://{server}.api.cognitive.microsoft.com/sts/v1.0/issuetoken", key);
        }

        /// <summary>
        /// Random selection of english speaking male voices.
        /// </summary>
        public string[] MaleVoices => new string[]
        {
            "en-AU-WilliamNeural",
            "en-CA-LiamNeural",
            "en-IE-ConnorNeural",
            "en-NZ-MitchellNeural",
            "en-GB-RyanNeural",
            "en-US-BrandonNeural",
            "en-US-ChristopherNeural",
            "en-US-EricNeural",
            "en-US-GuyNeural",
            "en-US-JacobNeural",
            "en-GB-AlfieNeural"
        };

        /// <summary>
        /// Random selection of english speaking female voices.
        /// </summary>
        public string[] FemaleVoices => new string[] {
            "en-IE-EmilyNeural",
            "en-NZ-MollyNeural",
            "en-GB-LibbyNeural",
            "en-GB-SoniaNeural",
            "en-US-AmberNeural",
            "en-US-AriaNeural",
            "en-US-AshleyNeural",
            "en-US-CoraNeural",
            "en-US-ElizabethNeural",
            "en-US-JennyNeural",
            "en-US-MichelleNeural",
            "en-US-MonicaNeural",
            "en-US-SaraNeural",
            "en-AU-NatashaNeural",
            "en-CA-ClaraNeural"
        };

        public string Extension => ".wav";

        public async Task<string> GetSound(string path, string voice, string text)
        {
            var body = new XDocument(
            new XElement("speak",
                new XAttribute("version", "1.0"),
                new XAttribute(XNamespace.Xml + "lang", "en-US"),
                new XElement("voice",
                    new XAttribute(XNamespace.Xml + "lang", "en-US"),
                    new XAttribute("name", voice),
                    new XElement("prosody",
                        new XAttribute("rate", "25%"), text))));

            if (accessToken == null)
            {
                accessToken = await auth.FetchTokenAsync().ConfigureAwait(false);
            }

            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage())
                {
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri("https://westeurope.tts.speech.microsoft.com/cognitiveservices/v1");
                    request.Content = new StringContent(body.ToString(), Encoding.UTF8, "application/ssml+xml");
                    request.Headers.Add("Authorization", "Bearer " + accessToken);
                    request.Headers.Add("Connection", "Keep-Alive");
                    request.Headers.Add("User-Agent", "RedditTTS");
                    request.Headers.Add("X-Microsoft-OutputFormat", "riff-24khz-16bit-mono-pcm");
                    using (var response = await client.SendAsync(request).ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();
                        using (var dataStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            using (var fileStream = new FileStream(path + this.Extension, FileMode.Create, FileAccess.Write, FileShare.Write))
                            {
                                await dataStream.CopyToAsync(fileStream).ConfigureAwait(false);
                                fileStream.Close();
                            }
                        }
                    }
                }
            }
            return path + this.Extension;
        }
    }
}
