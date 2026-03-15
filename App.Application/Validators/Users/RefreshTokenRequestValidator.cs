using App.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Validators.Users
{
    public class RefreshTokenRequestValidator : AbstractValidator<TokenRequest.RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required")
                .MinimumLength(20).WithMessage("Refresh token must be at least 20 characters");
        }
    }
}
