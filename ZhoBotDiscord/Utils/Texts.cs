namespace ZhoBotDiscord.Utils
{
    internal static class Texts
    {
        private const string HighlitingPattern = "**`{0}`**";

        private const string Info = "/info";
        private const string CheckDownloadLimit = "/check-download-limit";
        private const string PostVideoByUrl = "/post-video-by-url";
        private const string PostGifByUrl = "/post-gif-by-url";        

        public static string Introduction => $"{Emotes.Cookie}  Greetings, {{0}}! I am ZhoBot, a comfortable and versatile bot designed to enhance your video and GIF sharing experience on Discord! Since my purpose is to conveniently download files and not write texts, I have turned to ChatGPT-4, an AI language model, to help me with this introduction.{Environment.NewLine}{Environment.NewLine}{Emotes.Cookie}  My main strength lies in my ability to let you send videos and GIFs from almost any source without downloading them to your device. Just send a link, and I will handle the rest. Popular platforms like YouTube, Vimeo, and TikTok are supported, making it a breeze to share content with friends and community members.{Environment.NewLine}{Environment.NewLine}{Emotes.Cookie}  Please note that while I strive to support as many sources as possible, there may be instances where a particular source isn't supported. Additionally, Discord's own file size limits still apply.{Environment.NewLine}{Environment.NewLine}{Emotes.Cookie}  I was created by Boris Zhuravel, and I am built on ASP.NET and harness the power of yt-dlp to deliver a user-friendly experience. To check out my source code or contribute to my development, feel free to visit the GitHub repository.{Environment.NewLine}{Environment.NewLine}{Emotes.Cookie}  Now that you're acquainted with me and my capabilities, it's time to welcome me into your Discord life and make sharing videos and GIFs more enjoyable than ever! {Emotes.BotImage}";
        public static string CommandsDescription => $"{Emotes.Cookie}  Here are the list of my commands:  {Emotes.Cookie}{Environment.NewLine}{Environment.NewLine}{GetFormattedString(Info, HighlitingPattern)} - This command shows information about me, ZhoBot, and about my creator.{Environment.NewLine}{Environment.NewLine}{GetFormattedString(CheckDownloadLimit, HighlitingPattern)} - This command responds with the maximum attachment size allowed on the current server.{Environment.NewLine}{Environment.NewLine}{GetFormattedString(PostVideoByUrl, HighlitingPattern)} - This command allows the user to post a video by inputting a URL. The user can also add a message and set a filename. Please note that there is a download size limit and the command supports a huge number of sources, but not all.{Environment.NewLine}{Environment.NewLine}{GetFormattedString(PostGifByUrl, HighlitingPattern)} - This command is the same as the post-video-by-url command, but it only supports direct URLs to GIF files. {Environment.NewLine}{Environment.NewLine}Please keep in mind that there may be certain restrictions in place for these commands depending on the server settings.{Environment.NewLine}Have a good day! {Emotes.Smile}";
    
        private static string GetFormattedString(string input, string pattern)
        {
            return String.Format(pattern, input);
        }
    }
}