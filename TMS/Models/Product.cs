using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TMS.Models
{
    public class Product
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public String Type { get; set; }
        [Required]
        public String Color { get; set; }
        [Required(ErrorMessage = "Please enter Manafacturing date")]
        public double Price { get; set; }
        [Required]
        public String ManufacturedBy { get; set; }
        [Required(ErrorMessage = "Please enter Manafacturing date")]
        [CustomHireDate(ErrorMessage = "Manafacturing  Date must be less than or equal to Today's Date")]
        [DataType(DataType.Date)]
        public DateTime? ManufacturedDate { get; set; }
        public HttpPostedFileBase file { get; set; }
        public string ImageURl { get; set; }


        int rowEffected;
        string dbConnection = @"data source=LAPTOP-6TKD6K97;initial catalog=TMS;integrated security=True";

        public int InsertProduct(Product a)
        {

            SqlConnection con = new SqlConnection(dbConnection);
            con.Open();
            string query = "INSERT INTO dbo.Product (ID,Type,Color,Price,ManufacturedBy,ManufacturedDate,ImgURL) VALUES('" + a.ID + "','" + a.Type + "','" + a.Color + "','" + a.Price + "','" + a.ManufacturedBy + "','" + a.ManufacturedDate + "','"+a.ImageURl+"')";
            SqlCommand cmd = new SqlCommand(query, con);
            rowEffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowEffected;
        }

        public List<Product> GetList(String searchBy, String search)
        {
            List<Product> ProductList = new List<Product>();
            SqlConnection con = new SqlConnection(dbConnection);
            string query = string.Empty;
            if (!String.IsNullOrEmpty(searchBy))
            {
                query = @"select * from Product where " + searchBy + " like '%" + search + "%'";
            }
            else
            {
                query = @"select * from Product";
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                ProductList.Add(new Product
                {
                    ID = Convert.ToInt32(dataReader["ID"].ToString()),
                    Type = dataReader["Type"].ToString(),
                    Color = dataReader["Color"].ToString(),
                    Price = Convert.ToInt32(dataReader["Price"].ToString()),
                    ManufacturedBy = dataReader["ManufacturedBy"].ToString(),
                    ManufacturedDate = DateTime.Parse(dataReader["ManufacturedDate"].ToString()),
                    
                });

            }
            con.Close();
            return ProductList;
        }
        public int Delete(Int64 id)
        {
            int effectedRows = 0;

            try
            {
                SqlConnection con = new SqlConnection(dbConnection);
                string query = @"delete from Product where ID='" + id + "'";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                effectedRows = cmd.ExecuteNonQuery();
                con.Close();
                return effectedRows;
            }
            catch (Exception exp)
            {
                return effectedRows;
            }

        }
        public int Update(Product e)
        {
            int RowCount = 0;
            try
            {
                SqlConnection con = new SqlConnection(dbConnection);
                string query = @"update Product set Type='" + e.Type + "',Color='" + e.Color + "',Price = '" + e.Price + "',ManufacturedBy= '" + e.ManufacturedBy + "',ManufacturedDate = '" + e.ManufacturedDate + "',ImgURL = '" + e.ImageURl + "' where ID = '" + e.ID + "'";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                RowCount = cmd.ExecuteNonQuery();
                con.Close();
                return RowCount;
            }
            catch (Exception exp)
            {
                return RowCount;
            }
        }
        public Product GetSingleProduct(Int64 id)

        {
            Product e = null;
            SqlConnection con = new SqlConnection(dbConnection);
            string query = @"select * from Product where ID='" + id + "' ";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.Read())
            {
                e = new Product
                {
                    ID = Convert.ToInt32(dataReader["ID"].ToString()),
                    Type = dataReader["Type"].ToString(),
                    Color = dataReader["Color"].ToString(),
                    Price = Convert.ToInt32(dataReader["Price"].ToString()),
                    ManufacturedBy = dataReader["ManufacturedBy"].ToString(),
                    ManufacturedDate = DateTime.Parse(dataReader["ManufacturedDate"].ToString()),
                    ImageURl = dataReader["imgURL"].ToString(),
                };
            }
            con.Close();
            return e;
        }
    }
}
    
