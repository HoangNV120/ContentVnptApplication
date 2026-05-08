using ContentVnptApplication.Models;
using System.Web.Mvc;

public class BannerController : Controller
{
    private BannerService bannerService = new BannerService();

    public PartialViewResult LatestBanner()
    {
        var banner = bannerService.LatestBanner();

        return PartialView("_MainBannerPartial", banner);
    }
}