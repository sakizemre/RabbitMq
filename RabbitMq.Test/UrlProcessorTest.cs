using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using RabbitMq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class UrlProcessorTests
{
    [Fact]
    public async Task ProcessUrl_ReturnsStatusCode()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<UrlProcessor>>();
        var handlerMock = new Mock<HttpMessageHandler>();
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("[{'id':1,'value':'1'}]"),
        };

        handlerMock.Protected()
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>())
           .ReturnsAsync(response);

        var httpClient = new HttpClient(handlerMock.Object);
        var urlProcessor = new UrlProcessor(loggerMock.Object);

        // Act
        await urlProcessor.ProcessUrl("http://test.com");

        // Assert
        loggerMock.Verify(
            x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
    }
}