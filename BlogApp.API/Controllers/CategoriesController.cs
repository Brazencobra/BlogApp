using BlogApp.Business.Dtos.CategoryDtos;
using BlogApp.Business.Exceptions.Category;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.HelperServices.Interfaces;
using BlogApp.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        readonly ICategoryService _service;
        readonly ITokenHandler _token;
        public CategoriesController(ICategoryService service, ITokenHandler token)
        {
            _service = service;
            _token = token;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAllAsync());
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Post([FromForm] CategoryCreateDto dto)
        {
            await _service.CreateAsync(dto);
            return Ok(dto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] CategoryUpdateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok(dto);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoveAsync(id);
            return Ok();
        }
    }
}
