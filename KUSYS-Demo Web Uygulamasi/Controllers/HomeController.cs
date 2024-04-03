using KUSYS_Demo_Web_Uygulamasi.DTOs;
using KUSYS_Demo_Web_Uygulamasi.Services.IServices;
using Microsoft.AspNetCore.Mvc;


namespace KUSYS_Demo_Web_Uygulamasi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly ILoginService _loginService;

        public HomeController(ILogger<HomeController> logger, ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        public async Task<IActionResult> Index()
        {
            TokenDTO token = await _loginService.Login("deneme@deneme.com", "12345");
            return View();
        }
    }
}