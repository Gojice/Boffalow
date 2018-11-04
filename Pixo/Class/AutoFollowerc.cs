using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstagramApiSharp;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;

namespace Pixo
{
    class AutoFollowerc
    {
        private int _num;
        private int _delay;
        private string _way;
        private string _X;
        private IResult<string> mediaID;
        private IResult<InstaLikersList> PostLikeer;
      

        public void Start(int number, int speed, string methoud, string source)
        {
            _delay = speed;
            _num = number;
            _way = methoud;
            _X = source;

            //_GetFollowers();

            if(methoud == "Followerofuser")
            {
                _FollowFollower();
            }
        }

        private async void _FollowFollower()
        {
           // UserWorkation.SetDelay(_delay);
            var allFollowers = await UserWorkation.InstaApi.UserProcessor.GetUserFollowersAsync(_X, PaginationParameters.MaxPagesToLoad(1));
            if (allFollowers.Succeeded)
            {
                int i = 0;
                foreach(var p in allFollowers.Value)
                {
                    try
                    {
                        var user = await UserWorkation.InstaApi.UserProcessor.GetUserAsync(p.UserName);
                        //var s = await UserWorkation.InstaApi.UserProcessor.GetUserInfoByIdAsync(p.Pk);
                        if (!user.Value.FriendshipStatus.Following)
                        {
                            var Fo = await UserWorkation.InstaApi.UserProcessor.FollowUserAsync(p.Pk);
                            if (Fo.Succeeded)
                            {
                                _num--;
                                AutoFollowPage.Lister.Items.Add(user.Value);
                                i++;
                                AutoFollowPage.tbCounter.Text = i + "";
                                await Task.Delay(_delay * 1000);
                                if (_num == 0)
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Followe Errors: " + ex);
                        await Task.Delay(60000);
                    }
                   
                }
            }

        }

        private async void _GetFollowers()
        {
            try
            {
                 mediaID = await UserWorkation.InstaApi.MediaProcessor.GetMediaIdFromUrlAsync(new Uri(_X));
                if (mediaID.Succeeded)
                {
                    GetLiker(mediaID);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Get Media ID Error: " + ex);
            }
        }

        private async void GetLiker(IResult<string> mediaID)
        {
            PostLikeer = await UserWorkation.InstaApi.MediaProcessor.GetMediaLikersAsync(mediaID.Value);
            if (PostLikeer.Succeeded)
            {
                do
                {
                    //Follow Ten user:
                    if (PostLikeer.Value.Count >= 10)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                           var Fo =  await UserWorkation.InstaApi.UserProcessor.FollowUserAsync(PostLikeer.Value[i].Pk);
                            if(Fo.Succeeded)
                            {
                                _num--;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < PostLikeer.Value.Count; i++)
                        {
                            var Fo = await UserWorkation.InstaApi.UserProcessor.FollowUserAsync(PostLikeer.Value[i].Pk);
                            if (Fo.Succeeded)
                            {
                                _num--;
                            }
                        }
                    }
                    BestNextPK(PostLikeer.Value);
                    PostLikeer = await UserWorkation.InstaApi.MediaProcessor.GetMediaLikersAsync(Best.Pk);
                } while (_num > 0);
            }
        }

        InstaMedia Best = null;
        private async void BestNextPK(InstaLikersList value)
        {
            List< IResult < InstaUser >> PKList = new List<IResult<InstaUser>>();
            if (value.Count >= 10)
            {
                for(int i = 0; i < 10; i++)
                {
                    try
                    {
                        IResult<InstaUser> X = await UserWorkation.InstaApi.UserProcessor.GetUserAsync(value[i].UserName);
                        PKList.Add(X);
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine("okListErroe: " + ex);
                    }
                }
            }
            else if(value.Count>0)
            {
                for (int i = 0; i < value.Count; i++)
                {
                    IResult<InstaUser> X = await UserWorkation.InstaApi.UserProcessor.GetUserAsync(value[i].UserName);
                    PKList.Add(X);
                }
            }
            else
            {
                PKList = null;
            }

            //Remove user without post & chise a last post with many like!
            List<IResult<InstaUser>> CleanList = new List<IResult<InstaUser>>();
            var ListOrdered = PKList.OrderBy(o => o.Value.FollowersCount);
            PaginationParameters pagination = PaginationParameters.MaxPagesToLoad(1);

            foreach (var u in ListOrdered)
            {
                var Posts = await UserWorkation.InstaApi.UserProcessor.GetUserMediaAsync(u.Value.UserName, pagination);
                    foreach (var p in Posts.Value)
                    {
                        if (p.LikesCount > 10)
                        {
                            Best = p;
                        }
                    }
            }

        }




        
    }
}
