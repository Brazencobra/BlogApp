using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Dtos.UserDtos
{
    public record RegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                    .WithMessage("Ad boş ola bilməz")
                .NotNull()
                    .WithMessage("Ad boş ola bilməz")
                .Length(2, 32)
                    .WithMessage("Ad 2-32 uzunluğunda olmalıdır");
            RuleFor(u => u.Surname)
                .NotEmpty()
                    .WithMessage("Soyad boş ola bilməz")
                .NotNull()
                    .WithMessage("Soyad boş ola bilməz")
                .Length(2, 32)
                    .WithMessage("Soyad 2-32 uzunluğunda olmalıdır");
            RuleFor(u => u.UserName)
                .NotEmpty()
                    .WithMessage("Loqin boş ola bilməz")
                .NotNull()
                    .WithMessage("Loqin boş ola bilməz")
                .Length(3, 16)
                    .WithMessage("Loqin 3-16 uzunluğunda olmalıdır");
            RuleFor(u => u.Email)
                .NotEmpty()
                    .WithMessage("E-mail boş ola bilməz")
                .NotEmpty()
                    .WithMessage("E-mail boş ola bilməz")
                .EmailAddress()
                    .WithMessage("E-mail doğru formatda deyil");
            RuleFor(u => u.Password)
                .NotEmpty()
                    .WithMessage("Şifrə boş ola bilməz")
                .NotEmpty()
                    .WithMessage("Şifrə boş ola bilməz")
                .Length(2, 8)
                    .WithMessage("Şifrə 2-8 uzunluğunda olmalıdır");
            RuleFor(u => u)
                .Must(u => u.Password == u.ConfirmPassword)
                    .WithMessage("Təkrar şifrə eyni deyil");
        }
    }
}
