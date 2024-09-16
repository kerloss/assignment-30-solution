using assignment_20.BLL.Interfacies;
using assignment_20.BLL.Repositories;
using assignment_20.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace assignment_30.PL.Controllers
{
    public class EmployeeController : Controller
    {
        // 1. inhertance => DepartmentController is a Controller
        // 2. Association => DepartmentController has a DepartmentRepository [Aggregation,Composition]
        /// <summary>
        /// Composition : Required
        /// Aggregation : optional [NULL]
        /// </summary>
        
        private readonly IEmployeeRepository _IemployeeRepository;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository employeeRepository,IWebHostEnvironment env)
        {
            _IemployeeRepository = employeeRepository;
            _env = env;
        }

        //BaseUrl/Employee/Index
        public IActionResult Index()
        {
            TempData.Keep();    //if u want to keep tempData in next Action to use it
            //Get All
            var Employees = _IemployeeRepository.GetAll();
            //Extra Info
            //binding through View's Dictionary : transfer Data from Action to View
            //one way data Binding
            //1. ViewDate => key, Value
            ViewData["Message"] = "Hello ViewData";
            //2. ViewBag
            ViewBag.Message = "Hello ViewBag";

            return View(Employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            //3. TempData => from Action to Action

            if (ModelState.IsValid)
            {
                var count = _IemployeeRepository.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "Employee created Succesfully";
                }
                else
                {
                    TempData["Message"] = "An Error Occured";
                }
                    return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest(); // 400
            }
            var employee = _IemployeeRepository.GetById(id.Value);
            if (employee == null)
            {
                return NotFound(); // 404
            }
            return View(viewName, employee);
        }

        public IActionResult Edit(int? id)
        {
            //if (!id.HasValue)
            //{
            //    return BadRequest(); // 400
            //}
            //var employee = _IemployeeRepository.GetById(id.Value);
            //if (employee == null)
            //{
            //    NotFound(); // 404
            //}
            //return View(employee);
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //dont allow any tools talking with website only website
        public IActionResult Edit([FromRoute] int id, Employee employee)
        // take id from Route not from Form (more secure)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            try
            {
                _IemployeeRepository.Update(employee);
                TempData["Message"] = "Edit Employee Successufuly";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1.Log Exception
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error occured during update Department");
                return View(employee);
            }
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            try
            {
                _IemployeeRepository.Delete(employee);
                TempData["MessageDelete"] = "Delete Employee Successfuly";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error occured during deleting department");
                }
                return View(employee);
            }
        }
    }
}
