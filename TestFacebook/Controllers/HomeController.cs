namespace TestFacebook.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using TestFacebook.Managers;
    using TestFacebook.Models;
    using TestFacebook.Repositories;

    public class HomeController : Controller
    {
        private readonly FacebookManager facebookManager;

        private readonly FacebookUserRepository facebookUserRepository;

        public HomeController()
        {
            this.facebookManager = new FacebookManager();
            this.facebookUserRepository = new FacebookUserRepository();
        }

        public ActionResult Index()
        {
            IQueryable<FacebookUser> viewModel = this.facebookUserRepository.Get();
            return this.View(viewModel);
        }

        public ActionResult AuthorizeWithFacebook()
        {
            string url = this.Url.Action("FacebookCallback", "Facebook", null, this.Request.Url.Scheme);
            string loginUrl = this.facebookManager.GetLoginUrl(url);

            return this.Redirect(loginUrl);
        }
    }
}