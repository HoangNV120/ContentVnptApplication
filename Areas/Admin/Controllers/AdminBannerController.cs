using ContentVnptApplication.Models;
using ContentVnptApplication.Services;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ContentVnptApplication.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class AdminBannerController : Controller
    {
        private BannerService bannerService = new BannerService();

        public ActionResult Index(string keyword, int page = 1)
        {
            int pageSize = 10;

            var data = bannerService.GetPaged(keyword, page, pageSize);

            ViewBag.Keyword = keyword;

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Banner model, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                if (ImageFile.ContentLength > 10 * 1024 * 1024)
                {
                    ModelState.AddModelError("", "File không được vượt quá 5MB");
                    return View(model);
                }

                var allowedExt = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var ext = Path.GetExtension(ImageFile.FileName).ToLower();

                if (!allowedExt.Contains(ext))
                {
                    ModelState.AddModelError("", "Chỉ cho phép JPG, PNG, WEBP");
                    return View(model);
                }

                var allowedMime = new[] { "image/jpeg", "image/png", "image/webp" };

                if (!allowedMime.Contains(ImageFile.ContentType))
                {
                    ModelState.AddModelError("", "File không hợp lệ");
                    return View(model);
                }
            }
            bannerService.Create(model, ImageFile);
            TempData["Success"] = "Thêm banner thành công!";

            return RedirectToAction("Index", "AdminBanner", new { area = "Admin" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Banner model, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                bannerService.Update(model, ImageFile);
                TempData["Success"] = "Cập nhật banner thành công!";
            }

            return RedirectToAction("Index", "AdminBanner", new { area = "Admin" });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            bannerService.Delete(id);
            TempData["Success"] = "Xóa banner thành công!";
            return RedirectToAction("Index", "AdminBanner", new { area = "Admin" });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetActive(int id)
        {
            bannerService.SetActiveBanner(id);
            TempData["Success"] = "Đã chọn banner hiển thị!";
            return RedirectToAction("Index", "AdminBanner", new { area = "Admin" });

        }
    }
}