using Moq.Protected;
using Moq;
using System.Net;

namespace TestTasks.UnitTests.Helpers
{
    public static class HttpClientMockHelper
    {
        public static HttpClient CreateMockHttpClient(Dictionary<string, string> responses)
        {
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync((HttpRequestMessage request, CancellationToken cancellationToken) =>
            {
                Console.WriteLine($"Requested URL: {request.RequestUri}");

                if (responses.TryGetValue(request.RequestUri.ToString(), out var content))
                {
                    return new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(content)
                    };
                }

                return new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound };
            });

            return new HttpClient(handlerMock.Object);
        }
    }
}
