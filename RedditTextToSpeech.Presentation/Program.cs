using PowerArgs;
using RedditTextToSpeech.Core;
using RedditTextToSpeech.Logic;
using RedditTextToSpeech.Logic.Services;
using RedditTextToSpeech.Presentation;
using System.Diagnostics;


ISpeechSynthesisService GetSpeechSynth(string? server, string? key, string config)
{
    try
    {
        if (server != null && key != null)
        {
            File.WriteAllLines(config, new string[] {server, key});
            return new AzureSpeechSynthesisService(server, key);
        }
        else if (File.Exists(config))
        {
            var lines = File.ReadAllLines(config);
            return new AzureSpeechSynthesisService(lines[0], lines[1]);
        }
    }
    catch (Exception)
    {
        File.Delete(config);
        Console.WriteLine("Azure specification is invalid. Defaulting to built-in windows TTS.");
    }
    return new WindowsSpeechSynthesisService();
}

try
{
    var parsed = Args.Parse<Arguments>(args);

    var gender = parsed.Gender ?? Gender.Male;
    var start = parsed.Start ?? TimeSpan.Zero;
    var output = parsed.Output ?? String.Empty;
    var comments = parsed.Comments ?? 0;
    var alternate = parsed.Alternate ?? false;

    var imageService = new ImageService(400);
    var videoService = new FFMPEGService();
    var redditService = new RedditService();
    var speechService = GetSpeechSynth(parsed.Server, parsed.Key, "azure.config");

    var videoGenerator = new RedditVideoGenerator(videoService, imageService, speechService, redditService);

    var video = string.Empty;
    if (comments <= 0)
    {
        video = await videoGenerator.GenerateVideo(parsed.Url, parsed.Background, output, gender, start);
    }
    else
    {
        video = await videoGenerator.GenerateVideo(parsed.Url, parsed.Background, output, gender, start, comments, alternate);
    }
    if (video != string.Empty)  new Process() { StartInfo = new ProcessStartInfo(video) { UseShellExecute = true } }.Start();
}
catch (ArgException ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine(ArgUsage.GenerateUsageFromTemplate<Arguments>());
}