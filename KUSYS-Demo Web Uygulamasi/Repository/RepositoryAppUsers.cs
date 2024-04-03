using KUSYS_Demo_Web_Uygulamasi.Models.Entities;
using KUSYS_Demo_Web_Uygulamasi.Models;
using KUSYS_Demo_Web_Uygulamasi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using KUSYS_Demo_Web_Uygulamasi.Models.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace KUSYS_Demo_Web_Uygulamasi.Repository
{
    public class RepositoryAppUsers : IRepositoryAppUsers
    {
        readonly CourseDbContext _context;
        readonly UserManager<AppUser> _userManager;
        public RepositoryAppUsers(CourseDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IQueryable<AppUser> Get()
        {
            var result = _context.Users.FromSqlRaw("ListAppUser");
            return result;
        }
        public IQueryable<AppUser> Get(int Id)
        {
            var result = _context.Users.FromSqlRaw($"SelectAppUserId {Id}");
            return result;
        }
        public async Task<bool> Add(string Name, string Surname, string Role, string UserName, string Email, string PasswordHash)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                UserName = UserName,
                Name = Name,
                Surname = Surname,
                Email = Email,
                Role = Role,

            }, PasswordHash);
            return result.Succeeded;
        }
        public async Task<bool> Remove(int Id)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[DeleteAppUser]  @Id={Id}");
            return true;
        }
        public async Task<bool> Update(int Id, string Name, string Surname, string Role, string UserName, string Email, string PasswordHash)
        {
            //await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[UpdateAppUser] @Id={Id}, @Name={Name},@Surname={Surname},@Role={Role},@UserName={UserName},@Email={Email},@PasswordHash={PasswordHash}");
            return true;
        }
    }
}
