using KUSYS_Demo_Web_Uygulamasi.Models.Entities;

namespace KUSYS_Demo_Web_Uygulamasi.Repository.IRepository
{
    public interface IRepositoryCourse
    {
        IQueryable<Course> Get();
        IQueryable<Course> Get(int Id);
        Task<bool> Add(string CourseName, int Level);
        Task<bool> Remove(int Id);
        Task<bool> Update(int Id, string CourseName, int Level);
    }
}
