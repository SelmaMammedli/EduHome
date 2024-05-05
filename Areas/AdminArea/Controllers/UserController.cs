using EduHome.Areas.AdminArea.Views.ViewModels.Event;
using EduHome.Areas.AdminArea.Views.ViewModels.Role;
using EduHome.DAL;
using EduHome.Enums;
using EduHome.Extensions;
using EduHome.Helper;
using EduHome.Models;
using EduHome.ViewModels.UserVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        public UserController(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;

        }

        public IActionResult Index(string search)
        {

            var users = search is null ? _userManager.Users
               .AsNoTracking()
               .ToList() :
               _userManager.Users
               .AsNoTracking()
               .Where(u => u.UserName.ToLower().Contains(search.ToLower()) /*&& !u.IsActive*/)
               .ToList();
            return View(users);

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(UserCreateVM registerVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = new();
            user.FullName = registerVM.FullName;
            user.Email = registerVM.Email;
            user.UserName = registerVM.UserName;
            user.IsActive = true;
            IdentityResult result = await _userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);

            }
            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
            //email confirmation
            
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(string?id)
        {

            if (id == null) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            UserUpdateVM userUpdateVM = new UserUpdateVM();
            userUpdateVM.FullName = user.FullName;
            userUpdateVM.UserName = user.UserName;
            userUpdateVM.Email = user.Email;
          
          
            return View(userUpdateVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(string? id, UserUpdateVM userUpdateVM)
        {
            if (id == null) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            user.FullName = userUpdateVM.FullName;
            user.UserName = userUpdateVM.UserName;
            user.Email = userUpdateVM.Email;

            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
       
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");


        }
        public async Task<IActionResult> Detail(string id)
        {
            if (id == null) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var roles = await _userManager.GetRolesAsync(user);
            UserDetailVM userDetailVM = new();
            userDetailVM.User = user;
            userDetailVM.UserRoles = roles;
            return View(userDetailVM);
        }
        public async Task<IActionResult> IsActive(string id)
        {
            if (id == null) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            user.IsActive = !user.IsActive;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
    }
}
