using System;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
