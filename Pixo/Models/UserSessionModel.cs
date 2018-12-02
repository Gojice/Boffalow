namespace Pixo.Models
{
    public class UserSessionModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserModel User { get; set; }
        public string RankToken { get; set; }
        public string CsrfToken { get; set; }
        public string FacebookUserId { get; set; }
    }
}
