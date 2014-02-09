namespace TestFacebook.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Configuration;
    using System.Web.Script.Serialization;
    using System.Web.UI.WebControls;

    using Facebook;

    using Newtonsoft.Json;

    using TestFacebook.Models.Facebook;

    public class FacebookManager
    {
        private readonly string clientId;

        private readonly string clientSecret;

        private readonly FacebookClient facebookClient;

        public FacebookManager()
        {
            this.facebookClient = new FacebookClient();
            this.clientId = WebConfigurationManager.AppSettings["ClientId"];
            this.clientSecret = WebConfigurationManager.AppSettings["ClientSecret"];
        }

        public string GetLoginUrl(string redirectUri)
        {
            Uri loginUrl = this.facebookClient.GetLoginUrl(
                new
            {
                client_id = this.clientId,
                client_secret = this.clientSecret,
                redirect_uri = redirectUri,
                response_type = "code",
                scope = "user_birthday,email,user_friends,user_location,user_photos"
            });

            return loginUrl.AbsoluteUri;
        }

        public string GetToken(string code, string redirectUri)
        {
            dynamic result = this.facebookClient.Post(
                "oauth/access_token",
                new
                    {
                        client_id = this.clientId,
                        client_secret = this.clientSecret,
                        redirect_uri = redirectUri,
                        code
                    });

            return result.access_token;
        }

        public async Task<UserInfo> GetUserInfo(string token)
        {
            this.facebookClient.AccessToken = token;
            object userInfoObject = await this.facebookClient.GetTaskAsync("me?fields=id,name,picture,birthday,location,picture");
            var serializer = new JavaScriptSerializer();
            var userInfo = serializer.Deserialize<UserInfo>(userInfoObject.ToString());

            return userInfo;
        }

        public async Task<List<Friend>> GetUserFriends(string token, int limit, int offset)
        {
            this.facebookClient.AccessToken = token;
            object friendsObject = await this.facebookClient.GetTaskAsync(string.Format("me/friends?fields=name,id,gender&limit={0}&offset={1}", limit, offset));
            var serializer = new JavaScriptSerializer();
            var friendsResponse = serializer.Deserialize<FriendsResponse>(friendsObject.ToString());

            return friendsResponse.Data;
        }

        public async Task<CurrentLocation> GetUserLocation(string token)
        {
            this.facebookClient.AccessToken = token;
            object curentLocationObject = await this.facebookClient.GetTaskAsync("fql", new { q = "SELECT current_location FROM user WHERE uid=me()" });
            var response = JsonConvert.DeserializeObject<CurrentLocationResponse>(curentLocationObject.ToString());

            if (response.Data != null && response.Data.FirstOrDefault() != null)
            {
                return response.Data.FirstOrDefault().CurrentLocation;
            }

            return new CurrentLocation();
        }
    }
}