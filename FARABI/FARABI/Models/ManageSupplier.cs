using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace FARABI.Models
{
    public class ManageSupplier
    {
        public string role { get; set; }
        public int id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        public DataTable GetAllSupplier()
        {
            DataTable dt = new DataTable();
            string strConString = "Data Source=.;Initial Catalog=FARABI;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from login where status=0", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }


        public DataTable GetEmpByID(int intEmpID)
        {
            DataTable dt = new DataTable();

            string strConString = "Data Source=.;Initial Catalog=FARABI;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from login where Log_Id=" + intEmpID, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }


        public int Manageupplier(int intEmpID, string status,System.Guid activationcode,string date,string otp,string feeback)
        {
            string strConString = "Data Source=.;Initial Catalog=FARABI;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Update login SET ActivetionCode=@ActivetionCode,Status=@status,date=@date,OTP=@OTP,feedback=@feedback where Log_Id=@Log_Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@ActivetionCode", activationcode);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@OTP", otp);
                cmd.Parameters.AddWithValue("@feedback", feeback);
                cmd.Parameters.AddWithValue("@Log_Id", intEmpID);
                return cmd.ExecuteNonQuery();
            }
        }
        //public int DeleteEmp(int intEmpID)
        //{
        //    string strConString = "Data Source=.;Initial Catalog=FARABI;Integrated Security=True";

        //    using (SqlConnection con = new SqlConnection(strConString))
        //    {
        //        con.Open();
        //        string query = "Delete from login where Log_Id=@Log_Id";
        //        SqlCommand cmd = new SqlCommand(query, con);
        //        cmd.Parameters.AddWithValue("@Log_Id", intEmpID);
        //        return cmd.ExecuteNonQuery();
        //    }
        //}

        
    }
}