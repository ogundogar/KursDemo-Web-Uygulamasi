using KUSYS_Demo_Web_Uygulamasi.CustomAttributes;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using KUSYS_Demo_Web_Uygulamasi.Repository.IRepository;
using System.Reflection;

namespace KUSYS_Demo_Web_Uygulamasi.Filters
{
    public class RolePermissionFilter : IAsyncActionFilter
    {
        readonly IRepositoryAppUsers _repository;

        public RolePermissionFilter(IRepositoryAppUsers repository)
        {
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Giriş yapan kullanıcının UserName bilgisi
            //Program.cs içindeki AddAuthentication içindeki ClaimTypes tarafından getiriliyor.
            var name = context.HttpContext.User.Identity?.Name;
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
                //İstek hangi action'a gidiyorsa onunla ilgili bilgileri yakalıyor.
                var attribute = descriptor.MethodInfo.GetCustomAttribute(typeof(AuthorizeCostumAttribute)) as AuthorizeCostumAttribute;
                //attribute null ise çalışmaya devam etsin
                if(attribute==null)
                    await next();
                //attribu null değil ise attribute costom attribute değeri attributeType değişkenine atansın
                if (attribute != null)
                {
                    var attributeType = attribute.AuthorityLevel;
                    //attribute type null değil ise AppUser'ın yetkisi kontrol edilir.
                    if (attributeType != null)
                    {
                        bool hasRole = await _repository.HasRolePermissionToAttributesAsync(name, attributeType.ToString());
                    //AppUser sınıfı içindeki HasRolePermissionToAttributesAsync fonksiyonundan true geliyorsa devam etsin.
                    if (hasRole)
                            await next();
                    } 
                }
           
            context.Result = new UnauthorizedResult();
        }
    }
}
