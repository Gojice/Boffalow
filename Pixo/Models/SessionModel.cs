namespace Pixo.Models
{
    public class SessionModel
    {
        public DeviceInfoModel DeviceInfo { get; set; }
        public UserSessionModel UserSession { get; set; }
        public bool IsAuthenticated { get; set; }
        public CookieInfoModel Cookies { get; set; }
        public CookieModel[] RawCookies { get; set; }
    }
}
