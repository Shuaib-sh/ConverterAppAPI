using App.Application.DTOs.DataFormat;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Validators.DataFormat
{
    public class ParseHL7RequestValidator : AbstractValidator<ParseHL7Request>
    {
        public ParseHL7RequestValidator()
        {
            RuleFor(x => x.Input)
                .NotEmpty().WithMessage("HL7 input is required")
                .MinimumLength(10).WithMessage("Invalid HL7 message");
        }
    }
}
