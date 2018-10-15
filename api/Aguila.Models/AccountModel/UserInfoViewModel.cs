namespace Aguila.Models.AccountModel
{
    public class UserInfoViewModel
    {
        public string Email { get; set; }
        public string UserId { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }
}
