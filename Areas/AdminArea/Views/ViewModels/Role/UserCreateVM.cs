using System.ComponentModel.DataAnnotations;

namespace EduHome.Areas.AdminArea.Views.ViewModels.Role
{
    public class UserCreateVM
    {
        [Required, MaxLength(25)]
        public string FullName { get; set; }
        [Required, MaxLength(25)]
        public string UserName { get; set; }
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
