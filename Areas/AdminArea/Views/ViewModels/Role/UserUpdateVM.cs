using System.ComponentModel.DataAnnotations;

namespace EduHome.Areas.AdminArea.Views.ViewModels.Role
{
    public class UserUpdateVM
    {
        [Required, MaxLength(25)]
        public string FullName { get; set; }
        [Required, MaxLength(25)]
        public string UserName { get; set; }
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
    }
}
