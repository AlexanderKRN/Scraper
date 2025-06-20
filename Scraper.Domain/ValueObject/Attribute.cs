using CSharpFunctionalExtensions;
using Scraper.Domain.Common;

namespace Scraper.Domain.ValueObject;

/// <summary>
/// Аттрибут тега
/// </summary>
public class Attribute
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; private set; } = null!;

    /// <summary>
    /// Значение
    /// </summary>
    public string Value { get; private set; } = null!;

    /// <summary>
    /// Конструктор для EF Core
    /// </summary>
    protected Attribute()
    {
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="name"> Название </param>
    /// <param name="value"> Значение </param>
    protected Attribute(string name, string value)
    {
        Name = name;
        Value = value;
    }

    /// <summary>
    /// Создание нового атрибута
    /// </summary>
    /// <param name="name"> Название </param>
    /// <param name="value"> Значение </param>
    /// <returns></returns>
    public static Result<Attribute, Error> Create(string name, string value)
    {
        name = name.Trim();
        if (name.IsEmpty())
            return ErrorList.General.ValueIsInvalid();

        return new Attribute(name, value);
    }
}
