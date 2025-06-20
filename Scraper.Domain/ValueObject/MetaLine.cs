using CSharpFunctionalExtensions;
using Scraper.Domain.Common;

namespace Scraper.Domain.ValueObject;

/// <summary>
/// Одна строка тега "meta"
/// </summary>
public class MetaLine
{
    /// <summary>
    /// Перечень аттрибутов с ограничением добавления
    /// </summary>
    public IReadOnlyList<Attribute> Attributes => _attributes;
    private readonly List<Attribute> _attributes = [];

    /// <summary>
    /// Конструктор для EF Core
    /// </summary>
    protected MetaLine()
    {
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="attributes"> Аттрибуты одной строки тега </param>
    protected MetaLine(List<Attribute> attributes)
    {
        _attributes = attributes.ToList();
    }

    /// <summary>
    /// Создание нового аттрибута
    /// </summary>
    /// <param name="attributes"> Аттрибуты одной строки тега </param>
    /// <returns></returns>
    public static Result<MetaLine, Error> Create (List<Attribute> attributes)
    {
        if (attributes is null)
            return ErrorList.General.ValueIsInvalid();

        return new MetaLine(attributes);
    }
}
