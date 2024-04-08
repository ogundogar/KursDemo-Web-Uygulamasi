using Microsoft.AspNetCore.Identity;

namespace KUSYS_Demo_Web_Uygulamasi.Models.Entities.Identity
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<TbAuthorityLevel>? AuthorityLevels { get; set; }
    }
}
