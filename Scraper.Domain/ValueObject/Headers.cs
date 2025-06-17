using CSharpFunctionalExtensions;
using Scraper.Domain.Common;

namespace Scraper.Domain.ValueObject;

public class Headers
{
    public string Title { get; private set; }

    public IReadOnlyList<MetaLine> Meta => _meta;
    private readonly List<MetaLine> _meta = [];

    private Headers(string title, List<MetaLine> meta)
    {
        Title = title;
        _meta = meta.ToList();
    }

    public static Result<Headers, Error> Create(string title, List<MetaLine> meta)
    {
        title = title.Trim();
        if (title.IsEmpty())
            return ErrorList.General.ValueIsInvalid();

        return new Headers(title, meta);
    }
}
