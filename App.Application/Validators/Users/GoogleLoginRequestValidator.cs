using App.Application.DTOs.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Validators.Users
{
    public class GoogleLoginRequestValidator : AbstractValidator<GoogleLoginRequestDto>
    {
        public GoogleLoginRequestValidator()
        {
            RuleFor(x => x.IdToken)
                .NotEmpty().WithMessage("Id token is required");
        }
    }
}
