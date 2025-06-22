using FluentAssertions;
using Scraper.Domain.Common;
using Scraper.Domain.ValueObject;
using Attribute = Scraper.Domain.ValueObject.Attribute;

namespace Scraper.Domain.Entities.Tests;

/// <summary>
/// Тестирование ордера
/// </summary>
public class OrderToScrapeTests
{
    /// <summary>
    /// Тест добавления отчёта
    /// </summary>
    [Fact]
    public void AddNoticeTest()
    {
        // Arrange
        var notice = ScrapingNotice.Create(
            "https://yandex.ru/",
                Headers.Create(
                    "Title",
                    new List<MetaLine>
                    {
                        MetaLine.Create(
                        new List<Attribute>
                        {
                            Attribute.Create("name", "value").Value
                        }).Value
                    }).Value
            );

        var order = OrderToScrape.Create(new List<string> { "https://yandex.ru/" });

        // Act
        var result = order.Value.AddNotice(notice.Value);

        // Assert
        result.IsSuccess.Should().Be(true);
        order.Value.Notices.Should().Contain(notice.Value);
        order.Value.Notices.Should().HaveCount(1);

    }

    /// <summary>
    /// Тест создания ордера с корректными данными
    /// </summary>
    [Fact]
    public void CreateTest_WithValidData()
    {
        // Arrange
        var urls = new List<string> {
            "https://yandex.ru/",
            "https://yandex.ru/",
            "https://yandex.ru/"};

        // Act
        var result = OrderToScrape.Create(urls);

        // Assert
        result.IsSuccess.Should().Be(true);
        result.Value.Should().NotBe(null);
    }

    /// <summary>
    /// Тест создания ордера с ошибочными данными
    /// </summary>
    /// <param name="urls"></param>
    [Theory]
    [MemberData(nameof(OrderToScrapeTestsWithErrorData))]
    public void CreateTest_WithErrorData(List<string> urls)
    {
        // Arrange

        // Act
        var result = OrderToScrape.Create(urls);

        // Assert
        result.IsSuccess.Should().Be(false);
        result.Error.Should().NotBe(Error.None);
    }

    /// <summary>
    /// Фейковые данные
    /// </summary>
    public static IEnumerable<object[]> OrderToScrapeTestsWithErrorData =>
        new List<object[]>
        {
            new object[]
            {
                new List<string>()
            },
            new object[]
            {
                new List<string> {
                    "https://yandex.ru/", "https://yandex.ru/", "https://yandex.ru/",
                    "https://yandex.ru/", "https://yandex.ru/", "https://yandex.ru/",
                    "https://yandex.ru/", "https://yandex.ru/", "https://yandex.ru/",
                    "https://yandex.ru/", "https://yandex.ru/", "https://yandex.ru/",
                    "https://yandex.ru/", "https://yandex.ru/", "https://yandex.ru/",
                    "https://yandex.ru/", "https://yandex.ru/", "https://yandex.ru/",
                    "https://yandex.ru/", "https://yandex.ru/", "https://yandex.ru/",}
            },
        };
}