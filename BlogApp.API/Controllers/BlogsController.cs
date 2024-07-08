using BlogApp.Business.Dtos.BlogDtos;
using BlogApp.Business.Services.Interfaces;
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

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
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
            try
            {
                await _blogService.CreateAsync(dto);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogService.RemoveAsync(id);
            return Ok();
        }
    }
}
