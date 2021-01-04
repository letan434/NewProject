using System;
using System.Threading.Tasks;
using NewProject.ViewModels.Common;
using NewProject.ViewModels.System;
using NewProject.ViewModels.System.common;
using NewProject.ViewModels.System.Users;
using NewProject.WebPortal.Data.Entities;

namespace NewProject.WebPortal.Services
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authencate(LoginRequest request);
        Task<ApiResult<bool>> Register(RegisterRequest request);
        Task<ApiResult<bool>> Update(string id, UserUpdateRequest request);
        Task<ApiResult<UserVm>> GetById(string id);
        Task<ApiResult<bool>> Delete(string id);
        Task<ApiResult<bool>> RoleAssign(string id, RoleAssignRequest request);
        Task<ApiResult<PagedResult<UserVm>>> GetUsersPagings(GetUserPagingRequest request);
    }
}
