using KUSYS_Demo_Web_Uygulamasi.DTOs;
using KUSYS_Demo_Web_Uygulamasi.Models.Entities.Identity;
using KUSYS_Demo_Web_Uygulamasi.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace KUSYS_Demo_Web_Uygulamasi.Services
{
    public class LoginService: ILoginService
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;

        public LoginService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler = null)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<TokenDTO> Login(string userNameOrEmail, string password)
        {
            AppUser user = await _userManager.FindByNameAsync(userNameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(userNameOrEmail);
            
            if (user == null)
                throw new Exception("Hatalı Kullanıcı Adı veya Şifre... ");
           

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result == SignInResult.Success)
            {
                TokenDTO token=_tokenHandler.CreateAccessToken(60,user);
                return token;
            }
            throw new Exception("Hatalı Kullanıcı Adı veya Şifre... ");

        }
    }
}
