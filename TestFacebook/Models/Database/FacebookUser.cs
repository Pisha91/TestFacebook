namespace TestFacebook.Models
{
    /// <summary>
    /// The facebook data.
    /// </summary>
    public class FacebookUser
    {
        public int Id { get; set; }

        public long UserId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string PictureUrl { get; set; }

        public int FriendsCount { get; set; }

        public float MaleFriendsInPercentages { get; set; }

        public float FemaleFriendsInPercentages { get; set; }
    }
}