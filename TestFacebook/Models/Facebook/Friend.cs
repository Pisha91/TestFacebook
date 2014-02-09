namespace TestFacebook.Models.Facebook
{
    using Newtonsoft.Json;

    public class Friend
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }
    }
}