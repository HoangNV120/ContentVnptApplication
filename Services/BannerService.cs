using ContentVnptApplication.Models;
using ContentVnptApplication.Services;
using ContentVnptApplication.ViewModel;
using System;
using System.Linq;
using System.Web;

public class BannerService
{
    private readonly AppDbContext db;
    private CloudinaryService cloudinaryService = new CloudinaryService();

    public BannerService()
    {
        db = new AppDbContext();
    }
    public Banner LatestBanner()
    {
        var activeBanner = db.Banners
            .Where(x => x.DisplayOrder == 1)
            .OrderByDescending(x => x.UpdatedAt)
            .FirstOrDefault();

        if (activeBanner != null)
            return activeBanner;

        return db.Banners
            .OrderByDescending(x => x.UpdatedAt)
            .FirstOrDefault();
    }

    public void Create(Banner model, HttpPostedFileBase imageFile)
    {
        var uploadResult = cloudinaryService.UploadImage(imageFile);

        if (uploadResult != null)
        {
            model.ImageUrl = uploadResult.SecureUrl.ToString();
            model.PublicId = uploadResult.PublicId;
        }

        model.CreatedAt = DateTime.Now;
        model.UpdatedAt = DateTime.Now;

        db.Banners.Add(model);
        db.SaveChanges();
    }

    public void Update(Banner model, HttpPostedFileBase imageFile)
    {
        var banner = db.Banners.Find(model.Id);
        if (banner == null) return;

        banner.Title = model.Title;

        if (imageFile != null)
        {
            Console.WriteLine("Ok");
            if (!string.IsNullOrEmpty(banner.PublicId))
            {
                cloudinaryService.DeleteImage(banner.PublicId);
            }

            var uploadResult = cloudinaryService.UploadImage(imageFile);

            if (uploadResult != null)
            {
                banner.ImageUrl = uploadResult.SecureUrl.ToString();
                banner.PublicId = uploadResult.PublicId;
            }
        }

        banner.UpdatedAt = DateTime.Now;

        db.SaveChanges();
    }

    public void Delete(int id)
    {
        var banner = db.Banners.Find(id);
        if (banner == null) return;

        if (!string.IsNullOrEmpty(banner.PublicId))
        {
            cloudinaryService.DeleteImage(banner.PublicId);
        }

        db.Banners.Remove(banner);

        db.SaveChanges();
    }

    public void SetActiveBanner(int id)
    {
        var allBanners = db.Banners.ToList();
        foreach (var b in allBanners)
        {
            b.DisplayOrder = 0;
        }

        var selected = db.Banners.Find(id);
        if (selected != null)
        {
            selected.DisplayOrder = 1;
        }

        db.SaveChanges();
    }

    public PagedViewModel<Banner> GetPaged(string keyword, int page, int pageSize)
    {
        var query = db.Banners.AsQueryable();

        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(x =>
                x.Title.Contains(keyword)
            );
        }

        var totalItems = query.Count();

        var items = query
            .OrderByDescending(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedViewModel<Banner>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize)
        };
    }
}