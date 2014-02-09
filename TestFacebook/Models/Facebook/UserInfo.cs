namespace TestFacebook.Models.Facebook
{
    using System;

    using Newtonsoft.Json;

    public class UserInfo
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("birthday")]
        public DateTime BirthDay { get; set; }

        [JsonProperty("picture")]
        public Picture Picture { get; set; }

        [JsonIgnore]
        public int Age
        {
            get
            {
                DateTime currentDate = DateTime.Now;
                int age = currentDate.Year - this.BirthDay.Year;
                age = this.BirthDay.AddYears(age).Date > currentDate.Date ? age - 1 : age;

                return age;
            }
        }
    }
}