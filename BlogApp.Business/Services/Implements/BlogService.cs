using AutoMapper;
using BlogApp.Business.Dtos.BlogDtos;
using BlogApp.Business.Dtos.CategoryDtos;
using BlogApp.Business.Exceptions.AppUser;
using BlogApp.Business.Exceptions.Category;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.HelperServices.HelperMethods;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.DAL.Repositories.Implements;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        readonly ICategoryRepository _categoryRepository;
        readonly string? userId;
        readonly UserManager<AppUser> _user;
        public BlogService(IBlogRepository repository, IMapper mapper, IHttpContextAccessor context, ICategoryRepository categoryRepository, UserManager<AppUser> user)
        {
            _repo = repository;
            _mapper = mapper;
            _context = context;
            userId = _context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _categoryRepository = categoryRepository;
            _user = user;
        }
        public async Task CreateAsync(BlogCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException();
            if (!await _user.Users.AnyAsync(u => u.Id == userId)) throw new UserNotFoundException();
            List<BlogCategory> blogCategories = new ();
            Blog blog = _mapper.Map<Blog>(dto);
            foreach (var id in dto.CategoryIds)
            {
                var category = await _categoryRepository.FindByIdAsync(id);
                if (category is null) throw new CategoryNotFoundException($"{id} id-li kateqoriya tapılmadı"); 
                blogCategories.Add( new BlogCategory { Category = category , Blog = blog });
            }
            blog.AppUserId = userId;
            blog.BlogCategories = blogCategories;
            await _repo.CreateAsync(blog);
            await _repo.SaveAsync();
        }

        public async Task<IEnumerable<BlogListItemDto>> GetAllAsync()
        {
            var entity = _repo.GetAll("AppUser","BlogCategories" , "BlogCategories.Category");
            return _mapper.Map<IEnumerable<BlogListItemDto>>(entity);
        }

        public async Task<BlogDetailDto> GetByIdAsync(int id)
        {
            if (id <= 0) throw new NegativeIdException();
            var blog = await _repo.FindByIdAsync(id);
            if (blog is null) throw new NotFoundException<Blog>();
            await _repo.SaveAsync();
            return _mapper.Map<BlogDetailDto>(blog);
        }

        public async Task RemoveAsync(int id)
        {
            if (id <= 0) throw new NegativeIdException();
            var blog = await _repo.FindByIdAsync(id);
            if (blog is null) throw new NotFoundException<Blog>();
            _repo.Delete(blog);
            await _repo.SaveAsync();
        }

        public Task UpdateAsync(int id, BlogUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
