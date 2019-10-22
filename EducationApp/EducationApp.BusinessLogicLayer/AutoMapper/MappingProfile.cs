using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.DataAccessLayer.Entities;

namespace EducationApp.BusinessLogicLayer.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAuthorModel, Author>();
        }
    }
}
