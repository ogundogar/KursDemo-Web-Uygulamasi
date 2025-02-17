﻿using KUSYS_Demo_Web_Uygulamasi.Models.Entities.Common;
using KUSYS_Demo_Web_Uygulamasi.Models.Entities.Identity;

namespace KUSYS_Demo_Web_Uygulamasi.Models.Entities
{
    public class TbCourse : BaseEntity
    {
        public string? CourseCode { get; set; }
        public string CourseName { get; set; }
        public int Level { get; set; }
        public ICollection<AppUser> AppUser { get; set; }
    }
}
