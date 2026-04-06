using App.Application.DTOs.Email.cs;
using FluentValidation;
using iText.StyledXmlParser.Css.Resolve.Shorthand.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Validators.Email
{
    public class SendEmailRequestValidator : AbstractValidator<SendEmailRequestDto>
    {
        public SendEmailRequestValidator()
        {
            RuleFor(x => x.To)
            .NotEmpty().EmailAddress();

            RuleFor(x => x.Subject)
                .NotEmpty();

            RuleFor(x => x.OutputContent)
                .NotEmpty();
        }

    }
}
