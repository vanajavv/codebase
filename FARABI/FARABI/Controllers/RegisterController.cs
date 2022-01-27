using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FARABI.DB_context;
using FARABI.Models;

namespace FARABI.Controllers
{
    public class RegisterController : Controller
    {
        #region entity Connection
        FARABIEntities objCon = new FARABIEntities();
        #endregion
        // GET: Register
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(login objUsr)
        {
            // email not verified on registration time    
            objUsr.EmailVerification = false;
            var IsExists = IsEmailExists(objUsr.Email);
            if (IsExists)
            {
                ModelState.AddModelError("EmailExists", "Email Aleady Exists");
                return View("Registration");
            }
            //it generate unique code       
            objUsr.ActivetionCode = Guid.NewGuid();
            //password convert    
            objUsr.Password = encryptPassword.textToEncrypt(objUsr.Password);
            objUsr.Role = "Supplier";
            objUsr.Status = "0";
            objUsr.date = DateTime.Now.ToShortDateString();
            objCon.logins.Add(objUsr);
            ViewBag.title = "FARABI Registration";
            int val = objCon.SaveChanges();
            if (val == 1)
            {
                ViewBag.Message = "Successfully Registered";
                return View();
            }
            else
            {
                ViewBag.Message = "Registeration Failed ";
                return View();
            }


        }
        #region Check Email Exists or not in DB    
        public bool IsEmailExists(string eMail)
        {
            var IsCheck = objCon.logins.Where(email => email.Email == eMail).FirstOrDefault();
            return IsCheck != null;
        }
        #endregion
    }
}