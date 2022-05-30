using RedditTextToSpeech.Logic;
using RedditTextToSpeech.Logic.Services;
using RedditTextToSpeech.Core;

var imageService = new ImageService(400);
var videoService = new FFMPEGService();
var redditService = new RedditService();
var speechService = new WindowsSpeechSynthesisService();

var videoGenerator = new RedditVideoGenerator(videoService, imageService, speechService, redditService);

await videoGenerator.GenerateVideo("https://www.reddit.com/r/AmItheAsshole/comments/v0yazc/aita_for_refusing_to_give_my_husband_my_new_wifi/",
    "C:/Users/Sebhe/Downloads/input.mp4", Gender.Male, TimeSpan.Zero);