using ContentVnptApplication.Models;
using ContentVnptApplication.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ContentVnptApplication.Services
{
    public class UserService
    {
        private AppDbContext db = new AppDbContext();
        private UserManager<ApplicationUser> userManager;

        public UserService()
        {
            userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(db)
            );
        }

        public List<ApplicationUser> GetAll()
        {
            return userManager.Users
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .ToList();
        }

        public ApplicationUser GetById(string id)
        {
            return userManager.FindById(id);
        }

        public PagedViewModel<ApplicationUser> GetPaged(string keyword, int page, int pageSize)
        {
            var query = userManager.Users.Where(x => !x.IsDeleted);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x =>
                    x.UserName.Contains(keyword) ||
                    x.Email.Contains(keyword) ||
                    x.FullName.Contains(keyword)
                );
            }

            var totalItems = query.Count();

            var items = query
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedViewModel<ApplicationUser>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems / pageSize)
            };
        }

        public IdentityResult Create(ApplicationUser model, string password)
        {
            model.CreatedAt = DateTime.Now;
            model.IsDeleted = false;

            return userManager.Create(model, password);
        }

        public IdentityResult Update(ApplicationUser model)
        {
            var user = userManager.FindById(model.Id);
            if (user == null)
                return IdentityResult.Failed("User not found");

            user.FullName = model.FullName;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Address = model.Address;
            user.UpdatedAt = DateTime.Now;

            return userManager.Update(user);
        }

        public IdentityResult Delete(string id)
        {
            var user = userManager.FindById(id);
            if (user == null)
                return IdentityResult.Failed("User not found");

            user.IsDeleted = true;
            user.DeletedAt = DateTime.Now;
            user.UpdatedAt = user.DeletedAt;

            return userManager.Update(user);
        }
        public IdentityResult SetBanStatus(string id, bool isBanned)
        {
            var user = userManager.FindById(id);
            if (user == null)
                return IdentityResult.Failed("User not found");

            user.IsBanned = isBanned;
            user.UpdatedAt = DateTime.Now;

            return userManager.Update(user);
        }

        public ApplicationUser GetCurrentUser()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return userManager.Users.FirstOrDefault(x => x.Id == userId);
        }

        public bool UpdateProfile(string userId, string fullName, string address, string phone)
        {
            var user = userManager.Users.FirstOrDefault(x => x.Id == userId);

            if (user == null) return false;

            user.FullName = fullName;
            user.Address = address;
            user.PhoneNumber = phone;
            user.UpdatedAt = DateTime.Now;
            Debug.WriteLine(user.FullName);

            var result = userManager.Update(user);

            return result.Succeeded;
        }

    }
}