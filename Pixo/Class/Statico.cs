using InstagramApiSharp.Classes;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixo
{
    static class Statico
    {
        public static int NotifDelay = 5000;
        public static InAppNotification Notifer { set; get; }

        //Lohin Data:
        public static IResult<InstaChallengeRequireVerifyMethod> Challenge { get; set; }
    }
}
