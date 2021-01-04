using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewProject.ViewModels.Common;
using NewProject.ViewModels.System.Roles;

namespace NewProject.WebPortal.Services
{
    public class RoleService :IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ApiResult<List<RoleVm>>> GetAll()
        {
            var roles = await _roleManager.Roles
               .Select(r => new RoleVm()
               {
                   Id = r.Id,
                   Name = r.Name,
               }).ToListAsync();
            return new ApiSuccessResult<List<RoleVm>>(roles);
        }
    }
}
