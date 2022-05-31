using RedditTextToSpeech.Core;
using RedditTextToSpeech.Logic;
using RedditTextToSpeech.Logic.Services;

var imageService = new ImageService(400);
var videoService = new FFMPEGService();
var redditService = new RedditService();
var speechService = new WindowsSpeechSynthesisService();

var videoGenerator = new RedditVideoGenerator(videoService, imageService, speechService, redditService);

await videoGenerator.GenerateVideo("https://www.reddit.com/r/AskReddit/comments/v0r9qs/what_are_we_living_in_the_golden_age_of/",
    "C:/Users/Sebhe/Downloads/input.mp4", Gender.Male, TimeSpan.Zero, 3);