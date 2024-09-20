using assignment_20.BLL.Interfacies;
using assignment_20.BLL.Repositories;
using assignment_20.DAL.Models;
using assignment_30.PL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;

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
        private readonly IMapper _Imapper;
        private readonly IDepartmentRepository _IdepartmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository,IWebHostEnvironment env /*,IDepartmentRepository departmentRepository*/, IMapper mapper)
        {
            _IemployeeRepository = employeeRepository;
            _env = env;
            _Imapper = mapper;
            //_IdepartmentRepository = departmentRepository;
        }

        //BaseUrl/Employee/Index
        public IActionResult Index(string searchInput)
        {
            //TempData.Keep();    //if u want to keep tempData in next Action to use it
            //Get All
            if (string.IsNullOrEmpty(searchInput))
            {
                var Employees = _IemployeeRepository.GetAll();
                var mappedEmployee = _Imapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
                return View(mappedEmployee);
            }
            else
            {
                var Employees = _IemployeeRepository.GetEmployeeByName(searchInput);
                var mappedEmployee = _Imapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
                return View(mappedEmployee);
            }
            //Extra Info
            //binding through View's Dictionary : transfer Data from Action to View
            //one way data Binding
            //1. ViewDate => key, Value
            //ViewData["Message"] = "Hello ViewData";
            //2. ViewBag
            //ViewBag.Message = "Hello ViewBag";
        }

        public IActionResult Create()
        {
            //ViewData["DepartmentsViewData"] = _IdepartmentRepository.GetAll();
            //ViewBag.DepartmentsViewBag = _IdepartmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            //3. TempData => from Action to Action

            if (ModelState.IsValid)
            {
                ////Manual Mapping
                //var mappedEmployee = new Employee()
                //{
                //    //if we have something in model not used in ViewModel u can put Default Value in Model
                //    Name = employeeViewModel.Name,
                //    Address = employeeViewModel.Address,
                //    Age = employeeViewModel.Age,
                //    Email = employeeViewModel.Email,
                //    Salary = employeeViewModel.Salary,
                //    IsActive = employeeViewModel.IsActive,
                //    IsDeleted = employeeViewModel.IsDeleted,
                //    HireDate = employeeViewModel.HireDate,
                //    PhoneNumber = employeeViewModel.PhoneNumber,
                //};

                var mappedEmployee = _Imapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
                var count = _IemployeeRepository.Add(mappedEmployee);
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
            return View(employeeViewModel);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest(); // 400
            }
            var employee = _IemployeeRepository.GetById(id.Value);
            //ViewData["DepartmentsViewData"] = _IdepartmentRepository.GetAll();
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
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeViewModel)
        // take id from Route not from Form (more secure)
        {
            if (id != employeeViewModel.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(employeeViewModel);
            }

            try
            {
                var mappedEmployee = _Imapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
                _IemployeeRepository.Update(mappedEmployee);
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
                return View(employeeViewModel);
            }
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int id, EmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(employeeViewModel);
            }

            try
            {
                var mappedEmployee = _Imapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
                _IemployeeRepository.Delete(mappedEmployee);
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
                return View(employeeViewModel);
            }
        }
    }
}
