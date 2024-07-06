using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Dtos.UserDtos
{
    public record LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty()
                    .WithMessage("Loqin boş ola bilməz")
                .NotNull()
                    .WithMessage("Loqin boş ola bilməz")
                .Length(3, 16)
                    .WithMessage("Loqin 3-16 uzunluğunda olmalıdır");
            RuleFor(u => u.Password)
                .NotEmpty()
                    .WithMessage("Şifrə boş ola bilməz")
                .NotEmpty()
                    .WithMessage("Şifrə boş ola bilməz")
                .Length(2, 8)
                    .WithMessage("Şifrə 2-8 uzunluğunda olmalıdır");
        }
    }
}
