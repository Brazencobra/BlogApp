using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Dtos.CategoryDtos
{
    public record CategoryCreateDto
    {
        public string Name { get; set; }
        public IFormFile Logo { get; set; }
    }
    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotNull()
                    .WithMessage("Ad boş ola bilməz")
                .NotEmpty()
                    .WithMessage("Ad boş ola bilməz")
                .MaximumLength(64)
                    .WithMessage("64 simvoldan çox ola bilməz");
            RuleFor(c => c.Logo)
                .NotEmpty()
                    .WithMessage("Boş ola bilməz");
        }
    }
}
