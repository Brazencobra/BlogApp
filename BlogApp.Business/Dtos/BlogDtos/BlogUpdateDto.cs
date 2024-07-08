using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Dtos.BlogDtos
{
    public record BlogUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public IEnumerable<int>? CategoryIds { get; set; }
    }
    public class BlogUpdateDtoValidator : AbstractValidator<BlogUpdateDto>
    {
        public BlogUpdateDtoValidator()
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
            RuleFor(x => x)
                .Custom(CheckRepeatId);
        }
        void CheckRepeatId(BlogUpdateDto dto, ValidationContext<BlogUpdateDto> context)
        {
            HashSet<int> ids = new HashSet<int>();
            foreach (var item in dto.CategoryIds)
            {
                if (ids.Contains(item))
                {
                    context.AddFailure("CategoryIds", "Bir kateqoriya yalnızca 1dəfə əlavə edilə bilər");
                }
                ids.Add(item);
            }
        }
    }
}
