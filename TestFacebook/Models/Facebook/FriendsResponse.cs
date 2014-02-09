namespace TestFacebook.Models.Facebook
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class FriendsResponse
    {
        [JsonProperty("data")]
        public List<Friend> Data { get; set; }
    }
}