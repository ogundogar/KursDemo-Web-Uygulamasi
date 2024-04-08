using KUSYS_Demo_Web_Uygulamasi.Models.Entities;
using KUSYS_Demo_Web_Uygulamasi.Models;
using KUSYS_Demo_Web_Uygulamasi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using KUSYS_Demo_Web_Uygulamasi.DTOs;
using System.Data;

namespace KUSYS_Demo_Web_Uygulamasi.Repository
{
    public class RepositoryCourse : IRepositoryCourse
    {
        readonly CourseDbContext _context;
        public RepositoryCourse(CourseDbContext context)
        {
            _context = context;
        }
        public DbSet<TbCourse> Table => _context.Set<TbCourse>();
        public IQueryable<TbCourse> Get()
        {
            var result = _context.Courses.FromSqlRaw("ListCourse");
            return result;
        }
        public IQueryable<TbCourse> Get(int Id)
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
    }
}
