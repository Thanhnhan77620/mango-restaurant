using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Mongo.Services.Identity.DbContexts
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>//ApplicationUser:IdentityUser
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }
    }
}
