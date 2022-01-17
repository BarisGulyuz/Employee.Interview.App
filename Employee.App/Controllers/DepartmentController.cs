using Employee.App.Data;
using Employee.App.Data.Entities;
using Employee.App.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee.App.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly GenericRepository<Department> departmentManager = new GenericRepository<Department>();
        private readonly GenericRepository<Employe> employeeManager = new GenericRepository<Employe>();
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var values = departmentManager.GetAll(x => x.Employees);
            return View(values);
        }

        [HttpPost]
        public ActionResult Add(Department department)
        {
            if (ModelState.IsValid)
            {
                departmentManager.Insert(department);
                TempData.Add("SuccessMessage", department.Name + " " + "Adlı Departman Başarıyla Eklendi");
                return RedirectToAction("Index");
            }
            return View(department);
        }
        [HttpGet]
        public PartialViewResult Add()
        {
            return PartialView();
        }

        public ActionResult Delete(int id)
        {
            var department = departmentManager.Get(x => x.Id == id);
            var checkDepartment = employeeManager.GetAll(x => x.DeparmentId == id);
            if (checkDepartment.Count == 0)
            {
                departmentManager.Delete(department);
                TempData.Add("SuccessMessage", department.Name + " " + "Adlı Departman Başarıyla Silindi");
                return RedirectToAction("Index");
            }
            TempData.Add("ErrorMessage", department.Name + " " + "Adlı Departman Altında Çalışan Personeller Olduğu İçin Departman Silinemiyor");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult MultipleDelete(int[] id)
        {
            foreach (var item in id)
            {
                var department = departmentManager.Get(x => x.Id == item);
                departmentManager.Delete(department);
                TempData.Add("SuccessMessage","Departmanlar Başarıyla Silindi");
            }
            return Json("1");
        }


    }
}