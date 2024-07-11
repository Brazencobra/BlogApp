using BlogApp.Business.Dtos.BlogDtos;
using BlogApp.Business.Dtos.CommentDtos;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        readonly IBlogService _blogService;
        readonly ICommentService _commentService;

        public BlogsController(IBlogService blogService, ICommentService commentService)
        {
            _blogService = blogService;
            _commentService = commentService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _blogService.GetAllAsync());
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _blogService.GetByIdAsync(id));
        }
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> Post(BlogCreateDto dto)
        {
            await _blogService.CreateAsync(dto);
            return Ok();
        }
        [Authorize]
        [HttpPut("[action]")]
        public async Task<IActionResult> Put(int id,BlogUpdateDto dto)
        {
            await _blogService.UpdateAsync(id,dto);
            return Ok();
        }
        [Authorize]
        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.RemoveAsync(id);
            return Ok();
        }
        [Authorize]
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Comment(int id,CommentCreateDto dto)
        {
            await _commentService.CreateAsync(id,dto);
            return Ok();
        }
        [Authorize]
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> React(int id, Reactions reaction)
        {
            await _blogService.ReactAsync(id, reaction);
            return Ok();
        }
        [Authorize]
        [HttpDelete("[action]")]
        public async Task<IActionResult> RemoveReact(int id)
        {
            await _blogService.RemoveReactAsync(id);
            return Ok();
        }
    }
}
