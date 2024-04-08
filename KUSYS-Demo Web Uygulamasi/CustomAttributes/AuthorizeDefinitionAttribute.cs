using KUSYS_Demo_Web_Uygulamasi.Enums;

namespace KUSYS_Demo_Web_Uygulamasi.CustomAttributes
{
    public class AuthorizeDefinitionAttribute : Attribute
    {
        public string Menu { get; set; }
        public string Definition { get; set; }
        public ActionType ActionType { get; set; }
    }
}
