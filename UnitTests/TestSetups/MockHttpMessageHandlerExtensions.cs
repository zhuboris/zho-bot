using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Headers;

namespace UnitTests.TestSetups
{
    internal static class MockHttpMessageHandlerExtensions
    {
        private const string SendAsyncMethodName = "SendAsync";

        public static Mock<HttpMessageHandler> SetupResponce(this Mock<HttpMessageHandler> mock, HttpStatusCode statusCode, MediaTypeHeaderValue? mediaTypeHeader, long? contentLength)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new ByteArrayContent(Array.Empty<byte>())
            };
            response.Content.Headers.ContentType = mediaTypeHeader;
            response.Content.Headers.ContentLength = contentLength;

            mock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    SendAsyncMethodName,
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            return mock;
        }

        public static Mock<HttpMessageHandler> SetupResponceWithoutContext(this Mock<HttpMessageHandler> mock)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = null
            };

            mock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    SendAsyncMethodName,
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            return mock;
        }
    }
}