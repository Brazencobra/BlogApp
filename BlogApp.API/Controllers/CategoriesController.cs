﻿using BlogApp.Business.Dtos.CategoryDtos;
using BlogApp.Business.Exceptions.Category;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(id));
            }
            catch (NegativeIdException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (CategoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
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
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok();
        }
    }
}
