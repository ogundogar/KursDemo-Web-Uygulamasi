using KUSYS_Demo_Web_Uygulamasi.Enums;
using KUSYS_Demo_Web_Uygulamasi.Models.Entities;
using KUSYS_Demo_Web_Uygulamasi.Models.Entities.Identity;
using KUSYS_Demo_Web_Uygulamasi.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace KUSYS_Demo_Web_Uygulamasi.Repository
{
    public class RepositoryRole : IRepositoryRole
    {
        readonly RoleManager<AppRole> _roleManager;
        readonly IRepositoryAuthorityLevel _repositoryAuthorityLevel;

        public RepositoryRole(RoleManager<AppRole> roleManager, IRepositoryAuthorityLevel repositoryAuthorityLevel)
        {
            _roleManager = roleManager;
            _repositoryAuthorityLevel = repositoryAuthorityLevel;
        }
        public IQueryable<AppRole> Get()
        {
            var query = _roleManager.Roles;
            return query;
        }
        public async Task<AppRole> Get(string id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            return role;
        }
        public async Task<bool> Add(string name)
        {
            
            IdentityResult result = await _roleManager.CreateAsync(new() { Name = name });
            return result.Succeeded;
        }
        public async Task<bool> Remove(string id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            IdentityResult result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }
        public async Task<bool> Update(string id, string name)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            role.Name = name;
            IdentityResult result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }
        public async Task<bool> AssignRoleAuthorityLevelAsync(string AppRoleId, AuthorityLevel authorityLevel)
        {
            TbAuthorityLevel? AuthorityLevel = await _repositoryAuthorityLevel.Table.Include(p => p.Roles).FirstOrDefaultAsync(p => p.AuthorityLevel == authorityLevel.ToString());
            AppRole role = await Get(AppRoleId);
            AuthorityLevel.Roles.Add(role);
            await _repositoryAuthorityLevel.SaveAsync();
            return true;
        }
        public async Task<string> GetAppRoleAuthorityLevelAsync(string RoleName)
        {

            var AppRoleAuthorityLevel =await _repositoryAuthorityLevel.Table
                .Include(p => p.Roles)
                .SelectMany(authorityLevel => authorityLevel.Roles, (authorityLevel, appRole) => new
                {
                AuthorityLevel = authorityLevel.AuthorityLevel,
                RoleName = appRole.Name
                })
                .FirstOrDefaultAsync(p=>p.RoleName==RoleName);

            return AppRoleAuthorityLevel.AuthorityLevel;
        }
    }
}
