using ContentVnptApplication.Models;
using ContentVnptApplication.Services;
using System.Web.Mvc;

namespace ContentVnptApplication.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class AdminPostController : Controller
    {
        private PostService postService = new PostService();
        private PostCategoryService categoryService = new PostCategoryService();

        public ActionResult Index(string keyword, int page = 1)
        {
            int pageSize = 10;

            var posts = postService.GetPaged(keyword, page, pageSize);

            ViewBag.Categories = categoryService.GetAll();
            ViewBag.Keyword = keyword;

            return View(posts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(
    Post model,
    string keyword,
    int page = 1)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = categoryService.GetAll();

                var vm = postService.GetPaged(keyword, page, 10);

                ViewBag.OpenModal = true;
                ViewBag.EditPost = model;
                ViewBag.Keyword = keyword;

                return View("Index", vm);
            }

            if (model.Id == 0)
            {
                postService.Create(model);

                TempData["Success"] = "Thêm bài viết thành công!";
            }
            else
            {
                var ok = postService.Update(model);

                if (!ok)
                    return HttpNotFound();

                TempData["Success"] = "Sửa bài viết thành công!";
            }

            return RedirectToAction(
                "Index",
                "AdminPost",
                new
                {
                    area = "Admin",
                    keyword = keyword,
                    page = page
                }
            );
        }

        public ActionResult Delete(int id)
        {
            postService.Delete(id);
            TempData["Success"] = "Xóa bài viết thành công!";
            return RedirectToAction("Index", "AdminPost", new { area = "Admin" });
        }
    }
}