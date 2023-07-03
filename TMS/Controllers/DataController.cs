using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using TMS.Models;

namespace TMS.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult Welcome()
        {
            return View();
        }
        public ActionResult WelcomeC()
        {
            return View();
        }
        public ActionResult Signup()

        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(Customer u)
        {
            if (ModelState.IsValid)
            {
                Customer CustomerEntity = new Customer();
                if (CustomerEntity.InsertCustomer(u) > 0)
                {
                    return RedirectToAction("Login");
                }
                return View(u);
            }

            return View(u);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login2 login)
        {
            if (ModelState.IsValid)
            {
                if (new Customer().isValidUser(login.Email, login.Password))
                {

                    Session["login"] = login;

                    return RedirectToAction("CustomerShow", "Data");
                }
                else
                {
                    ViewBag.InvalidCustomer = "Invalid Customer Name or Password";
                    return View(login);
                }
            }
            return View(login);
        }
        public ActionResult CustomerShow(string searchBy, string search)
        {
            Product e = new Product();
            List<Product> List = e.GetList(searchBy, search);
            return View(List);
        }
        public ActionResult CDetails(Int64 id)
        {
            Product entity = new Product();
            Product mark = entity.GetSingleProduct(id);
            return View(mark);
        }

        public ActionResult Admin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Admin(adminlg admlogin)
        {
            if (ModelState.IsValid)
            {
                if (new adminlg().ValidAdm(admlogin.Email, admlogin.Password))
                {

                    Session["adminlogin"] = admlogin;

                    return RedirectToAction("CrProduct", "Data");
                }
                else
                {
                    ViewBag.InvalidCustomer = "Invalid Admin Name or Password";
                    return View(admlogin);
                }
            }
            return View(admlogin);
        }

        public ActionResult CrProduct(string searchBy, string search)
        {
            Product e = new Product();
            List<Product> List = e.GetList(searchBy, search);
            ViewBag.Color = Color();
            return View();
        }
        [HttpPost]
        public ActionResult CrProduct(Product a)
        {

            if (ModelState.IsValid)
            {
                var imagePath = Path.Combine(Server.MapPath("~/images"), a.file.FileName);
                a.file.SaveAs(imagePath);
                a.ImageURl = a.file.FileName;
                Product m = new Product();
                ViewBag.IsPost = true;
                m.InsertProduct(a);
            }
            ViewBag.Color = Color();
            return View();
        }
        private List<SelectListItem> Color()
        {
            List<SelectListItem> ColorList = new List<SelectListItem>();
            ColorList.Add(new SelectListItem { Text = "Red", Value = "Red" });
            ColorList.Add(new SelectListItem { Text = "Blue", Value = "Blue " });
            ColorList.Add(new SelectListItem { Text = "Black", Value = "Black" });
            ColorList.Add(new SelectListItem { Text = "Yellow", Value = "Yellow" });
            ColorList.Add(new SelectListItem { Text = "Grey", Value = "Grey " });
            ColorList.Add(new SelectListItem { Text = "Mahroon", Value = "Mahroon" });


            return ColorList;
        }
        public ActionResult Edit(Int64 id)
        {
            Product e = new Product();
            Product mark = e.GetSingleProduct(id);
            ViewBag.Color = Color();
            return View(mark);
        }
        [HttpPost]
        public ActionResult Edit(Product e)
        {
            if (ModelState.IsValid)
            {
                Product entity = new Product();
                int rowCount = entity.Update(e);
                return RedirectToAction("Edit");
            }
            ViewBag.Color = Color();
            return View(e);

        }
        public ActionResult Show(string searchBy, string search)
        {
            Product e = new Product();
            List<Product> List = e.GetList(searchBy, search);
            return View(List);

        }
        public ActionResult Delete(Int64 id)
        {
            Product entity = new Product();
            int emp = entity.Delete(id);
            return RedirectToAction("Show");
        }
        public ActionResult Details(Int64 id)
        {
            Product entity = new Product();
            Product mark = entity.GetSingleProduct(id);
            return View(mark);
        }
    }
}