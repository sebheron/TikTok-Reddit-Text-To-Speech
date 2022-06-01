# TikTok-Reddit-Text-To-Speech
![HitCount](https://hits.dwyl.com/sebheron/tiktok-reddit-text-to-speech.svg?style=flat)
![Downloads](https://img.shields.io/github/downloads/sebheron/tiktok-reddit-text-to-speech/total.svg)
![Stars](https://img.shields.io/github/stars/sebheron/tiktok-reddit-text-to-speech)
![Issues](https://img.shields.io/github/issues/sebheron/tiktok-reddit-text-to-speech)
![Forks](https://img.shields.io/github/forks/sebheron/tiktok-reddit-text-to-speech)

Application and library for generating TikTok videos from Reddit posts and comments.
The console application requires FFMPEG to run.

## Features
- Loads reddit information directly from URL.
- Synthesises speech (using Azure or built-in Windows voices)
- Builds images to represent parts of Reddit posts/comments.
- Compiles a video using FFMPEG (and possibly other methods further down the line).

## Examples
### Basic usage
This example generates a post video with a single voice, using the built-in Windows TTS service.

`ttsgen -url https://www.reddit.com/r/tifu/comments/v1qkbx/tifu_by_pointing_out_the_groundhog_that_lives_in/ -background input.mp4`

https://user-images.githubusercontent.com/6990718/171319951-8a7e4287-5c42-41ac-bf62-de582b73e3f4.mp4

### Complex usage
This example generates a comments video with multiple voices, using the Azure TTS service, with alternating voices.

Azure key information is saved for future usage, so it does not have to be specified each generation.

`ttsgen -url https://www.reddit.com/r/AskReddit/comments/v1luy1/college_graduates_of_reddit_what_happened_to_that/ -background input.mp4 -start 00:00:30 -comments 4 -server westeurope -key YOUR_AZURE_SUBSCRIPTION_KEY -alternate true`

https://user-images.githubusercontent.com/6990718/171317064-be5638ad-d265-4530-9e82-12498b8d20ae.mp4

## Extending
The library itself is completely modular and by implementing the various interfaces at your disposal, the videos generated can be completely changed.
Refer to the [docs](https://sebheron.github.io/tiktok-reddit-text-to-speech) for more detail on how this is currently achieved.
