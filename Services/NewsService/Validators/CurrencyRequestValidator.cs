using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

using DTO.NewsRequests.Currency;

namespace NewsService.Validators
{
    public class CurrencyRequestValidator : AbstractValidator<CurrencyRequest>
    {
        public CurrencyRequestValidator()
        {
            RuleFor(r => r)
                .NotNull()
                .WithMessage("Request parameters must be not null");

            RuleFor(r => r)
                .Must(r => r.CurrecyPublisher != 0)
                .WithMessage("You must set currency publisher");

            RuleFor(r => r.CurrencyCodes)
                .NotNull()
                .WithMessage("List with currency codes must be not null");

            RuleFor(r => r)
                .Must(r => r.CurrencyCodes.Count > 0)
                .WithMessage("You must set currency codes");
        }
    }
}
