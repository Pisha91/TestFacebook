namespace TestFacebook.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using TestFacebook.Managers;
    using TestFacebook.Models;
    using TestFacebook.Models.Facebook;
    using TestFacebook.Repositories;

    public class FacebookController : Controller
    {
        private readonly FacebookManager facebookManager;

        private readonly FacebookUserRepository facebookUserRepository;

        public FacebookController()
        {
            this.facebookUserRepository = new FacebookUserRepository();
            this.facebookManager = new FacebookManager();
        }

        public async Task<ActionResult> FacebookCallback(string code)
        {
            int friendsCount = 0;
            const int FriendsLimit = 5000;
            string url = this.Url.Action("FacebookCallback", "Facebook", null, this.Request.Url.Scheme);
            string accessToken = this.facebookManager.GetToken(code, url);
           
            UserInfo userInfo = await this.facebookManager.GetUserInfo(accessToken);
            CurrentLocation currentLocation = await this.facebookManager.GetUserLocation(accessToken) ?? new CurrentLocation();
            List<Friend> friends = await this.facebookManager.GetUserFriends(accessToken, FriendsLimit, friendsCount);

            friendsCount = friends.Count;
            int i = 1;
            while (friendsCount == FriendsLimit * i)
            {
                List<Friend> moreFriends = await this.facebookManager.GetUserFriends(accessToken, FriendsLimit, FriendsLimit * i);
                friends.AddRange(moreFriends);
                friendsCount = friends.Count;

                i++;
            }
            
            var facebookUser = new FacebookUser
                                   {
                                       Age = userInfo.Age,
                                       UserId = userInfo.Id,
                                       Name = userInfo.Name,
                                       PictureUrl = userInfo.Picture.Data.Url,
                                       City = currentLocation.City ?? string.Empty,
                                       Country = currentLocation.Country ?? string.Empty,
                                       FriendsCount = friends.Count,
                                       FemaleFriendsInPercentages = friends.Count > 0 ? (friends.Count(x => x.Gender == "female") / (float)friends.Count) * 100 : 0,
                                       MaleFriendsInPercentages = friends.Count > 0 ? (friends.Count(x => x.Gender == "male") / (float)friends.Count) * 100 : 0
                                   };

            this.facebookUserRepository.Add(facebookUser);
            
            return this.View(facebookUser);
        }
    }
}