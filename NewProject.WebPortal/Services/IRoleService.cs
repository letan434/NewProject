using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewProject.ViewModels.Common;
using NewProject.ViewModels.System.Roles;

namespace NewProject.WebPortal.Services
{
    public interface IRoleService
    {
        Task<ApiResult<List<RoleVm>>> GetAll();
    }
}
