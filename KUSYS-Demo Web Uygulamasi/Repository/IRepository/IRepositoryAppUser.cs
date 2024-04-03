using KUSYS_Demo_Web_Uygulamasi.Models.Entities.Identity;

namespace KUSYS_Demo_Web_Uygulamasi.Repository.IRepository
{
    public interface IRepositoryAppUsers
    {
        IQueryable<AppUser> Get();
        IQueryable<AppUser> Get(int Id);
        Task<bool> Add(string Name, string Surname, string Role, string UserName, string Email, string PasswordHash);
        Task<bool> Remove(int Id);
        Task<bool> Update(int Id, string Name, string Surname, string Role, string UserName, string Email, string PasswordHash);
    }
}
