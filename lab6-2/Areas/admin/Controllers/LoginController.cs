using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Security;
namespace lab6_2.Areas.admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Check(string LoginName,string PWD)
        {
            lab6_2.Areas.admin.Models.shopEntities db = new Models.shopEntities();
            List<lab6_2.Areas.admin.Models.User> lst = db.User.Where(m => m.LoginName == LoginName && m.PWD == PWD).ToList();
            if (lst.Count>=1)
            {
                FormsAuthentication.SetAuthCookie(LoginName, false);
                return Redirect("/admin/Shop/Index");
            }
            else
            {
                return Redirect("/admin/Login/Index?message=error");
            }
        }
    }
}