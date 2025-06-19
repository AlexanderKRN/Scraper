using FluentValidation;

namespace Scraper.Application.Features.ScrapingByOder;

public class SetOrderHandlerValidator : AbstractValidator<SetOrderHandlerRequest>
{
    public SetOrderHandlerValidator()
    {
        RuleFor(x => x.FilePath)
            .NotEmpty()
            .NotNull();
    }
}
