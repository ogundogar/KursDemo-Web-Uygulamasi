using KUSYS_Demo_Web_Uygulamasi.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo_Web_Uygulamasi.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        readonly IRepositoryAppUsers _repository;
        public AppUsersController(IRepositoryAppUsers repository)
        {
            _repository = repository;
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
        public async Task<IActionResult> Post(string Name, string Surname, string Role, string UserName, string Email, string PasswordHash)
        {
            bool result = await _repository.Add(Name, Surname, Role, UserName, Email, PasswordHash);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Put(int Id, string? Name, string? Surname, string? Role, string? UserName, string? Email, string? PasswordHash)
        {
            bool result = await _repository.Update(Id, Name, Surname, Role, UserName, Email, PasswordHash);
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
