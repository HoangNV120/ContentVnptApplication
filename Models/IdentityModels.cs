using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace ContentVnptApplication.Models { 

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public string Address { get; set; }
    public bool IsBanned { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }

    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    {
        var userIdentity = await manager.CreateIdentityAsync(
            this,
            DefaultAuthenticationTypes.ApplicationCookie
        );

        userIdentity.AddClaim(new Claim("FullName", this.FullName ?? ""));
        userIdentity.AddClaim(new Claim("Address", this.Address ?? ""));
   

        return userIdentity;
    }
}
}