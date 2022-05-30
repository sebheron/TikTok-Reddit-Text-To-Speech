using RedditTextToSpeech.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Diagnostics;

namespace RedditTextToSpeech.Logic.Services
{
    public class FFMPEGService : IVideoService
    {
        public string Extension => ".mp4";

        public async Task<string> GetVideo(string path, IList<AudioImagePair> values, TimeSpan startTime, string background)
        {
            await Task.Run(() =>
            {
                var durations = new List<TimeSpan>();
                var totalDuration = TimeSpan.Zero;
                var count = values.Count;
                var tempPath = $"{Guid.NewGuid()}.wav";

                WaveFileWriter writer = null;
                byte[] buffer = new byte[1024];

                for (int i = 0; i < count; i++)
                {
                    var reader = new WaveFileReader(values[i].AudioPath);
                    if (writer == null) writer = new WaveFileWriter(tempPath, reader.WaveFormat);
                    int read;
                    while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        writer.Write(buffer, 0, read);
                    }
                    var readTime = reader.TotalTime;
                    if (i == count - 1)
                    {
                        readTime += TimeSpan.FromMilliseconds(500);
                    }
                    durations.Add(readTime);
                    totalDuration += readTime;
                    reader.Dispose();
                }
                writer?.Dispose();

                var inputs = new StringBuilder();
                var overlays = new StringBuilder();
                var time = TimeSpan.Zero;
                for (int i = 0; i < count; i++)
                {
                    inputs.Append($"-i {values[i].ImagePath} ");
                    overlays.Append($"[{i + 1}]");
                    overlays.Append(i > 0 ? $"[v{i - 1}]" : "[a]");
                    overlays.Append($"scale2ref=iw*0.9:ow/mdar[p{i}][s{i}];");
                    overlays.Append($"[s{i}][p{i}]overlay = (main_w-overlay_w)/2:(main_h-overlay_h)/2:enable = 'between(t,{time.TotalSeconds},{(time + durations[i]).TotalSeconds})'[v{i}]");
                    if (i < count - 1)
                    {
                        overlays.Append(";");
                    }
                    time += durations[i];
                }

                this.GetProcessOutput($"-y -ss {startTime} -t {totalDuration} -i \"{background}\" -c:v copy -c:a copy output{this.Extension}");
                this.GetProcessOutput($"-y -i output.mp4 {inputs} -i {tempPath} -vcodec libx265 -crf 30 -filter_complex \"[0:v]crop = ih*(1242/2688):ih[a];{overlays}\" -map {count + 1}:a -map \"[v{count - 1}]\" -tag:v hvc1 -preset fast -shortest \"{path}{this.Extension}\"");
            });
            return path;
        }

        private string GetProcessOutput(string args)
        {
            var p = new Process();
            p.StartInfo.FileName = "ffmpeg";
            p.StartInfo.Arguments = args;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return output;
        }
    }
}
