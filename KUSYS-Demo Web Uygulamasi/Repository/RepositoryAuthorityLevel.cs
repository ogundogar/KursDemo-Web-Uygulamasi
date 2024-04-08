using KUSYS_Demo_Web_Uygulamasi.Enums;
using KUSYS_Demo_Web_Uygulamasi.Models;
using KUSYS_Demo_Web_Uygulamasi.Models.Entities;
using KUSYS_Demo_Web_Uygulamasi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo_Web_Uygulamasi.Repository
{
    public class RepositoryAuthorityLevel : IRepositoryAuthorityLevel
    {
        readonly CourseDbContext _context;

        public RepositoryAuthorityLevel(CourseDbContext context)
        {
            _context = context;
        }
        public DbSet<TbAuthorityLevel> Table => _context.Set<TbAuthorityLevel>();
        public IQueryable<TbAuthorityLevel> Get() => Table;
        public async Task<TbAuthorityLevel> Get(int Id)
        {
            var AuthorityLevel = await Table.FindAsync(Id);
            return AuthorityLevel;
        }
        public async Task<bool> Add(AuthorityLevel AuthorityLevel)
        {
            var result = await Table.AddAsync(new() { AuthorityLevel=AuthorityLevel.ToString()});
            return true;
        }
        public bool Remove(AuthorityLevel AuthorityLevel)
        {
            var result = Table.Remove(new() { AuthorityLevel=AuthorityLevel.ToString()});
            return true;
        }
        public bool Update(AuthorityLevel AuthorityLevel)
        {
            var result = Table.Update(new() { AuthorityLevel = AuthorityLevel.ToString() });
            return true;
        }
        public async Task<int> SaveAsync()=> await _context.SaveChangesAsync();
    }
}
