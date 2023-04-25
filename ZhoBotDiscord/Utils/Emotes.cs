using Discord;

namespace ZhoBotDiscord.Utils
{
    internal static class Emotes
    {
        private const string BotImageEmoteToParse = "<:ZhoBig:1099430820386721903>";
        private const string PlusEmoteToParse = "<:Plus:1099650662888910848>";
        private const string ListEmoteToParse = "<:List:1099651769254039592>";
        private const string GitEmoteToParse = "<:Git:1099625000346079304>";
        private const string SmileEmojiToParse = ":slight_smile:";
        private const string CookieEmojiToParse = ":cookie:";

        static Emotes()
        {
            BotImage = BotImageEmoteToParse;
            Plus = PlusEmoteToParse;
            List = ListEmoteToParse;
            Git = GitEmoteToParse;
            Smile = SmileEmojiToParse;
            Cookie = CookieEmojiToParse;
        }

        public static Emote BotImage { get; }
        public static Emote Plus { get; }
        public static Emote List { get; }
        public static Emote Git { get; }
        public static Emoji Smile { get; }
        public static Emoji Cookie { get; }
    }
}