using AutoMapper;
using StudentsAPI.Dtos;
using StudentsAPI.Model;
using StudentsAPI.ViewModels;

namespace StudentsAPI.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper() 
        {
            CreateMap<Students, PostStudentViewModel>();
            CreateMap<PostStudentViewModel, Students>();
            CreateMap<Students, StudentDTO>();
            CreateMap< StudentDTO,Students>();

            CreateMap<Class, PostClassViewModel>();
            CreateMap<PostClassViewModel, Class>();
            CreateMap<ClassDTO, Class>();
            CreateMap<Class, ClassDTO>();

        }
    }
}
