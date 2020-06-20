using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirusAlertAPI.Helpers;
using VirusAlertAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using VirusAlertAPI.Models;
using VirusAlertAPI.Providers;
using VirusAlertAPI.Results;



namespace VirusAlertAPI.Controllers
{
    public class ГлавнаяController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        public ГлавнаяController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ГлавнаяController()
        {

        }

        public ActionResult Index()
        {
            ViewBag.Title = "Стоп Грипп";

            DB.DataContext db = new DB.DataContext();

            Models.HomeModel model = new Models.HomeModel();
            var dir = HttpRuntime.AppDomainAppVirtualPath;
            model.Map = StringHelper.Combine(dir, "Content/images/") + "map20200519.jpg";
            model.Diagram = StringHelper.Combine(dir, "Content/images/") + "Сhart2200619.png";
            var virus = db.VirusToday(1);
            model.Data = new Models.VirusDataModel() { Infected = virus.vdInfectTotal, Health = virus.vdHealthTotal, Dead = virus.vdDeathTotal };

            var url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            var ingpath = StringHelper.Combine(dir, "Content/images/");
            model.News = db.news.Where(x => x.newRegId == 1).Select(y => 
            new Models.NewsModel() {Image = ingpath + y.newImgLink,
            Text = y.newUrl, FullText=y.newText, Url= url + "/Главная/Новости/" + y.newId.ToString()}).ToList();

            model.Limits = new List<Models.LimitModel>();
            var ln = db.Limits.ToList()[0];
            model.Limits.Add(new Models.LimitModel() { Icon= StringHelper.Combine(dir, "Content/images/") + ln.lmImage, Text=ln.lmDesc});
            ln = db.Limits.ToList()[2];
            model.Limits.Add(new Models.LimitModel() { Icon = StringHelper.Combine(dir, "Content/images/") + ln.lmImage, Text = ln.lmDesc });

            return View(model);
        }

        public ActionResult Login()
        {
            var model = new Models.LoginModel();
            model.Email = @"СергейСидоров@почта.рус";
            model.Password = "a-sA1234567";
            var url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            model.Uri = url + "/Token";

            return View(model);
        }

        public ActionResult Registration()
        {
            Models.RegistryModel model = new Models.RegistryModel();
            return View(model);
        }

        public ActionResult DoRegistration(string name, string oms, string email, string pass, string pass2)
        {
            Models.RegistryModel model = new Models.RegistryModel();
            model.Name = name;
            model.Oms = oms;
            model.Email = email;

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = UserManager.Create(user, model.Password);

            if (!result.Succeeded)
            {
                model.Error = result.Errors.FirstOrDefault();
                return View(model);
            }

            return Redirect("~/");
            
        }

        public ActionResult Новости(int id)
        {
            DB.DataContext db = new DB.DataContext();
            Models.NewsModel model = new Models.NewsModel();
            var url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            var dir = HttpRuntime.AppDomainAppVirtualPath;
            var imgPath = StringHelper.Combine(dir, "Content/images/");
            model = db.news.Where(x => x.newId == id).Select(y =>
            new Models.NewsModel()
            {
                Image = imgPath + y.newImgLink,
                Text = y.newUrl,
                FullText = y.newText,
                Url = url + "/Главная/Новости/" + y.newId.ToString()
            }).FirstOrDefault();

            return View(model);
        }

        

    }
}
