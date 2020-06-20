using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace VirusAlertAPI.Controllers
{
    public class PersonalController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                DB.DataContext db = new DB.DataContext();
                var usr = db.AppUsers.Where(x => x.usrEmail == User.Identity.Name).FirstOrDefault();
                Models.UserInfoModel model = new Models.UserInfoModel();
                model.Name = usr.usrName;
                model.Email = usr.usrEmail;
                model.Oms = usr.usrOms;
                return View(model);
            }
            else
            {
                return Redirect("~/");
            }
        }
    }
}
