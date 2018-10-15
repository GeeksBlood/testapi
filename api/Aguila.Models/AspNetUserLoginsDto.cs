namespace Aguila.Models
{
    public partial class AspNetUserLoginsDto
    {
        public int Id { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserId { get; set; }
    }
}
