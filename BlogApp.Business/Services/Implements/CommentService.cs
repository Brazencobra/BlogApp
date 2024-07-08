using AutoMapper;
using BlogApp.Business.Dtos.BlogDtos;
using BlogApp.Business.Dtos.CommentDtos;
using BlogApp.Business.Exceptions.AppUser;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Services.Implements
{
    public class CommentService : ICommentService
    {
        readonly ICommentRepository _repo;
        readonly IBlogRepository _blogRepository;
        readonly IMapper _mapper;
        readonly UserManager<AppUser> _userManager;
        readonly string _userId;
        readonly IHttpContextAccessor _context;
        public CommentService(ICommentRepository repo, IBlogRepository blogRepository, IMapper mapper, UserManager<AppUser> userManager, IHttpContextAccessor context)
        {
            _repo = repo;
            _blogRepository = blogRepository;
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _userId = _context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task CreateAsync(int id, CommentCreateDto dto)
        {
            if (id <= 0) throw new NegativeIdException();
            if (!await _blogRepository.IsExistAsync(b => b.Id == id)) throw new NotFoundException<Blog>();
            if (string.IsNullOrWhiteSpace(_userId)) throw new ArgumentNullException();
            if (!await _userManager.Users.AnyAsync(u => u.Id == _userId)) throw new UserNotFoundException();
            var comment = _mapper.Map<Comment>(dto);
            comment.AppUserId = _userId;
            comment.BlogId = id;
            await _repo.CreateAsync(comment);
            await _repo.SaveAsync();
        }

        public async Task<IEnumerable<CommentListItemDto>> GetAllAsync()
        {
            var entity = _repo.GetAll();
            return _mapper.Map<IEnumerable<CommentListItemDto>>(entity);
        }

        public async Task<CommentDetailDto> GetByIdAsync(int id)
        {
            if (id <= 0) throw new NegativeIdException();
            var entity = await _repo.FindByIdAsync(id);
            if (entity is null) throw new NotFoundException<Comment>();

            return _mapper.Map<CommentDetailDto>(entity);
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
