using System;
using System.Collections.Generic;

namespace NewProject.ViewModels.System.common
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Items { get; set; }
    }
}
