using ContentVnptApplication.Models;
using ContentVnptApplication.Services;
using System.Linq;
using System.Web.Mvc;


namespace ContentVnptApplication.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class AdminUserController : Controller
    {
        private UserService userService = new UserService();

        public ActionResult Index(string keyword, int page = 1)
        {
            int pageSize = 10;

            var users = userService.GetPaged(keyword, page, pageSize);

            ViewBag.Keyword = keyword;

            return View(users);
        }

        public ActionResult Delete(string id)
        {
            userService.Delete(id);
            TempData["Success"] = "Xóa người dùng thành công!";
            return RedirectToAction("Index", "AdminUser", new { area = "Admin" });
        }
        public ActionResult Ban(string id)
        {
            var result = userService.SetBanStatus(id, true);

            TempData["Success"] = result.Succeeded
                ? "Đã khóa người dùng"
                : result.Errors.FirstOrDefault();

            return RedirectToAction("Index", "AdminUser", new { area = "Admin" });
        }

        public ActionResult Unban(string id)
        {
            var result = userService.SetBanStatus(id, false);

            TempData["Success"] = result.Succeeded
                ? "Đã mở khóa người dùng"
                : result.Errors.FirstOrDefault();

            return RedirectToAction("Index", "AdminUser", new { area = "Admin" });
        }
    }
}