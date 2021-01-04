using System;
using System.Collections.Generic;
using NewProject.ViewModels.System.common;

namespace NewProject.ViewModels.System.Users
{
    public class RoleAssignRequest
    {
        public string Id { get; set; }
        public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
    }
}
