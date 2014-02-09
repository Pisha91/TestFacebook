namespace TestFacebook.Models.Facebook
{
    using Newtonsoft.Json;

    public class CurrentLocationArrayElement
    {
        [JsonProperty("current_location")]
        public CurrentLocation CurrentLocation { get; set; }
    }
}