using KUSYS_Demo_Web_Uygulamasi.Enums;
using KUSYS_Demo_Web_Uygulamasi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo_Web_Uygulamasi.Repository.IRepository
{
    public interface IRepositoryAuthorityLevel
    {
        DbSet<TbAuthorityLevel> Table { get; }
        IQueryable<TbAuthorityLevel> Get();
        Task<TbAuthorityLevel> Get(int Id);
        Task<bool> Add(AuthorityLevel AuthorityLevel);
        bool Remove(AuthorityLevel AuthorityLevel);
        bool Update(AuthorityLevel AuthorityLevel);
        Task<int> SaveAsync();
    }
}