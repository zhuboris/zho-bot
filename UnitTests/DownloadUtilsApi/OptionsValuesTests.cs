using DownloadUtilsApi;

namespace UnitTests.DownloadUtilsApi
{
    public class OptionsValuesTests
    {
        [Theory]
        [InlineData(10485760L, "\"bv*[filesize<7,5M][height>=240] + ba[filesize<2,5M] / b[filesize<10M][height>=240] / b[filesize_approx<10M][height>=240]\"")]
        [InlineData(5242880L, "\"bv*[filesize<3,75M][height>=240] + ba[filesize<1,25M] / b[filesize<5M][height>=240] / b[filesize_approx<5M][height>=240]\"")]
        public void GetFormatOptionValue_ShouldReturnCorrectFormatOptionValue(long maxSizeInBytes, string expectedOptionValue)
        {
            // Act
            string actualOptionValue = OptionsValues.YtDlp.Values.GetFormatOptionValue(maxSizeInBytes);

            // Assert
            Assert.Equal(expectedOptionValue, actualOptionValue);
        }
    }
}