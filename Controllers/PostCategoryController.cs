using ContentVnptApplication.Services;
using System.Web.Mvc;

namespace ContentVnptApplication.Controllers
{
    public class PostCategoryController : BaseController
    {
        private PostCategoryService categoryService = new PostCategoryService();
        private PostService postService = new PostService();

        public PartialViewResult Menu()
        {
            var categories = categoryService.GetMenu();
            return PartialView("_Menu", categories);
        }

        public ActionResult ByCategory(string slug, string keyword, int page = 1)
        {
            var category = categoryService.GetBySlug(slug);

            if (category == null)
                return HttpNotFound();

            var model = postService.GetPagedByCategory(
                category.Id,
                keyword,
                page,
                2
            );

            ViewBag.CategoryName = category.Title;
            ViewBag.Keyword = keyword;
            ViewBag.Slug = slug;

            return View(model);
        }
    }
}