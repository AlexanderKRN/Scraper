using FluentValidation;

namespace Scraper.Application.Features.ScrapingByOder;

/// <summary>
/// Валидатор данных в запросе по новому ордеру
/// </summary>
public class SetOrderHandlerValidator : AbstractValidator<SetOrderHandlerRequest>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SetOrderHandlerValidator()
    {
        RuleFor(x => x.FilePath)
            .NotEmpty()
            .NotNull();
    }
}
