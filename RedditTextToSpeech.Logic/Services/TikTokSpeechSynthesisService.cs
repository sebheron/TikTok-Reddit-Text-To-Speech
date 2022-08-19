using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RedditTextToSpeech.Logic.Services
{
    public class TikTokSpeechSynthesisService : ISpeechSynthesisService
    {
        private readonly string proxy = "https://api.allorigins.win/raw?url=";
        private readonly string endpoint = "https://api16-normal-useast5.us.tiktokv.com/media/api/text/speech/invoke/";

        public string Extension => ".mp3";

        public string[] FemaleVoices => new string[] {"en_us_001", "en_au_001", "de_001", "br_001", "br_003", "br_004", "id_001", "jp_001", "jp_003", "jp_005", "kr_003", "en_female_f08_salut_damour", "en_female_f08_warmy_breeze"};

        public string[] MaleVoices => new string[] {"en_us_006", "en_us_007", "en_us_009", "en_us_010", "en_uk_001", "en_uk_003", "en_au_002", "fr_001", "fr_002", "de_002", "es_002", "es_mx_002", "br_005", "jp_006", "kr_002", "kr_004", "en_male_m03_lobby", "en_male_m03_sunshine_soon", "en_us_ghostface", "en_us_chewbacca", "en_us_c3po", "en_us_stitch", "en_us_stormtrooper", "en_us_rocket"};

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