using assignment_20.BLL.Interfacies;
using Microsoft.AspNetCore.Mvc;

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

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _IdepartmentRepository = departmentRepository;
        }
        //BaseUrl/Department/Index
        public IActionResult Index()
        {
            //Get All
            _IdepartmentRepository.GetAll();
            return View();
        }
    }
}
