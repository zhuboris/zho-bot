using GlobalUtils;
using System.Text.Json;
using UnitTests.TestSetups;

namespace UnitTests.GlobalUtils
{
    public class TokenGetterTests
    {
        private const string TestFilePath = "testsettings.json";
        private const string TestKey = "TestKey";
        private const string TestTokenValue = "TestTokenValue";

        public TokenGetterTests()
        {
            Cleanup.DeleteFileIfItExists(TestFilePath);
            CreateTestSettingsFile();
        }

        [Fact]
        public void GetToken_ShouldReturnTokenValue_WhenKeyExistsInSettings()
        {
            // Arrange
            var expectedToken = TestTokenValue;

            // Act
            var actualToken = TokenGetter.GetToken(TestFilePath, TestKey);

            // Assert
            Assert.Equal(expectedToken, actualToken);
        }

        [Fact]
        public void GetToken_ShouldReturnNull_WhenKeyDoesNotExistInSettings()
        {
            // Arrange
            var nonExistentKey = "NonExistentKey";

            // Act
            var actualToken = TokenGetter.GetToken(TestFilePath, nonExistentKey);

            // Assert
            Assert.Null(actualToken);
        }

        private void CreateTestSettingsFile()
        {
            var settings = new
            {
                Settings = new
                {
                    TestKey = TestTokenValue
                }
            };

            var jsonString = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(TestFilePath, jsonString);
        }
    }
}