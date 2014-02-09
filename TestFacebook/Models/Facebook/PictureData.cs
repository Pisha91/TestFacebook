namespace TestFacebook.Models.Facebook
{
    using Newtonsoft.Json;

    public class PictureData
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}