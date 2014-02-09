namespace TestFacebook.Models.Facebook
{
    using Newtonsoft.Json;

    public class Picture
    {
        [JsonProperty("data")]
        public PictureData Data { get; set; }
    }
}