using EduHome.Models;

namespace EduHome.Areas.AdminArea.Views.ViewModels.Role
{
    public class UserDetailVM
    {
        public AppUser User { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
