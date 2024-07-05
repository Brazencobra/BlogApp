using AutoMapper;
using BlogApp.Business.Dtos.CategoryDtos;
using BlogApp.Business.Exceptions.Category;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.DAL.Contexts;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BlogApp.Business.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        readonly ICategoryRepository _repo;
        readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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

        public async Task<CategoryDetailDto> GetByIdAsync(int id)
        {
            Category category = await _getCategoryAsync(id);
            return _mapper.Map<CategoryDetailDto>(category);
        }

        public async Task RemoveAsync(int id)
        {
            Category category = await _getCategoryAsync(id);
            _repo.Delete(category);
            await _repo.SaveAsync();
        }

        public async Task UpdateAsync(int id, CategoryUpdateDto dto)
        {
            Category category = await _getCategoryAsync(id);
            _mapper.Map(dto,category);
            await _repo.SaveAsync();
        }

        public async Task<IEnumerable<CategoryListItemDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<CategoryListItemDto>>(_repo.GetAll());
        }

        private async Task<Category> _getCategoryAsync(int id)
        {
            if (id <= 0) throw new NegativeIdException();
            var entity = await _repo.FindByIdAsync(id);
            if (entity is null) throw new NotFoundException<Category>();
            return entity;
        }
    }
}
