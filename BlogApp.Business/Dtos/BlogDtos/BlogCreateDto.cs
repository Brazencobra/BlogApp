using BlogApp.Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Dtos.BlogDtos
{
    public record BlogCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
    }
    public class BlogCreateDtoValidator : AbstractValidator<BlogCreateDto>
    {
        public BlogCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                    .WithMessage("Boş ola bilməz")
                .NotEmpty()
                    .WithMessage("Boş ola bilməz")
                .Length(2, 255)
                    .WithMessage("2-255 uzunlugunda olmalidir");
            RuleFor(x => x.Description)
                .NotEmpty()
                    .WithMessage("Boş ola bilməz")
                .NotEmpty()
                    .WithMessage("Boş ola bilməz");
            RuleFor(x => x.CoverImageUrl)
                .NotEmpty()
                    .WithMessage("Boş ola bilməz")
                .NotEmpty()
                    .WithMessage("Boş ola bilməz");
        } 
    }
}
