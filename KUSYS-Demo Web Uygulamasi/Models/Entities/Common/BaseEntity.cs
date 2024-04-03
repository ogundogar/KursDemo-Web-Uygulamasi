namespace KUSYS_Demo_Web_Uygulamasi.Models.Entities.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
