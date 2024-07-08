using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Dtos.CommentDtos
{
    public class CommentUpdateDto
    {
        public string Text { get; set; }
    }
    public class CommentUpdateDtoValidator : AbstractValidator<CommentUpdateDto> 
    {
        public CommentUpdateDtoValidator()
        {
            RuleFor(c => c.Text)
                .NotEmpty()
                    .WithMessage("Şərh boş ola bilməz")
                .NotEmpty()
                    .WithMessage("Şərh boş ola bilməz");
        }
    }
}
