namespace TestFacebook.Repositories
{
    using System.Linq;

    using TestFacebook.Models;

    public class FacebookUserRepository
    {
        private readonly ApplicationDbContext context;

        public FacebookUserRepository()
        {
            this.context = new ApplicationDbContext();
        }

        public IQueryable<FacebookUser> Get()
        {
            return this.context.FacebookUsers;
        }

        public void Add(FacebookUser user)
        {
            this.context.FacebookUsers.Add(user);
            this.context.SaveChanges();
        }
    }
}