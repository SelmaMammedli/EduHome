using EduHome.Models;
using Microsoft.AspNetCore.Identity;

namespace EduHome.Areas.AdminArea.Views.ViewModels.Role
{
    public class ChangeRoleVM
    {
        public List<IdentityRole> Roles { get; set; }
        public IList<string> UserRoles { get; set; }
        public AppUser User { get; set; }
    }
}
