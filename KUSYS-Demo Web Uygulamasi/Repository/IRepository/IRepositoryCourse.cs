using KUSYS_Demo_Web_Uygulamasi.DTOs;
using KUSYS_Demo_Web_Uygulamasi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo_Web_Uygulamasi.Repository.IRepository
{
    public interface IRepositoryCourse
    {
        DbSet<TbCourse> Table { get; }
        IQueryable<TbCourse> Get();
        Task<List<AppUserCourse>> GetAppUserCourse();
        IQueryable<TbCourse> Get(int Id);
        Task<bool> Add(string CourseName, int Level);
        Task<bool> Remove(int Id);
        Task<bool> Update(int Id, string CourseName, int Level);
    }
}
