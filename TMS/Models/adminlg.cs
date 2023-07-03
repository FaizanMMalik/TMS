using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class adminlg
    {
        [Display(Name = "Email Address")]
        [Required]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        int rowEffected;
        string dbConnection = @"data source=LAPTOP-6TKD6K97;initial catalog=TMS;integrated security=True";
        public Boolean ValidAdm(string Email, string Password)
        {
            Boolean isValid = false;
            try
            {
                SqlConnection con = new SqlConnection(dbConnection);
                string query = @"Select * from AdminLogin where Email=@Email and Password =@Password ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("Email", Email);
                cmd.Parameters.AddWithValue("Password", Password);
                con.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    isValid = true;
                }
            }
            catch (Exception exp)
            {

            }
            return isValid;
        }
    }

}