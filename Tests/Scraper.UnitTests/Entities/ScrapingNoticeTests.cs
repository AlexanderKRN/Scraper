using FluentAssertions;
using Scraper.Domain.Common;
using Scraper.Domain.ValueObject;
using Attribute = Scraper.Domain.ValueObject.Attribute;

namespace Scraper.Domain.Entities.Tests;

/// <summary>
/// Тестирование отчёта
/// </summary>
public class ScrapingNoticeTests
{
    /// <summary>
    /// Тест создания отчёта с корректными данными
    /// </summary>
    /// <param name="urls"> Перечень URL </param>
    /// <param name="headers"> Данные заголовка </param>
    [Theory]
    [MemberData(nameof(ScrapingNoticeTestsWithValidData))]
    public void CreateTest_WithValidData(string urls, Headers headers)
    {
        // Arrange

        // Act
        var result = ScrapingNotice.Create(urls, headers);

        // Assert
        result.IsSuccess.Should().Be(true);
        result.Value.Should().NotBe(null);
    }

    /// <summary>
    /// Тест создания отчёта с ошибочными данными
    /// </summary>
    /// <param name="urls"> Перечень URL </param>
    /// <param name="headers"> Данные заголовка </param>
    [Theory]
    [MemberData(nameof(ScrapingNoticeTestsWithErrorData))]
    public void CreateTest_WithErrorData(string urls, Headers headers)
    {
        // Arrange

        // Act
        var result = ScrapingNotice.Create(urls, headers);

        // Assert
        result.IsSuccess.Should().Be(false);
        result.Error.Should().NotBe(Error.None);
    }

    /// <summary>
    /// Фейковые данные
    /// </summary>
    public static IEnumerable<object[]> ScrapingNoticeTestsWithValidData =>
        new List<object[]>
        {
            new object[]
            {
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
            },
            new object[]
            {
                "https://mail.ru/",
                null
            },
        };

    /// <summary>
    /// Фейковые данные
    /// </summary>
    public static IEnumerable<object[]> ScrapingNoticeTestsWithErrorData =>
    new List<object[]>
    {
            new object[]
            {
                "",
                null
            }
    };
}