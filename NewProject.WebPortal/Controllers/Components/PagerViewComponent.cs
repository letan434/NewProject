using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewProject.ViewModels.System.common;

namespace NewProject.WebPortal.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
