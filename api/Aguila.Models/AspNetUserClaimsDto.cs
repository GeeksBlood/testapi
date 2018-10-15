namespace Aguila.Models
{
    public class AspNetUserClaimsDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual ApplicationUserDto User { get; set; }
    }
}
