using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstagramApiSharp;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using Newtonsoft.Json;

namespace Pixo
{
    static class  FollowerAnalyser
    {
        private static IResult<InstaUserShortList> result;

        public static InstaUserShortList Followers = new InstaUserShortList();
        public static IResult<InstaUserShortList> fresult { get; private set; }
        public static InstaUserShortList Followings = new InstaUserShortList();
        public static List<InstaUserShort> Diffrent { get; private set; }
        public static List<InstaUserShort> Equals { get; private set; }
        public static List<InstaUserShort> Fans { get; private set; }
        public static List<InstaUserShort> Whait { get; private set; }

        public static bool AllFollowings { get; private set; }
        public static bool AllFollowers { get; private set; }
        public static bool Diffrents { get; private set; }


        static public async void AllFollower()
        {
            PaginationParameters P = PaginationParameters.Empty;

                result = await UserWorkation.InstaApi.UserProcessor.GetUserFollowersAsync(UserWorkation.User, P);
                if (result.Succeeded)
                {
                Followers = result.Value;
                }
            AllFollowers = result.Succeeded;
        }


        static public async void AllFollowing()
        {
            PaginationParameters P = PaginationParameters.Empty;

                fresult = await UserWorkation.InstaApi.UserProcessor.GetUserFollowingAsync(UserWorkation.User, P);
            if (fresult.Succeeded)
            {
                Followings = fresult.Value;
            }
                
            AllFollowings = fresult.Succeeded;
        }

        static public async void Diffrensial()
        {
            while(!AllFollowings && !AllFollowers)
            {
                await Task.Delay(100);
            }

            var dif = Followings.Intersect(Followers).ToList();

            Diffrent = Followings.Except(Followers).ToList();
            Diffrents = true;
        
            Equals = Followers.Intersect(Followings).ToList();

            Fans = Followers.Except(Followings).ToList();

        }

        static public  void WhiteList()
        {
            var json = JsonConvert.SerializeObject(Followings);
        }
    }
}
