using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FARABI.Models;

namespace FARABI.Controllers
{
    public class SupplierDashController : Controller
    {
        DB_context.FARABIEntities objCon = new DB_context.FARABIEntities();
        // GET: SupplierDash
        public ActionResult SupplierHome()
        {
            return View();
        }
        public ActionResult UserVerification(string id)
        {
            var IsVerify = objCon.logins.Where(u => u.ActivetionCode == new Guid(id)).FirstOrDefault();
            string date = IsVerify.date;
            DateTime next = Convert.ToDateTime(date).AddDays(5);

            if (Convert.ToDateTime(date) > next)
            {
                ViewBag.title = "FARABI";
                ViewBag.Message = "Oops. Link Expired !!";
                return RedirectToAction("Registration", "Register");

            }
            else
            {

                return View("AdminLogin", "AdminLogin");
            }


        }
        [HttpPost]
        public ActionResult UserVerification(string id, Verification obj)
        {
            bool Status = false;

            //DateTime aday = DateTime.Now;

            //DateTime next = aday.AddHours(24);


            //string msg = Session["otp"].ToString();
            //ViewBag.otp = msg;

            objCon.Configuration.ValidateOnSaveEnabled = false; // Ignor to password confirmation     
            var IsVerify = objCon.logins.Where(u => u.ActivetionCode == new Guid(id)).FirstOrDefault();

            // Verification obj = new Verification();
            //if (msg == obj.OTP)
            //{
            ViewBag.view = "OTP Verified";
            if (IsVerify != null)
            {

                IsVerify.EmailVerification = true;

                objCon.SaveChanges();
                ViewBag.Message = "Email Verification completed";
                Status = true;
            }
            else
            {
                ViewBag.Message = "Invalid Request...Email not verify";
                ViewBag.Status = false;
            }


            //}
            //else
            //{
            //    ViewBag.view = "OTP Verification Failed";
            //}
            //if (IsVerify != null)
            //{

            //    IsVerify.EmailVerification = true;
            //    objCon.SaveChanges();
            //    ViewBag.Message = "Email Verification completed";
            //    Status = true;
            //}
            //else
            //{
            //    ViewBag.Message = "Invalid Request...Email not verify";
            //    ViewBag.Status = false;
            //}

            return View();
        }
    }
}