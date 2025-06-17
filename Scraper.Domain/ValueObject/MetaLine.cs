using CSharpFunctionalExtensions;
using Scraper.Domain.Common;

namespace Scraper.Domain.ValueObject;

public class MetaLine
{
    public IReadOnlyList<Attribute> Attributes => _attributes;
    private readonly List<Attribute> _attributes = [];

    private MetaLine(List<Attribute> attributes)
    {
        _attributes = attributes.ToList();
    }

    public static Result<MetaLine, Error> Create (List<Attribute> attributes)
    {
        if (attributes is null)
            return ErrorList.General.ValueIsInvalid();

        return new MetaLine(attributes);
    }
}
