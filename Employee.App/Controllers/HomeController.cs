using Employee.App.Data.Entities;
using Employee.App.Data.Repository;
using Employee.App.Enums;
using Employee.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly GenericRepository<Employe> employeeManager = new GenericRepository<Employe>();
        private readonly GenericRepository<Photo> photoManager = new GenericRepository<Photo>();
        public ActionResult Index()
        {
            var employess = employeeManager.GetAll(x => x.EmployeeTypeId == (int)EmployeeTypeEnum.Bakıcı);
            return View(employess);
        }

        public ActionResult Details(int? id)
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            var employee = employeeManager.Get(x => x.Id == id);
            var photos = photoManager.GetAll(x => x.EmployeeId == id);
            homeViewModel.Employes = employee;
            homeViewModel.Photos = photos;
            return View(homeViewModel);
        }
    }
}