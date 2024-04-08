using KUSYS_Demo_Web_Uygulamasi.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo_Web_Uygulamasi.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeServiceController : ControllerBase
    {
        readonly IAuthorizeService _authorizeService;

        public AuthorizeServiceController(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;
        }
        //Controllar'da gönderilen istekleri görüntülemek için
        [HttpGet]
        public IActionResult GetAuthorizeDefinitionEndpoints()
        {
            var datas = _authorizeService.GetAuthorizeDefinitionEndpoints(typeof(Program));
            return Ok(datas);
        }
    }
}
