using Employee.App.Data.Entities;
using Employee.App.Data.Repository;
using Employee.App.Enums;
using Employee.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Employee.App.Controllers
{
    public class AuthController : Controller
    {
        private readonly GenericRepository<Employe> employeeManager = new GenericRepository<Employe>();

        //[HttpPost]
        //public ActionResult Login(UserLoginViewModel userLogin)
        //{
        //    var MD5Pass = Crypto.Hash(userLogin.Password, "MD5");
        //    var login = employeeManager.Get(x => x.Mail == userLogin.Mail);
        //    try
        //    {
        //        if (login.Mail == userLogin.Mail && login.Password == MD5Pass && login.EmployeeTypeId == (int)EmployeeTypeEnum.Admin)
        //        {
        //            Session["AdminId"] = login.Id;
        //            Session["Mail"] = login.Mail;
        //            TempData.Add("SuccessMessage", "Giriş İşlemi Başarılı");
        //            return RedirectToAction("Index", "Department");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //    TempData.Add("LoginError", "Mailiniz veya Şifreniz hatalı");
        //    return View(userLogin);
        //}

        [HttpPost]


        public ActionResult Login(UserLoginViewModel userLogin)
        {
            var MD5Pass = Crypto.Hash(userLogin.Password, "MD5");
            var admininfo = employeeManager.Get(x => x.Mail == userLogin.Mail & x.Password == MD5Pass & x.EmployeeTypeId == (int)EmployeeTypeEnum.Admin);
            try
            {
                if (admininfo != null)
                {
                    FormsAuthentication.SetAuthCookie(admininfo.Mail, false);
                    Session["Mail"] = admininfo.Mail;
                    TempData.Add("SuccessMessage", "Giriş İşlemi Başarılı");
                    return RedirectToAction("Index", "Department");
                }
            }
            catch (Exception)
            {
                throw;
            }
            TempData.Add("AuthInfo", "Mailiniz veya Şifreniz hatalı");
            return View(userLogin);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            TempData.Add("AuthInfo", "Başarıyla Çıkış Yapıldı");
            return RedirectToAction("Login");
        }
    }
}