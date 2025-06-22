using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Scraper.Application.Providers;
using Scraper.Domain.Entities;

namespace Scraper.Application.Features.ScrapingByOder.Tests;

/// <summary>
/// Тестирование обработчика ордера
/// </summary>
public class SetOrderHandlerTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<ILogger<SetOrderHandler>> _loggerMock = new();

    /// <summary>
    /// Тест создания ордера
    /// </summary>
    [Fact()]
    public async void SetOrderHandlerTest()
    {
        // Arrange
        var ct = new CancellationToken();

        var request = new SetOrderHandlerRequest("C:\\temp\\Order.txt");

        var urlsFromFile = new List<string> { "https://yandex.ru/" };

        var order = OrderToScrape.Create(urlsFromFile);

        _orderRepositoryMock.Setup(x => x.Add(order.Value, ct));

        _unitOfWorkMock.Setup(x => x.SaveChangesAsync(ct)).ReturnsAsync(1);

        var sut = new SetOrderHandler(
            _orderRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _loggerMock.Object);

        // Act
        var result = await sut.Handle(request, ct);

        // Assert
        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(ct), Times.Once);
        result.IsSuccess.Should().Be(true);
        result.Value.Should().BeEmpty();
    }
}