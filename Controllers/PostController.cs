using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContentVnptApplication.Controllers;
using ContentVnptApplication.Services;

public class PostController : BaseController
{
    private PostService _postService = new PostService();

    public ActionResult Detail(string slug)
    {
        var post = _postService.GetBySlug(slug);

        if (post == null)
            return HttpNotFound();

        var relatedPosts = _postService.GetRelatedPosts(post.Id, post.PostCategoryId);

        ViewBag.RelatedPosts = relatedPosts;

        return View(post);
    }
}