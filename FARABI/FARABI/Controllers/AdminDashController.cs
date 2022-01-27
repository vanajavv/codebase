using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FARABI.Models;
using System.Data;
using System.Data.SqlClient;
using FARABI.DB_context;
using System.Net;
using System.Net.Mail;

namespace FARABI.Controllers
{
    public class AdminDashController : Controller
    {
        FARABIEntities objCon = new FARABIEntities();
        // GET: AdminDash
        public ActionResult AdminHome()
        {
            DataTable dt = new DataTable();
            string strConString = "Data Source=.;Initial Catalog=FARABI;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from login where Email='" + Session["Email"].ToString() + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            ViewBag.name = dt.Rows[0]["FirstName"].ToString();
            return View();
        }
        public ActionResult ManageUser()
        {
            ManageSupplier model = new ManageSupplier();
            DataTable dt = model.GetAllSupplier();
            return View(dt);

        }
        public ActionResult Edit(int EmpID)
        {
            ManageSupplier model = new ManageSupplier();
            DataTable dt = model.GetEmpByID(EmpID);
            return View("Edit", dt);
        }
        public string GenerateOTP()
        {
            string OTPLength = "4";
            string OTP = string.Empty;

            string Chars = string.Empty;
            Chars = "1,2,3,4,5,6,7,8,9,0";

            char[] seplitChar = { ',' };
            string[] arr = Chars.Split(seplitChar);
            string NewOTP = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < Convert.ToInt32(OTPLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                NewOTP += temp;
                OTP = NewOTP;
            }
            return OTP;
        }

        public ActionResult UpdateRecord(FormCollection frm, string action)
        {
            login logUSr = new login();
            if (action == "Submit")
            {
                try
                {
                    ManageSupplier model = new ManageSupplier();
                    var empstatus = frm["ddlStatus"];
                    var date="";
                    if (empstatus == "1")
                    {

                        string name = frm["txtName"];
                        //int age = Convert.ToInt32(frm["txtAge"]);
                        string Email = frm["txtEmail"];
                        Guid ActivetionCode = Guid.NewGuid();
                        date = DateTime.Now.ToShortDateString();
                        int id = Convert.ToInt32(frm["hdnID"]);
                        string otp = GenerateOTP();
                        int status = model.Manageupplier(id, empstatus, ActivetionCode, date, otp,"");
                        SendEmailToUser(Email, ActivetionCode.ToString(), otp, name,"");
                        TempData["date"] = date;
                        return RedirectToAction("ManageUser", date);
                    }
                    else if (empstatus == "2")
                    {
                       // string name = frm["txtName"];
                        //int age = Convert.ToInt32(frm["txtAge"]);
                       // string Email = frm["txtEmail"];
                        Guid ActivetionCode = Guid.NewGuid();
                        date = DateTime.Now.ToShortDateString();
                        int id = Convert.ToInt32(frm["hdnID"]);
                        //string otp = GenerateOTP();
                        int status = model.Manageupplier(id, empstatus, ActivetionCode, date, "","");
                        return View("ManageUser", date);
                    }
                    else if(empstatus=="3")
                    {
                        string name = frm["txtName"];
                        //int age = Convert.ToInt32(frm["txtAge"]);
                        string Email = frm["txtEmail"];
                        Guid ActivetionCode = Guid.NewGuid();
                        date = DateTime.Now.ToShortDateString();
                        int id = Convert.ToInt32(frm["hdnID"]);
                        string feedback = frm["txtSendBack"];
                        int status = model.Manageupplier(id, empstatus, ActivetionCode, date, "",feedback);
                        SendEmailToUser(Email, ActivetionCode.ToString(), "", name,feedback);
                        return RedirectToAction("ManageUser", date);
                    }
                    else
                    {
                        return RedirectToAction("ManageUser");
                    }

                }
                catch (Exception ex)
                {

                    return View(ex);
                }


            }
            else
            {
                return RedirectToAction("Index");
            }
           
        }
        public void SendEmailToUser(string emailId, string activationCode, string otp, string FirstName,string feedback)
        {

            Session["otp"] = otp;
            MailMessage mm = new MailMessage("vanaja992@gmail.com", emailId);

            mm.Subject = "Account Activation";
            string body = "Hello " + FirstName + ",";
            body += "<br /><br /><b>FeedBaack For Your Registration </b></br>" ;
            body += feedback; //"<br /><a href = '" + string.Format("{0}://{1}/SupplierDash/UserVerification/{2}", Request.Url.Scheme, Request.Url.Authority, activationCode) + "'>Click here to activate your account.</a>";
            body += "<br /><br />Thanks";
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("vanaja992@gmail.com", "Vanaja@1122");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);

        }
        //public ActionResult Delete(int EmpID)
        //{
        //    ManageSupplier model = new ManageSupplier();
        //    model.DeleteEmp(EmpID);
        //    return RedirectToAction("ManageUser");
        //}

        public ActionResult AddNews()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNews(News ObjNews)
        {
           //ObjNews.NTitle=
            //password convert    
            //objUsr.Password = encryptPassword.textToEncrypt(objUsr.Password);
            //objUsr.Role = "Supplier";
            //objUsr.Status = "0";
            //objUsr.date = DateTime.Now.ToShortDateString();
            objCon.News.Add(ObjNews);
            ViewBag.title = "FARABI Registration";
            int val = objCon.SaveChanges();
            return View();
        }
    }

}