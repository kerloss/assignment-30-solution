using assignment_20.BLL.Interfacies;
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
    public class DepartmentController : Controller
    {

        // 1. inhertance => DepartmentController is a Controller
        // 2. Association => DepartmentController has a DepartmentRepository [Aggregation,Composition]
        /// <summary>
        /// Composition : Required
        /// Aggregation : optional [NULL]
        /// </summary>

        //private readonly IDepartmentRepository _IdepartmentRepository;
        private readonly IUnitOfWork _iunitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _Imapper;

        public DepartmentController(IUnitOfWork iunitOfWork , IWebHostEnvironment env, IMapper mapper)
        {
            _iunitOfWork = iunitOfWork;
            //_IdepartmentRepository = departmentRepository;
            _env = env;
            _Imapper = mapper;
        }

        //BaseUrl/Department/Index
        public IActionResult Index(string searchInput)
        {
            //Get All
            if (string.IsNullOrEmpty(searchInput))
            {
            var Departments = _iunitOfWork.IdepartmentRepository.GetAll();
            var mappedDepartment = _Imapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(Departments);
            return View(mappedDepartment);   
            }
            else
            {
                var Departments = _iunitOfWork.IdepartmentRepository.GetDepartmentByName(searchInput);
                var mappedDepartment = _Imapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(Departments);
                return View(mappedDepartment);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //dont allow any tools talking with website only website
        public IActionResult Create(DepartmentViewModel departmentViewModel)
        {
            //3. TempData => from Actoin to Action
            if (ModelState.IsValid)
            {
                var mappedDepartment = _Imapper.Map<DepartmentViewModel, Department>(departmentViewModel);
                _iunitOfWork.IdepartmentRepository.Add(mappedDepartment); //State Added
                var count = _iunitOfWork.savechange();
                if (count > 0) 
                {
                    TempData["Message"] = "Create Department Successfuly";
                    //return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "An Error Occured";
                }
                    return RedirectToAction("Index");
            }
            return View(departmentViewModel);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest(); // 400
            }
            var department = _iunitOfWork.IdepartmentRepository.GetById(id.Value);
            var mappedDepartment = _Imapper.Map<Department, DepartmentViewModel>(department);
            if (department == null)
            {
                return NotFound(); // 404
            }
            return View(viewName, mappedDepartment);
        }

        // Department/Edit/10
        // Department/Edit
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
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentViewModel)  
        // take id from Route not from Form (more secure)
        {
            if (id != departmentViewModel.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(departmentViewModel);
            }

            try
            {
                var mappedDepartment = _Imapper.Map<DepartmentViewModel, Department>(departmentViewModel);
                _iunitOfWork.IdepartmentRepository.Update(mappedDepartment); //State Modifyed
                _iunitOfWork.savechange();
                TempData["Message"] = "Edit Department Successfuly";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1.Log Exception
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error occured during update Department");
                return View(departmentViewModel);
            }
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DepartmentViewModel departmentViewModel)
        {
            try
            {
                var mappedDepartment = _Imapper.Map<DepartmentViewModel, Department>(departmentViewModel);
                _iunitOfWork.IdepartmentRepository.Delete(mappedDepartment); //State Deleted
                _iunitOfWork.savechange();
                TempData["MessageDelete"] = "Delete Department Successfuly";
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
                return View(departmentViewModel);
            }
        }
    }
}
