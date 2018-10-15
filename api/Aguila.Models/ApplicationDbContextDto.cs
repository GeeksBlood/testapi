using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aguila.Models
{
    public class ApplicationDbContextDto : IdentityDbContext<ApplicationUserDto, ApplicationRoleDto, string>
    {
        public ApplicationDbContextDto(DbContextOptions<ApplicationDbContextDto> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
