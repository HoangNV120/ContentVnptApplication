using System.Web.Mvc;
using ContentVnptApplication.Services;

namespace ContentVnptApplication.Controllers
{
    public class BaseController : Controller
    {
        private PostCategoryService categoryService = new PostCategoryService();
        private PostService postService = new PostService();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Categories = categoryService.GetAll();
            base.OnActionExecuting(filterContext);
        }

        public PartialViewResult LatestPostsTicker()
        {
            var posts = postService.LatestPosts(5);

            return PartialView(
                "_LatestPostsTicker",
                posts
            );
        }
    }
}