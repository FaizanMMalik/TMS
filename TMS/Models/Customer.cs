using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class Customer
    {
        [Required]
        public int ID { get; set; }
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
        [Display(Name = "Email Address")]
        [Required]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]

        int rowEffected;
        string dbConnection = @"data source=LAPTOP-6TKD6K97;initial catalog=TMS;integrated security=True";
        public int InsertCustomer(Customer a)
        {

            SqlConnection con = new SqlConnection(dbConnection);
            con.Open();
            string query = "INSERT INTO dbo.Customer (ID,FirstName,LastName,Email,Password) VALUES('" + a.ID + "','" + a.FirstName + "','" + a.LastName + "','" + a.Email + "','" + a.Password + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            rowEffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowEffected;
        }
        public Boolean isValidUser(string Email, string Password)
        {
            Boolean isValid = false;
            try
            {
                SqlConnection con = new SqlConnection(dbConnection);
                string query = @"Select * from Customer where Email=@Email and Password =@Password ";
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