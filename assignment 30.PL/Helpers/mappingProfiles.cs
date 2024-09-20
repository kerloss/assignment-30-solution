using assignment_20.DAL.Models;
using assignment_30.PL.ViewModels;
using AutoMapper;

namespace assignment_30.PL.Helpers
{
    public class mappingProfiles : Profile
    {
        public mappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap()/*.ForMember(D=>D.Name,O=>O.MapFrom(S=>S.EmpName))*/;
            CreateMap<DepartmentViewModel, Department>().ReverseMap();

        }
    }
}
