using BlogApp.Business.Dtos.BlogDtos;
using BlogApp.Business.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Services.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogListItemDto>> GetAllAsync();
        Task<BlogDetailDto> GetByIdAsync(int id);
        Task CreateAsync(BlogCreateDto dto);
        Task UpdateAsync(int id,BlogUpdateDto dto);
        Task RemoveAsync(int id);
    }
}
