using AutoMapper;
using BlogApp.Business.Dtos.BlogDtos;
using BlogApp.Business.Dtos.CategoryDtos;
using BlogApp.Business.Exceptions.AppUser;
using BlogApp.Business.Exceptions.Category;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.HelperServices.HelperMethods;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Entities.Enums;
using BlogApp.DAL.Contexts;
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
        readonly AppDbContext _dbcontext;
        readonly IBlogReactionRepository _blogReactionRepo;
        public BlogService(IBlogRepository repository, IMapper mapper, IHttpContextAccessor context, ICategoryRepository categoryRepository, UserManager<AppUser> user, AppDbContext dbContext, IBlogReactionRepository blogReactionRepo)
        {
            _repo = repository;
            _mapper = mapper;
            _context = context;
            userId = _context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _categoryRepository = categoryRepository;
            _user = user;
            _dbcontext = dbContext;
            _blogReactionRepo = blogReactionRepo;
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
            var entity = _repo.GetAll("AppUser","BlogCategories" , "BlogCategories.Category" , "Comments" , "Comments.Children" , "Comments.AppUser" ,"BlogReactions");
            return _mapper.Map<IEnumerable<BlogListItemDto>>(entity);
        }

        public async Task<BlogDetailDto> GetByIdAsync(int id)
        {
            if (id <= 0) throw new NegativeIdException();
            var blog = await _repo.FindByIdAsync(id,
                "AppUser", "BlogCategories", "BlogCategories.Category", "Comments", "Comments.Children", "Comments.AppUser" , "BlogReactions" , "BlogReactions.AppUser");
            if (blog is null) throw new NotFoundException<Blog>();
            blog.ViewerCount++;
            await _repo.SaveAsync();
            return _mapper.Map<BlogDetailDto>(blog);
        }

        public async Task RemoveAsync(int id)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException();
            if(!await _user.Users.AnyAsync(u=>u.Id == userId)) throw new UserNotFoundException(); 
            if (id <= 0) throw new NegativeIdException();
            var blog = await _repo.FindByIdAsync(id);
            if (blog is null) throw new NotFoundException<Blog>();
            if(blog.AppUserId != userId) throw new UserAccessException();
            _repo.SoftDelete(blog);
            await _repo.SaveAsync();
        }

        public async Task ReactAsync(int id, Reactions reaction)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new UserNotFoundException();
            if(!await _user.Users.AnyAsync(u=>u.Id == userId)) throw new UserNotFoundException();
            if (id <= 0) throw new NegativeIdException();
            var blog = await _repo.FindByIdAsync(id,"BlogReactions");
            if (blog is null) throw new NotFoundException<Blog>();
            if (!blog.BlogReactions.Any(x=>x.AppUserId == userId && x.BlogId == id)) 
                 blog.BlogReactions.Add(new BlogReaction { AppUserId = userId , BlogId = id , Reaction = reaction });
            else
            {
                var currentReaction = blog.BlogReactions.FirstOrDefault(x=>x.AppUserId == userId && x.BlogId == id);
                if (currentReaction is null) throw new NotFoundException<BlogReaction>();
                currentReaction.Reaction = reaction;
            }
            await _repo.SaveAsync();
        }

        public async Task RemoveReactAsync(int id)
        {
            if(id <= 0) throw new NegativeIdException();
            if(string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException();
            if(!await _user.Users.AnyAsync(x=>x.Id == userId)) throw new UserNotFoundException();
            var entity = await _blogReactionRepo.GetSingleAsync(x => x.AppUserId == userId && x.BlogId == id);
            if (entity is null) throw new NotFoundException<BlogReaction>();
            _blogReactionRepo.Delete(entity);
            await _repo.SaveAsync();
        }

        public async Task UpdateAsync(int id, BlogUpdateDto dto)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException();
            if (!await _user.Users.AnyAsync(u => u.Id == userId)) throw new UserNotFoundException();
            if (id <= 0) throw new NegativeIdException();
            Blog blog = await _repo.FindByIdAsync(id);
            if (blog is null) throw new NotFoundException<Blog>();
            if (blog.AppUserId != userId) throw new UserAccessException();
            List<BlogCategory> blogCategories = new();
            if (dto.CategoryIds != null)
            {
                foreach (var item in dto.CategoryIds)
                {
                    var category = await _categoryRepository.FindByIdAsync(item);
                    if (category is null) throw new CategoryNotFoundException($"{item} id-li kateqoriya tapılmadı");
                    blogCategories.Add(new BlogCategory { Category = category, Blog = blog });
                }
            }
            _dbcontext.BlogCategories.RemoveRange(_dbcontext.BlogCategories.Where(x => x.BlogId == id));
            blog.AppUserId = userId;
            blog.BlogCategories = blogCategories;
            _mapper.Map(dto,blog);
            await _repo.SaveAsync();
        }
    }
}
