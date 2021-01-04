using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewProject.ViewModels.System.common;
using NewProject.ViewModels.System.Users;
using NewProject.WebPortal.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewProject.WebPortal.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;
        public UsersController(IUserService userService, IConfiguration configuration, IRoleService roleService)
        {
            _userService = userService;
            _configuration = configuration;
            _roleService = roleService;
            //_roleApiClient = roleApiClient;
        }
        public async Task<ActionResult> Index(string keywork, int pageIndex = 1, int pageSize = 5)
        {
            //var sessions = HttpContext.Session.GetString("Token");
            var request = new GetUserPagingRequest()
            {
                Keyword = keywork,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _userService.GetUsersPagings(request);
            ViewBag.Keywork = keywork;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.ResultObj);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if (!ModelState.IsValid) return View();
            var result = await _userService.Register(request);
            if (result.IsSuccessed)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var result = await _userService.GetById(id);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var userEdit = new UserUpdateRequest()
                {
                    Dob = user.Dob,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = id,
                    PhoneNumber = user.PhoneNumber
                };
                return View(userEdit);
            }
            else return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _userService.Update(request.Id, request);
            if (result.IsSuccessed)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var result = await _userService.GetById(id);
            return View(result.ResultObj);
        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            return View(new UserDeleteRequest
            {
                Id = id
            });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteRequest request)
        {
            if (!ModelState.IsValid) return View();
            var result = await _userService.Delete(request.Id);
            if (result.IsSuccessed) return RedirectToAction("Index");
            ModelState.AddModelError("", result.Message);
            return View(request);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public async Task<IActionResult> RoleAssign(string id)
        {
            var roleAssignRequest = await GetRoleAssignRequest(id);
            return View(roleAssignRequest);
        }
        [HttpPost]
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            if (!ModelState.IsValid) return View();
            var result = await _userService.RoleAssign(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "cap nhat thanh cong";
                return RedirectToAction("Index");

            }
            ModelState.AddModelError("", result.Message);
            var roleAssignRequest = await GetRoleAssignRequest(request.Id);
            return View(roleAssignRequest);

        }
        private async Task<RoleAssignRequest> GetRoleAssignRequest(string id)
        {
            var userObj = await _userService.GetById(id);
            var roleObj = await _roleService.GetAll();
            var roleAssignRequest = new RoleAssignRequest();
            foreach (var role in roleObj.ResultObj)
            {
                roleAssignRequest.Roles.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = userObj.ResultObj.Roles.Contains(role.Name)
                }
                );
            }
            return roleAssignRequest;
        }
    }
}
