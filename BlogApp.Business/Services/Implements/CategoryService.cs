using BlogApp.Business.Dtos.CategoryDtos;
using BlogApp.Business.Exceptions.Category;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateAsync(CategoryCreateDto dto)
        {
            Category category = new Category 
            { 
                Name = dto.Name,
                LogoUrl = "test",
                IsDeleted = false,
            };
            await _repo.CreateAsync(category);
            await _repo.SaveAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _repo.GetAll().ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            if (id <= 0) throw new NegativeIdException();
            var entity = await _repo.FindByIdAsync(id);
            if(entity is null) throw new CategoryNotFoundException();
            return entity;
        }
    }
}
