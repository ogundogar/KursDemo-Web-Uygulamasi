using KUSYS_Demo_Web_Uygulamasi.Models.Entities;
using KUSYS_Demo_Web_Uygulamasi.Models;
using KUSYS_Demo_Web_Uygulamasi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo_Web_Uygulamasi.Repository
{
    public class RepositoryCourse : IRepositoryCourse
    {
        readonly CourseDbContext _context;
        public RepositoryCourse(CourseDbContext context)
        {
            _context = context;
        }
        public IQueryable<Course> Get()
        {
            var result = _context.Courses.FromSqlRaw("ListCourse");
            return result;
        }
        public IQueryable<Course> Get(int Id)
        {
            var result = _context.Courses.FromSqlRaw($"SelectCourseId {Id}");
            return result;
        }
        public async Task<bool> Add(string CourseName, int Level)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[CreateCourse] @CourseName={CourseName}, @Level={Level}");
            return true;
        }

        public async Task<bool> Remove(int Id)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[DeleteCourse]  @Id={Id}");
            return true;

        }

        public async Task<bool> Update(int Id, string CourseName, int Level)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC [dbo].[UpdateCourse]  @Id={Id},@CourseName={CourseName}, @Level={Level}");
            return true;
        }
    }
}
