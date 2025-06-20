using CSharpFunctionalExtensions;
using Scraper.Domain.Common;

namespace Scraper.Domain.ValueObject;

/// <summary>
/// Тег "headers"
/// </summary>
public class Headers
{
    public string Title { get; private set; }

    /// <summary>
    /// Перечень строк тега "meta" с ограничением добавления
    /// </summary>
    public IReadOnlyList<MetaLine> Meta => _meta;
    private readonly List<MetaLine> _meta = [];

    /// <summary>
    /// Конструктор EF Core
    /// </summary>
    protected Headers()
    {
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="title"> Тег "title" </param>
    /// <param name="meta"> Тег "meta" </param>
    protected Headers(string title, List<MetaLine> meta)
    {
        Title = title;
        _meta = meta.ToList();
    }

    /// <summary>
    /// Создание нового тега "headers"
    /// </summary>
    /// <param name="title"> Тег "title" </param>
    /// <param name="meta"> Тег "meta" </param>
    /// <returns></returns>
    public static Result<Headers, Error> Create(string title, List<MetaLine> meta)
    {
        title = title.Trim();
        if (title.IsEmpty())
            return ErrorList.General.ValueIsInvalid();

        return new Headers(title, meta);
    }
}
