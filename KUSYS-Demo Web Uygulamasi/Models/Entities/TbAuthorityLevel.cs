using KUSYS_Demo_Web_Uygulamasi.Enums;
using KUSYS_Demo_Web_Uygulamasi.Models.Entities.Common;
using KUSYS_Demo_Web_Uygulamasi.Models.Entities.Identity;

namespace KUSYS_Demo_Web_Uygulamasi.Models.Entities
{
    public class TbAuthorityLevel:BaseEntity
    {
        public string AuthorityLevel { get; set; }
        public ICollection<AppRole>? Roles { get; set; }
    }
}
