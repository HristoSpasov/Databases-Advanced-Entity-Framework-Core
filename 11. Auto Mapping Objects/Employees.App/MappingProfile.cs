namespace Employees.App
{
    using AutoMapper;
    using Employees.App.Models;
    using Employees.Models;

    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Employee, EmployeeDto>();
            this.CreateMap<Employee, EmployeePersonalInfoDto>();
            this.CreateMap<Employee, ManagerDto>();
            this.CreateMap<Employee, UserManagerDto>()
                .ForMember(dest => dest.ManagerLastName, opt => opt.MapFrom(src => src.Manager.LastName));
        }
    }
}