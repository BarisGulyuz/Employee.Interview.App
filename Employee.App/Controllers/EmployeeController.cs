using Employee.App.Data.Entities;
using Employee.App.Data.Repository;
using Employee.App.Enums;
using Employee.App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Employee.App.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        GenericRepository<Employe> employeeManager = new GenericRepository<Employe>();
        GenericRepository<EmployeeType> employeeTypeManager = new GenericRepository<EmployeeType>();
        GenericRepository<Department> departmentManager = new GenericRepository<Department>();
        GenericRepository<Photo> photoManager = new GenericRepository<Photo>();
        [HttpGet]
        public ActionResult Index()
        {
            var values = employeeManager.GetAll(x => x.Photos);
            return View(values);
        }
        [HttpGet]
        public ActionResult Add()
        {
            DropdownDatas();
            return View();
        }

        private void DropdownDatas()
        {
            List<SelectListItem> departmant = (from x in departmentManager.GetAll()
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString()
                                               }).ToList();
            ViewBag.Departmants = departmant;
            List<SelectListItem> types = (from x in employeeTypeManager.GetAll()
                                          select new SelectListItem
                                          {
                                              Text = x.Name,
                                              Value = x.Id.ToString()
                                          }).ToList();
            ViewBag.Types = types;
            List<SelectListItem> managers = (from x in employeeManager.GetAll(x => x.EmployeeTypeId == (int)(EmployeeTypeEnum.Menager))
                                             select new SelectListItem
                                             {
                                                 Text = x.Name + " " + x.Surname,
                                                 Value = x.Id.ToString()
                                             }).ToList();
            ViewBag.Managers = managers;
        }

        [HttpPost]
        public ActionResult Add(Employe employe, HttpPostedFileBase Url, string password, Photo photo)
        {
            DropdownDatas();
            if (ModelState.IsValid)
            {
                employe.Password = Crypto.Hash(password, "MD5");
                employeeManager.Insert(employe);
                if (Url != null)
                {
                    WebImage webImage = new WebImage(Url.InputStream);
                    FileInfo fileInfo = new FileInfo(Url.FileName);

                    string fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                    webImage.Resize(250, 250);
                    webImage.Save("~/Uploads/Employee/" + fileName);
                    photo.Url = "/Uploads/Employee/" + fileName;
                    photo.EmployeeId = employe.Id;
                    photoManager.Insert(photo);

                }
                TempData.Add("SuccessMessage", employe.Name + " " + employe.Surname + " " + "Adlı Personel Başarıyla Eklendi");
                return RedirectToAction("Index");
            }

            return View(employe);
        }

        [HttpGet]
        public ActionResult AddPhotos(int id)
        {
            ViewBag.Id = id;
            var employee = employeeManager.Get(x => x.Id == id);
            ViewBag.Name = employee.Name + employee.Surname;
            return View();
        }
        [HttpPost]
        public ActionResult AddPhotos(int id, IEnumerable<HttpPostedFileBase> Url)
        {
            Photo photo = new Photo();
            foreach (var url in Url)
            {
                if (Url != null)
                {
                    WebImage webImage = new WebImage(url.InputStream);
                    FileInfo fileInfo = new FileInfo(url.FileName);

                    string fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                    webImage.Resize(250, 250);
                    webImage.Save("~/Uploads/Employee/" + fileName);
                    photo.Url = "/Uploads/Employee/" + fileName;
                    photo.EmployeeId = id;
                    photoManager.Insert(photo);

                }
            }
            TempData.Add("SuccessMessage", "Personel Fotoğrafları Başarıyla Eklendi");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            DropdownDatas();
            var employee = employeeManager.Get(x => x.Id == id);
            return View(employee);
        }

        [HttpPost]
        public ActionResult Update(Employe employe, Photo photo, string password, HttpPostedFileBase Url)
        {
            var employee = employeeManager.Get(x => x.Id == employe.Id);
            DropdownDatas();
            if (ModelState.IsValid)
            {
                if (employe.Password != password)
                {
                    employe.Password = Crypto.Hash(password, "MD5");
                }

                employeeManager.Update(employee, employe);

                if (Url != null)
                {
                    WebImage webImage = new WebImage(Url.InputStream);
                    FileInfo fileInfo = new FileInfo(Url.FileName);

                    string fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                    webImage.Resize(250, 250);
                    webImage.Save("~/Uploads/Employee/" + fileName);
                    photo.Url = "/Uploads/Employee/" + fileName;
                    photo.EmployeeId = employe.Id;
                    photoManager.Insert(photo);

                }
                TempData.Add("SuccessMessage", employe.Name + " " + employe.Surname + " " + "Adlı Personel Başarıyla Güncellendi");
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var employee = employeeManager.Get(x => x.Id == id);
            var checkEmployeeManagerStatus = employeeManager.GetAll(x => x.ManagerId == id);
            if (checkEmployeeManagerStatus.Count == 0)
            {
                employeeManager.Delete(employee);
                TempData.Add("SuccessMessage", employee.Name + " " + employee.Surname + " " + "Adlı Personel Başarıyla Silindi");
                return RedirectToAction("Index");
            }
            TempData.Add("ErrorMessage", employee.Name + " " + employee.Surname + " " + "Adlı Personelin, Menajerliğini yaptığı personeller olduğu için silinemiyor");
            return RedirectToAction("Index");
        }
    }
}