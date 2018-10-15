using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace Aguila.Models
{
    public class ApplicationRoleDto :IdentityRole
    {
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IPAddress { get; set; }
    }
}
