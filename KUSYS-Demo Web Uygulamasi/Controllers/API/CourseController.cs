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
        readonly ILoginService _loginService;

        public CourseController(IRepositoryCourse repository, ILoginService loginService)
        {
            _repository = repository;
            _loginService = loginService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repository.Get().ToListAsync();
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _repository.Get(Id).ToListAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<int>> Post(string CourseName, int Level)
        {
            bool result = await _repository.Add(CourseName, Level);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put(int Id, string CourseName, int Level)
        {
            bool result = await _repository.Update(Id, CourseName, Level);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            bool result = await _repository.Remove(Id);
            return Ok(result);
        }
    }
}
