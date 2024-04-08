using KUSYS_Demo_Web_Uygulamasi.Consts;
using KUSYS_Demo_Web_Uygulamasi.CustomAttributes;
using KUSYS_Demo_Web_Uygulamasi.Enums;
using KUSYS_Demo_Web_Uygulamasi.Repository.IRepository;
using KUSYS_Demo_Web_Uygulamasi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo_Web_Uygulamasi.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class CourseController : ControllerBase
    {
        readonly IRepositoryCourse _repository; 
      

        public CourseController(IRepositoryCourse repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AuthorizeCostum(AuthorityLevel = AuthorityLevel.Madde3)]
        public async Task<IActionResult> Get()
        {
            var result = await _repository.Get().ToListAsync();
            return Ok(result);
        }
        [HttpGet("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Courses, ActionType = ActionType.Reading, Definition = "Get Course By Id ")]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _repository.Get(Id).ToListAsync();
            return Ok(result);
        }
        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Courses, ActionType = ActionType.Writing, Definition = "Add Course")]
        public async Task<ActionResult<int>> Post(string CourseName, int Level)
        {
            bool result = await _repository.Add(CourseName, Level);
            return Ok();
        }
        [HttpPut]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Courses, ActionType = ActionType.Writing, Definition = "Update Course")]
        public async Task<IActionResult> Put(int Id, string CourseName, int Level)
        {
            bool result = await _repository.Update(Id, CourseName, Level);
            return Ok(result);
        }
        [HttpDelete]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Courses, ActionType = ActionType.Writing, Definition = "Remove Course")]
        public async Task<IActionResult> Delete(int Id)
        {
            bool result = await _repository.Remove(Id);
            return Ok(result);
        }
    }
}
