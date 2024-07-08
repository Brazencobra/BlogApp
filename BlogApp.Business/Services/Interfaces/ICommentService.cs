using BlogApp.Business.Dtos.BlogDtos;
using BlogApp.Business.Dtos.CommentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Services.Interfaces
{
    public interface ICommentService 
    {
        Task<IEnumerable<CommentListItemDto>> GetAllAsync();
        Task<CommentDetailDto> GetByIdAsync(int id);
        //id-Blogun id-dir(Hansi id-li bloga atilir bu comment)
        Task CreateAsync(int id,CommentCreateDto dto);
        //Task UpdateAsync(int id, CommentUpdateDto dto);
        Task RemoveAsync(int id);
    }
}
