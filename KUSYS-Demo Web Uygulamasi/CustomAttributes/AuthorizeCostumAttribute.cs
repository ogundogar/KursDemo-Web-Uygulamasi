using KUSYS_Demo_Web_Uygulamasi.Enums;

namespace KUSYS_Demo_Web_Uygulamasi.CustomAttributes
{
    public class AuthorizeCostumAttribute : Attribute
    {
        public AuthorityLevel AuthorityLevel { get; set; }
    }
}
