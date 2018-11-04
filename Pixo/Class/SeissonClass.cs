using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixo
{

        public class SeissonClass
        {
            public Deviceinfo DeviceInfo { get; set; }
            public Usersession UserSession { get; set; }
            public bool IsAuthenticated { get; set; }
            public Cookies Cookies { get; set; }
            public Rawcooky[] RawCookies { get; set; }
        }

        public class Deviceinfo
        {
            public string PhoneGuid { get; set; }
            public string DeviceGuid { get; set; }
            public string GoogleAdId { get; set; }
            public string RankToken { get; set; }
            public string AndroidBoardName { get; set; }
            public string AndroidBootloader { get; set; }
            public string DeviceBrand { get; set; }
            public string DeviceId { get; set; }
            public string DeviceModel { get; set; }
            public string DeviceModelBoot { get; set; }
            public string DeviceModelIdentifier { get; set; }
            public string FirmwareBrand { get; set; }
            public string FirmwareFingerprint { get; set; }
            public string FirmwareTags { get; set; }
            public string FirmwareType { get; set; }
            public string HardwareManufacturer { get; set; }
            public string HardwareModel { get; set; }
            public string Resolution { get; set; }
            public string Dpi { get; set; }
        }

        public class Usersession
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public Loggedinuser LoggedInUser { get; set; }
            public string RankToken { get; set; }
            public string CsrfToken { get; set; }
            public string FacebookUserId { get; set; }
        }

        public class Loggedinuser
        {
            public bool IsVerified { get; set; }
            public bool IsPrivate { get; set; }
            public long Pk { get; set; }
            public string ProfilePicture { get; set; }
            public object profile_pic_url { get; set; }
            public string ProfilePictureId { get; set; }
            public string UserName { get; set; }
            public string FullName { get; set; }
        }

        public class Cookies
        {
            public int Capacity { get; set; }
            public int Count { get; set; }
            public int MaxCookieSize { get; set; }
            public int PerDomainCapacity { get; set; }
        }

        public class Rawcooky
        {
            public bool IsQuotedVersion { get; set; }
            public bool IsQuotedDomain { get; set; }
            public string Comment { get; set; }
            public object CommentUri { get; set; }
            public bool HttpOnly { get; set; }
            public bool Discard { get; set; }
            public string Domain { get; set; }
            public bool Expired { get; set; }
            public DateTime Expires { get; set; }
            public string Name { get; set; }
            public string Path { get; set; }
            public string Port { get; set; }
            public bool Secure { get; set; }
            public DateTime TimeStamp { get; set; }
            public string Value { get; set; }
            public int Variant { get; set; }
            public int Version { get; set; }
        }

    }

