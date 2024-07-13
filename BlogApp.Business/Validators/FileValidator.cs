using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Validators
{
    partial class FileValidator : AbstractValidator<IFormFile>
    {
        public FileValidator(int sizeWithMb = 3,string contentType = "image")
        {
            RuleFor(x => x.ContentType)
                .Must(x => x.Contains(contentType))
                    .WithMessage("Fayl dogru formatda deyil");
            RuleFor(x => x.Length)
                .LessThanOrEqualTo(sizeWithMb * 1024 * 1024)
                    .WithMessage($"Fayl olcusu {sizeWithMb}mb-dan kicik olmalidir");
        }
    }
}
