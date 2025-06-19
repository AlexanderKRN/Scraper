using CSharpFunctionalExtensions;
using Scraper.Domain.Common;

namespace Scraper.Domain.ValueObject;

public class Attribute
{
    public string Name { get; private set; } = null!;
    public string Value { get; private set; } = null!;

    protected Attribute()
    {
    }

    protected Attribute(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public static Result<Attribute, Error> Create(string name, string value)
    {
        name = name.Trim();
        if (name.IsEmpty())
            return ErrorList.General.ValueIsInvalid();

        return new Attribute(name, value);
    }
}
