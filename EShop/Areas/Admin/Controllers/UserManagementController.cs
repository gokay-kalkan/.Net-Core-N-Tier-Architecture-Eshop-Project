using DataAccessLayer.Data;
using EShop.Areas.Admin.Models;
using EShop.Models;
using EntityLayer.Entity;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserManagementController : Controller
    {
        private UserManager<UserAdmin> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        DataContext db = new DataContext();

        public UserManagementController(UserManager<UserAdmin> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var list = _userManager.Users.ToList();
            return View(list);
        }

        public IActionResult UserCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreate(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserAdmin user = new UserAdmin
            {
                Email = model.Email,
                UserName = model.UserName,
                FullName = model.FullName

            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                TempData["kayitmesaj"] = "Başarıyla Yeni Kullanıcı Eklendi";
                return RedirectToAction("Index");
            }
            else
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e.Description));
                return View(model);
            }

        }

        public async Task<IActionResult> UserDelete(string id)
        {

            var user = db.UserAdmins.Where(x => x.Id == id).FirstOrDefault();

            await _userManager.DeleteAsync(user);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult RoleList()
        {
            var rolelist = _roleManager.Roles.ToList();

            return View(rolelist);
        }
        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RoleCreate(RoleViewModel roleViewModel)
        {
            IdentityRole ıdentityRole = new IdentityRole();

            ıdentityRole.Name = roleViewModel.Name;

            IdentityResult result = _roleManager.CreateAsync(ıdentityRole).Result;

            if (result.Succeeded)
            {
                return RedirectToAction("RoleList");
            }

            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(roleViewModel);
        }

        public IActionResult RoleUpdate(string id)
        {
            IdentityRole role = _roleManager.FindByIdAsync(id).Result;


            return View(role.Adapt<RoleViewModel>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RoleUpdate(RoleViewModel roleViewModel)
        {
            IdentityRole role = _roleManager.FindByIdAsync(roleViewModel.Id).Result;


            if (role != null)
            {
                role.Name = roleViewModel.Name;

                IdentityResult result = _roleManager.UpdateAsync(role).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                        ViewBag.description = item.Description;
                    }
                }
            }
            return View(roleViewModel);
        }

        public async Task<IActionResult> RoleAssign(string id)
        {
            UserAdmin user = await _userManager.FindByIdAsync(id);

            TempData["userid"] = id;

            ViewBag.FullName = user.FullName;

            IQueryable<IdentityRole> roles = _roleManager.Roles;

            List<string> userroles = _userManager.GetRolesAsync(user).Result as List<string>;

            List<RoleAssignViewModel> roleAssignViewModels = new List<RoleAssignViewModel>();

            foreach (var role in roles)
            {

                RoleAssignViewModel r = new RoleAssignViewModel();
                if (userroles.Contains(role.Name))
                {
                    r.Id = role.Id;
                    r.Name = role.Name;
                    r.Exist = true;


                }
                else
                {
                    r.Id = role.Id;
                    r.Name = role.Name;
                    r.Exist = false;
                }

                roleAssignViewModels.Add(r);
            }
            return View(roleAssignViewModels);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleAssign(List<RoleAssignViewModel> roleAssignViewModels)
        {
            UserAdmin user = _userManager.FindByIdAsync(TempData["userid"].ToString()).Result;
            foreach (var item in roleAssignViewModels)
            {
                if (item.Exist)
                {
                    await _userManager.AddToRoleAsync(user, item.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.Name);
                }
            }

            return RedirectToAction("Index");

        }

    }
}

