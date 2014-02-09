namespace TestFacebook.Models.Facebook
{
    using Newtonsoft.Json;

    public class CurrentLocation
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
    }
}