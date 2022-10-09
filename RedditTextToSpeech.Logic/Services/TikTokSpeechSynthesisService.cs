using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedditTextToSpeech.Core.Content;
using RedditTextToSpeech.Logic.Containers;

namespace RedditTextToSpeech.Logic.Services
{
    public class TikTokSpeechSynthesisService : ISpeechSynthesisService
    {
        private readonly string proxy = "https://api.allorigins.win/raw?url=";
        private readonly string endpoint = "https://api16-normal-useast5.us.tiktokv.com/media/api/text/speech/invoke/";

        public string Extension => ".mp3";

        public IVoice[] Voices => new DefinedVoice[]
        {
            new DefinedVoice("Ghostface", "en_us_ghostface", Gender.Male, true),
            new DefinedVoice("Chewbacca", "en_us_chewbacca", Gender.Male, true),
            new DefinedVoice("C3PO", "en_us_c3po", Gender.Male, true),
            new DefinedVoice("Stitch", "en_us_stitch", Gender.Male, true),
            new DefinedVoice("Stormtrooper", "en_us_stormtrooper", Gender.Male, true),
            new DefinedVoice("Rocket", "en_us_rocket", Gender.Male, true),
            new DefinedVoice("Female Singing Warm Breeze", "en_female_f08_warmy_breeze", Gender.Male, true),
            new DefinedVoice("Female Singing Salut Damour", "en_female_f08_salut_damour", Gender.Male, true),
            new DefinedVoice("Male Singing Lobby", "en_male_m03_lobby", Gender.Male, true),
            new DefinedVoice("Male Singing Sunshine Soon", "en_male_m03_sunshine_soon", Gender.Male, true),
            new DefinedVoice("EN Female US 1", "en_us_001", Gender.Female),
            new DefinedVoice("EN Female AU 1", "en_au_001", Gender.Female),
            new DefinedVoice("Female DE 1", "de_001", Gender.Female),
            new DefinedVoice("Female BR 1", "br_001", Gender.Female),
            new DefinedVoice("Female BR 3", "br_003", Gender.Female),
            new DefinedVoice("Female BR 4", "br_004", Gender.Female),
            new DefinedVoice("Female ID 1", "id_001", Gender.Female),
            new DefinedVoice("Female JP 1", "jp_001", Gender.Female),
            new DefinedVoice("Female JP 3", "jp_003", Gender.Female),
            new DefinedVoice("Female JP 5", "jp_005", Gender.Female),
            new DefinedVoice("Female KR 3", "kr_003", Gender.Female),
            new DefinedVoice("EN Male US 6", "en_us_006", Gender.Male),
            new DefinedVoice("EN Male US 7", "en_us_007", Gender.Male),
            new DefinedVoice("EN Male US 9", "en_us_009", Gender.Male),
            new DefinedVoice("EN Male US 10", "en_us_010", Gender.Male),
            new DefinedVoice("EN Male US 1", "en_uk_001", Gender.Male),
            new DefinedVoice("EN Male US 3", "en_uk_003", Gender.Male),
            new DefinedVoice("EN Male AU 2", "en_au_002", Gender.Male),
            new DefinedVoice("Male FR 1", "fr_001", Gender.Male),
            new DefinedVoice("Male FR 2", "fr_002", Gender.Male),
            new DefinedVoice("Male DE 2", "de_002", Gender.Male),
            new DefinedVoice("Male ES 2", "es_002", Gender.Male),
            new DefinedVoice("Male ES MX 2", "es_mx_002", Gender.Male),
            new DefinedVoice("Male BR 5", "br_005", Gender.Male),
            new DefinedVoice("Male JP 6", "jp_006", Gender.Male),
            new DefinedVoice("Male KR 2", "kr_002", Gender.Male),
            new DefinedVoice("Male KR 4", "kr_004", Gender.Male),
        };

        public async Task<string> GetSound(string path, string voice, string text)
        {
            using (var client = new HttpClient())
            {
                var url = $"{endpoint}?text_speaker={voice}&req_text={Uri.EscapeDataString(text)}";
                var response = await client.PostAsync($"{proxy}{Uri.EscapeDataString(url)}", null);
                if (response.IsSuccessStatusCode)
                {
                    var vstr = (await response.Content.ReadAsStringAsync()).Split(new string[] { "v_str\":\"" }, StringSplitOptions.RemoveEmptyEntries)[1]
                        .Split(new string[] { "\"," }, StringSplitOptions.RemoveEmptyEntries)[0];
                    byte[] binaryData = Convert.FromBase64String(vstr);
                    File.WriteAllBytes(path, binaryData);
                }
            }
            return String.Empty;
        }
    }
}