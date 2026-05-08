using ContentVnptApplication.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext() : base("name=ContentVNPTDb",throwIfV1Schema: false) { }

    public static AppDbContext Create()
    {
        return new AppDbContext();
    }

    public DbSet<PostCategory> PostCategories { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Banner> Banners { get; set; }
}