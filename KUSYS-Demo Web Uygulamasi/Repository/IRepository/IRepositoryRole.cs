using KUSYS_Demo_Web_Uygulamasi.Enums;
using KUSYS_Demo_Web_Uygulamasi.Models.Entities;
using KUSYS_Demo_Web_Uygulamasi.Models.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace KUSYS_Demo_Web_Uygulamasi.Repository.IRepository
{
    public interface IRepositoryRole
    {
        IQueryable<AppRole> Get();
        Task<AppRole> Get(string id);
        Task<bool> Add(string name);
        Task<bool> Remove(string id);
        Task<bool> Update(string id, string name);
        Task<bool> AssignRoleAuthorityLevelAsync(string AppRoleId, AuthorityLevel authorityLevel);
        Task<string> GetAppRoleAuthorityLevelAsync(string RoleName);
    }
}
