﻿using KUSYS_Demo_Web_Uygulamasi.Models.Entities.Identity;
using KUSYS_Demo_Web_Uygulamasi.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo_Web_Uygulamasi.Models
{
    public class CourseDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public DbSet<TbCourse> Courses { get; set; }
        public DbSet<TbAuthorityLevel> AuthorityLevels { get; set; }

        public CourseDbContext(DbContextOptions options) : base(options)
        { }
    
    }
}
