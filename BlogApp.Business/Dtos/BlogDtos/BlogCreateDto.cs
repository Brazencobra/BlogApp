using BlogApp.Business.Validators;
using BlogApp.Core.Entities;
using BlogApp.DAL.Contexts;
using FluentValidation;
using Microsoft.AspNetCore.Http;
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
        public string CoverImage { get; set; }
        //public IFormFile CoverImageFile{ get; set; }
        public IEnumerable<int> CategoryIds { get; set; }
    }
    public class BlogCreateDtoValidator : AbstractValidator<BlogCreateDto>
    {
        AppDbContext _context;
        public BlogCreateDtoValidator(AppDbContext context)
        {
            _context = context;
            RuleFor(x => x.Title)
                .SetValidator(new BaseValidator())
                .Length(2, 255)
                    .WithMessage("2-255 uzunlugunda olmalidir");
            RuleFor(x => x.Description)
                .NotEmpty()
                    .WithMessage("Boş ola bilməz")
                .NotEmpty()
                    .WithMessage("Boş ola bilməz");
            RuleFor(x => x.CoverImage)
                .NotEmpty()
                    .WithMessage("Boş ola bilməz")
                .NotEmpty()
                    .WithMessage("Boş ola bilməz");
            RuleForEach(x => x.CategoryIds)
                .NotEmpty()
                    .WithMessage("Boş ola bilməz")
                .NotEmpty()
                    .WithMessage("Boş ola bilməz")
                .GreaterThan(0);
            //RuleFor(x => x.CoverImageFile)
            //    .SetValidator(new FileValidator());
            RuleFor(x => x)
                .Custom(CheckRepeatId);
        }
        void CheckRepeatId(BlogCreateDto dto , ValidationContext<BlogCreateDto> context)
        {
            HashSet<int> ids = new HashSet<int>();
            foreach (var item in dto.CategoryIds)
            {
                if (ids.Contains(item))
                {
                    context.AddFailure("CategoryIds","Bir kateqoriya yalnızca 1dəfə əlavə edilə bilər");
                }
                ids.Add(item);
            }
        }
    }
}
