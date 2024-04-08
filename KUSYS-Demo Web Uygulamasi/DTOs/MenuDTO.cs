namespace KUSYS_Demo_Web_Uygulamasi.DTOs
{
    public class MenuDTO
    {
        public string Name { get; set; }
        public List<ActionDTO> Actions { get; set; } = new();
    }
}
