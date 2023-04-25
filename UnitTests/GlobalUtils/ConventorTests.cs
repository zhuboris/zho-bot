using GlobalUtils;

namespace UnitTests.GlobalUtils
{
    public class ConventorTests
    {
        [Fact]
        public void ConvertToLong_ShouldConvertUlongToLong_WhenValueIsWithinRange()
        {
            // Arrange
            ulong input = 1000;
            long expected = 1000;

            // Act
            long actual = input.ConvertToLong();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConvertToLong_ShouldReturnLongMaxValue_WhenUlongValueExceedsLongMaxValue()
        {
            // Arrange
            ulong input = (ulong)long.MaxValue + 1;
            long expected = long.MaxValue;

            // Act
            long actual = input.ConvertToLong();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConvertToMb_ShouldConvertBytesToFloatMb_WhenGivenBytesValueIsLong()
        {
            // Arrange
            long bytes = 10485760;
            float expected = 10.0f;

            // Act
            float actual = bytes.ConvertToMb();

            // Assert
            Assert.Equal((double)expected, (double)actual, 2);
        }

        [Fact]
        public void ConvertToMb_ShouldConverBytesToFloatMb_WhenGivenBytesValueIstUlong()
        {
            // Arrange
            ulong bytes = 10485760;
            float expected = 10.0f;

            // Act
            float actual = bytes.ConvertToMb();

            // Assert
            Assert.Equal((double)expected, (double)actual, 2);
        }
    }
}