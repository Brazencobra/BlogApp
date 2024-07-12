using BlogApp.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        readonly IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _roleService.GetAllAsync());
        }
        [Authorize]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _roleService.GetByIdAsync(id));
        }
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> Post(string name)
        {
            await _roleService.CreateAsync(name);
            return Ok();
        }
        [Authorize]
        [HttpPut("[action]")]
        public async Task<IActionResult> Put(string id,string name)
        {
            await _roleService.UpdateAsync(id,name);
            return Ok();
        }
        [Authorize]
        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(string roleName)
        {
            await _roleService.RemoveAsync(roleName);
            return Ok();
        }
        
    }
}
