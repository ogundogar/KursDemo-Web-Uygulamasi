using Microsoft.AspNetCore.Identity;

namespace KUSYS_Demo_Web_Uygulamasi.Models.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Role { get; set; }
        public ICollection<TbCourse> Course { get; set; }
    }
}
