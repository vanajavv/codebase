using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FARABI.Models;
using FARABI.DB_context;
using System.Web.Security;

namespace FARABI.Controllers
{
    public class AdminLoginController : Controller
    {
        FARABIEntities objCon = new FARABIEntities();
        // GET: AdminLogin
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(AdminLoginModel AdObj)
        {
            var _passWord = encryptPassword.textToEncrypt(AdObj.Password);
            bool Isvalid = objCon.logins.Any(x => x.Email == AdObj.EmailId && x.EmailVerification == true &&
            x.Password == _passWord);
            Session["Email"] = AdObj.EmailId;
            if (Isvalid&& objCon.logins.Any(x=>x.Role=="Admin" && x.Status=="1"))
            {
                int timeout = AdObj.Rememberme ? 60 : 5; // Timeout in minutes, 60 = 1 hour.    
                var ticket = new FormsAuthenticationTicket(AdObj.EmailId, false, timeout);
                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = System.DateTime.Now.AddMinutes(timeout);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
                return RedirectToAction("AdminHome", "AdminDash");
            }
            else if(Isvalid && objCon.logins.Any(x => x.Role == "Supplier" && x.Status == "1"))
            {
                int timeout = AdObj.Rememberme ? 60 : 5; // Timeout in minutes, 60 = 1 hour.    
                var ticket = new FormsAuthenticationTicket(AdObj.EmailId, false, timeout);
                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = System.DateTime.Now.AddMinutes(timeout);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
                return RedirectToAction("AdminHome", "AdminDash");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Information... Please try again!");
            }
            return View();
        }
    }
}