using DownloadAPI;
using GlobalUtils;
using System.Collections.Immutable;

namespace UnitTests.TestSetups
{
    public static class TestGlobalConstants
    {
        public const string FormatError = $"{MessagesText.Errors.Error} {MessagesText.Errors.FormatNotAvaiable}";
        public const string OtherError = MessagesText.Errors.Error;
        public const string UrlOfSuitableSizeVideo = "https://www.youtube.com/watch?v=7ZgGqUE2JcE";
        public const string UrlOfOversizedVideo = "https://www.youtube.com/watch?v=4CpijfMhBuI";
        public const string InvalidUrl = "InvalidUrl";
        public const string TestPath = "test_video";
        public const string TestExtension = ".mp4";

        public static readonly string[] ExpectedVideoExtensions = { VideoParameters.Extensions.Mp4, VideoParameters.Extensions.Webm };

        public static readonly IReadOnlyCollection<object[]> AllCorrectCodecs = GetAllCorrectCodecs().ToImmutableList();
        public static readonly IReadOnlyCollection<object[]> AllBadResultsValues = GetAllBadResults().ToImmutableList();

        private static IEnumerable<object[]> GetAllCorrectCodecs()
        {
            return VideoParameters.RequiredExtensionsBySupportedCodecs
                .Select(element => new object[] { element.Key, element.Value });
        }

        private static IEnumerable<object[]> GetAllBadResults()
        {
            return Enum.GetValues<DownloadResult>()
                .Where(result => result != DownloadResult.Ok)
                .Select(badResult => new object[] { (int)badResult });
        }
    }
}