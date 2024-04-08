using KUSYS_Demo_Web_Uygulamasi.DTOs;
using KUSYS_Demo_Web_Uygulamasi.Models.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo_Web_Uygulamasi.Repository.IRepository
{
    public interface IRepositoryAppUsers
    {
        DbSet<AppUser> Table { get; }
        IQueryable<AppUser> Get();
        IQueryable<AppUser> Get(int Id);
        Task<bool> Add(string Name, string Surname, string Role, string UserName, string Email, string PasswordHash);
        Task<bool> Remove(int Id);
        Task<bool> Update(int Id, string Name, string Surname, string Role, string UserName, string Email, string PasswordHash);
        Task<List<AppUserCourse>> GetAppUserCourse();
        Task<string[]> GetAppUserAppRole(string UserName);
        Task<bool> AddAppUserAppRole(string UserId, string[] roles);
        Task<bool> HasRolePermissionToAttributesAsync(string UserName, string attribute);

    }
}
