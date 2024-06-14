using BlogApp.Core.Entities;

namespace BlogApp.Business.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
