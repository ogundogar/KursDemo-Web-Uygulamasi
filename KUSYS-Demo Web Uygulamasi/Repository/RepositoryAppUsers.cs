using KUSYS_Demo_Web_Uygulamasi.Models;
using KUSYS_Demo_Web_Uygulamasi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using KUSYS_Demo_Web_Uygulamasi.Models.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using KUSYS_Demo_Web_Uygulamasi.DTOs;
using System.Data;
using KUSYS_Demo_Web_Uygulamasi.Enums;

namespace KUSYS_Demo_Web_Uygulamasi.Repository
{
    public class RepositoryAppUsers : IRepositoryAppUsers
    {
        readonly CourseDbContext _context;
        readonly UserManager<AppUser> _userManager;
        readonly IRepositoryRole _repositoryRole;
        public RepositoryAppUsers(CourseDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IRepositoryAuthorityLevel repositoryAuthorityLevel, IRepositoryRole repositoryRole)
        {
            _context = context;
            _userManager = userManager;
            _repositoryRole = repositoryRole;
        }


        public DbSet<AppUser> Table => _context.Set<AppUser>();
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
        
        //AppUser-Course Ara tablosu için
        public async Task<List<AppUserCourse>> GetAppUserCourse()
        {
            var appUserCourse = new List<AppUserCourse>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "EXEC ListAppUserCourse";

                if (command.Connection.State != ConnectionState.Open)
                    await command.Connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        appUserCourse.Add(new AppUserCourse
                        {
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            CourseName = reader.GetString(reader.GetOrdinal("CourseName")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Surname = reader.GetString(reader.GetOrdinal("Surname"))
                        });
                    }
                }
            }
            return appUserCourse;
        }

        //AppUser-AppRole
        public async Task<string[]> GetAppUserAppRole(string UserName)
        {
            AppUser user = await _userManager.FindByNameAsync(UserName);
            string[]? UserRole = null;
            if(user==null)
                return UserRole;
            UserRole = (await _userManager.GetRolesAsync(user)).ToArray();
            return UserRole;
        }

        public async Task<bool> AddAppUserAppRole(string UserId,string[] roles)
        {
            AppUser user =await _userManager.FindByIdAsync(UserId);
            await _userManager.UpdateSecurityStampAsync(user);
            await _userManager.AddToRolesAsync(user,roles);
            return true;
        }

        public async Task<bool> HasRolePermissionToAttributesAsync(string UserName, string attribute)
        {
            string[] UserRoles=await GetAppUserAppRole(UserName);

            if (UserRoles == null)
                return false;

            foreach (var userRole in UserRoles)
            {
               string role= await _repositoryRole.GetAppRoleAuthorityLevelAsync(userRole);
               if (role== attribute)
                    return true;
            }
            return false;
        }
    }
}

