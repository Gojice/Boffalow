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
        public static InstaUserShortList Followers { get; private set; }
        public static InstaUserShortList Followings { get; private set; }
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
            var result = await UserWorkation.InstaApi.UserProcessor.GetUserFollowersAsync(UserWorkation.User ,P);
            if (result.Succeeded)
            {
                Followers = result.Value;
                AllFollowers = result.Succeeded;
            }
            else
            {

            }

        }


        static public async void AllFollowing()
        {
            PaginationParameters P = PaginationParameters.Empty;
            var result = await UserWorkation.InstaApi.UserProcessor.GetUserFollowingAsync(UserWorkation.User, P);
            Followings = result.Value;
            AllFollowings = result.Succeeded;
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
