using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Dtos.CommentDtos
{
    public record CommentCreateDto
    {
        public string Text { get; set; }
        public int? ParentId { get; set; }
    }
    public class CommentCreateDtoValidator : AbstractValidator<CommentCreateDto>
    {
        public CommentCreateDtoValidator()
        {
            RuleFor(c => c.Text)
                .NotEmpty()
                    .WithMessage("Şərh boş ola bilməz")
                .NotEmpty()
                    .WithMessage("Şərh boş ola bilməz");
        }
    }
}
