using AutoMapper;
using BlogApp.Business.Dtos.BlogDtos;
using BlogApp.Business.Dtos.CategoryDtos;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.DAL.Repositories.Implements;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Services.Implements
{
    public class BlogService : IBlogService
    {
        readonly IBlogRepository _repo;
        readonly IMapper _mapper;
        readonly IHttpContextAccessor _context;
        readonly string userId;
        public BlogService(IBlogRepository repository,IMapper mapper , IHttpContextAccessor context)
        {
            _repo = repository;
            _mapper = mapper;
            userId = _context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }   
        public async Task CreateAsync(BlogCreateDto dto)
        {
            //Blog blog = new Blog();
            //_mapper.Map(dto,blog);
            //_repository.CreateAsync(blog);
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogListItemDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<BlogListItemDto>>(_repo.GetAll());
        }

        public Task<CategoryDetailDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, BlogUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
