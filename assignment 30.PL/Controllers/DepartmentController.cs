using assignment_20.BLL.Interfacies;
using assignment_20.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace assignment_30.PL.Controllers
{
    public class DepartmentController : Controller
    {
        // 1. inhertance => DepartmentController is a Controller
        // 2. Association => DepartmentController has a DepartmentRepository [Aggregation,Composition]
        /// <summary>
        /// Composition : Required
        /// Aggregation : optional [NULL]
        /// </summary>

        private readonly IDepartmentRepository _IdepartmentRepository;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentRepository departmentRepository , IWebHostEnvironment env)
        {
            _IdepartmentRepository = departmentRepository;
            _env = env;
        }

        //BaseUrl/Department/Index
        public IActionResult Index()
        {
            //Get All
            var Departments = _IdepartmentRepository.GetAll();
            return View(Departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //dont allow any tools talking with website only website
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                var count = _IdepartmentRepository.Add(department);
                if (count > 0) 
                {
                    return RedirectToAction("Index");
                    //return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest(); // 400
            }
            var department = _IdepartmentRepository.GetById(id.Value);
            if (department == null)
            {
                return NotFound(); // 404
            }
            return View(viewName, department);
        }

        // Department/Edit/10
        // Department/Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (!id.HasValue)
            //{
            //    return BadRequest(); // 400
            //}
            //var department = _IdepartmentRepository.GetById(id.Value);
            //if (department == null)
            //{
            //    NotFound(); // 404
            //}
            //return View(department);
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //dont allow any tools talking with website only website
        public IActionResult Edit([FromRoute] int id, Department department)  
        // take id from Route not from Form (more secure)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(department);
            }

            try
            {
                _IdepartmentRepository.Update(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1.Log Exception
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error occured during update Department");
                return View(department);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department department)
        {
            try
            {
                _IdepartmentRepository.Delete(department);
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
                return View(department);
            }
        }
    }
}
