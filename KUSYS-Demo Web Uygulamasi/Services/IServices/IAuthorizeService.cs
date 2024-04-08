using KUSYS_Demo_Web_Uygulamasi.DTOs;

namespace KUSYS_Demo_Web_Uygulamasi.Services.IServices
{
    public interface IAuthorizeService
    {
        List<MenuDTO> GetAuthorizeDefinitionEndpoints(Type type);
    }
}
