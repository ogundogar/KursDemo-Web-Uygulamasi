using KUSYS_Demo_Web_Uygulamasi.DTOs;
using KUSYS_Demo_Web_Uygulamasi.Models.Entities.Identity;

namespace KUSYS_Demo_Web_Uygulamasi.Services.IServices
{
    public interface ILoginService
    {
        Task<TokenDTO> Login(string userNameOrEmail, string password);
    }
}
