using FluentValidation;
using SchemaProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OperationProject.Validation
{
    public class TokenValidator : AbstractValidator<TokenRequest>
    {
        public TokenValidator() 
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                .Must(BeAValidEmail).WithMessage("Email is not valid.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
                .MinimumLength(5).WithMessage("Password must contains at least 5 characters.");
        }
        private bool BeAValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return true;
            string emailPattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
