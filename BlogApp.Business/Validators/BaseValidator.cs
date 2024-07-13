using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Validators
{
    public class BaseValidator : AbstractValidator<string>
    {
        public BaseValidator()
        {
            RuleFor(x=>x) 
                .NotNull()
                    .WithMessage("Boş ola bilməz")
                .NotEmpty()
                    .WithMessage("Boş ola bilməz");
        }
    }
}
