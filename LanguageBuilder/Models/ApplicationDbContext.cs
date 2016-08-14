using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LanguageBuilder.Models
{
    

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<LanguageBuilder.Models.DictWord> DictWords { get; set; }

        public System.Data.Entity.DbSet<LanguageBuilder.Models.UserWord> UserWords { get; set; }

        public System.Data.Entity.DbSet<LanguageBuilder.Models.Student> Students { get; set; }

        
        
    }
}