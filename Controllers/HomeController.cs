using System.Linq;
using System.Web.Mvc;
using ContentVnptApplication.Controllers;
using ContentVnptApplication.Models;
using ContentVnptApplication.Services;

public class HomeController : BaseController
{
    private PostService postService = new PostService();
    private PostCategoryService categoryService = new PostCategoryService();

    public ActionResult Index(int page = 1)
    {
        var result = postService.GetPaged(page, 5);

        ViewBag.Categories = categoryService.GetAll();

        return View(result);
    }
}