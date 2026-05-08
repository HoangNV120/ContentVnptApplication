using ContentVnptApplication.Models;
using ContentVnptApplication.Services;
using System.Web.Mvc;

namespace ContentVnptApplication.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class AdminPostCategoryController : Controller
    {
        private PostCategoryService categoryService = new PostCategoryService();

        public ActionResult Index(string keyword, int page = 1)
        {
            int pageSize = 10;

            var categories = categoryService.GetPaged(keyword, page, pageSize);

            ViewBag.Keyword = keyword;

            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(PostCategory model)
        {
            var list = categoryService.GetAll();

            if (categoryService.IsExistTitle(model.Title, model.Id))
            {
                ViewBag.Error = "Tên danh mục đã tồn tại!";
                return View("Index", list);
            }

            if (model.Id == 0)
            {
                categoryService.Create(model);
                TempData["Success"] = "Thêm danh mục bài viết thành công!";
            }
            else
            {
                var ok = categoryService.Update(model);
                if (!ok) return HttpNotFound();
                TempData["Success"] = "Sửa danh mục bài viết thành công!";
            }

            return RedirectToAction("Index", "AdminPostCategory", new { area = "Admin" });
        }

        public ActionResult Delete(int id)
        {
            categoryService.Delete(id);
            TempData["Success"] = "Xóa danh mục bài viết thành công!";
            return RedirectToAction("Index", "AdminPostCategory", new { area = "Admin" });

        }
    }
}