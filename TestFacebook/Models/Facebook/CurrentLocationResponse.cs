namespace TestFacebook.Models.Facebook
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class CurrentLocationResponse
    {
        [JsonProperty("data")]
        public List<CurrentLocationArrayElement> Data { get; set; }
    }
}