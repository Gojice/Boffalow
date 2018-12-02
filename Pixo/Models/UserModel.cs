namespace Pixo.Models
{
    public class UserModel
    {
        public bool IsVerified { get; set; }
        public bool IsPrivate { get; set; }
        public long Pk { get; set; }
        public string ProfilePicture { get; set; }
        public object ProfilePicUrl { get; set; }
        public string ProfilePictureId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}
