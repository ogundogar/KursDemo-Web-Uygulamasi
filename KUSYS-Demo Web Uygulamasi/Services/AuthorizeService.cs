using KUSYS_Demo_Web_Uygulamasi.CustomAttributes;
using KUSYS_Demo_Web_Uygulamasi.DTOs;
using KUSYS_Demo_Web_Uygulamasi.Enums;
using KUSYS_Demo_Web_Uygulamasi.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace KUSYS_Demo_Web_Uygulamasi.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        //Attribute'lar ile işaretlenmiş olan tüm Controller'ları getirmek için 
        public List<MenuDTO> GetAuthorizeDefinitionEndpoints(Type type)
        {
            //Controllere ulaşıyoruz.
            Assembly assembly = Assembly.GetAssembly(type);
            var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));
           
            List<MenuDTO> menus = new();
            if (controllers != null)
                foreach (var controller in controllers)
                {
                    //Controller içinde AuthorizeDefinition Attribute ile işaretli olan istekleri yakalıyoruz.
                    var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                    if (actions != null)
                        foreach (var action in actions)
                        {
                            //Yakalanan Attribute'lar arasında type'ı AuthorizeDefinitionAttribute ve HttpMethodAttribute olanları yaklamak için
                            var attributes = action.GetCustomAttributes(true);
                            if (attributes != null)
                            {
                                MenuDTO menu = null;
                                //Attribute type'ı AuthorizeDefinitionAttribute olanı yakalamak için
                                var authorizeDefinitionAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                                if (!menus.Any(m => m.Name == authorizeDefinitionAttribute.Menu))
                                {
                                    menu = new() { Name = authorizeDefinitionAttribute.Menu };
                                    menus.Add(menu);
                                }
                                else
                                    menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitionAttribute.Menu);

                                ActionDTO _action = new()
                                {
                                    ActionType = Enum.GetName(typeof(ActionType), authorizeDefinitionAttribute.ActionType),
                                    Definition = authorizeDefinitionAttribute.Definition
                                };
                                //Attribute type'ı HttpMethodAttribute olanı yakalamak için
                                var httpAttribute = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                                if (httpAttribute != null)
                                    _action.HttpType = httpAttribute.HttpMethods.First();
                                else
                                    _action.HttpType = HttpMethods.Get;

                                _action.Code = $"{_action.HttpType}.{_action.ActionType}.{_action.Definition.Replace(" ", "")}";

                                menu.Actions.Add(_action);
                            }
                        }
                }
            return menus;

        }
        
    }
}
