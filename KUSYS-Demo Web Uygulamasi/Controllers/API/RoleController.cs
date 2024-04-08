using KUSYS_Demo_Web_Uygulamasi.Consts;
using KUSYS_Demo_Web_Uygulamasi.CustomAttributes;
using KUSYS_Demo_Web_Uygulamasi.Enums;
using KUSYS_Demo_Web_Uygulamasi.Models.Entities.Identity;
using KUSYS_Demo_Web_Uygulamasi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo_Web_Uygulamasi.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RoleController : ControllerBase
    {

        readonly IRepositoryRole _service;
        readonly IRepositoryAuthorityLevel _serviceAuthorityLevel;
        public RoleController(IRepositoryRole service, IRepositoryAuthorityLevel serviceAuthorityLevel)
        {
            _service = service;
            _serviceAuthorityLevel = serviceAuthorityLevel;
        }

        [HttpGet]
        [AuthorizeCostum(AuthorityLevel = AuthorityLevel.Madde3)]
        public IActionResult Get()
        {
            var result =  _service.Get();
            return Ok(result);
        }
        [HttpGet("{Id}")]
       
        public async Task<IActionResult> Get(string Id)
        {
            AppRole result = await _service.Get(Id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string Name)
        {
            bool result = await _service.Add(Name);
            return Ok(result);
        }
        [HttpPut]
   
        public async Task<IActionResult> Put(string Id, string Name)
        {
            bool result = await _service.Update(Id, Name);
            return Ok(result);
        }
        [HttpDelete]
 
        public async Task<IActionResult> Delete(string Id)
        {
            bool result = await _service.Remove(Id);
            return Ok(result);
        }
        //AppRole-AuthorityLevel
        [HttpPost("AssignRoleAuthorityLevel")]
        public async Task<IActionResult> Post(string AppRoleId, AuthorityLevel authorityLevel)
        {
            bool result = await _service.AssignRoleAuthorityLevelAsync(AppRoleId,authorityLevel);
            return Ok(result);
        }

        [HttpGet("GetAppRoleAuthorityLevel")]

        public async Task<IActionResult> GetAppRoleAuthorityLevel(string RoleName)
        {
            var result = await _service.GetAppRoleAuthorityLevelAsync(RoleName);
            return Ok(result);
        }
    }
}
