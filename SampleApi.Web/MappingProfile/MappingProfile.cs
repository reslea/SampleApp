using AutoMapper;
using SampleAPI.Data.Entities;
using SampleApi.Web.Models;

namespace SampleApi.Web.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserReadModel>()
                .ForMember(model => model.FullName,
                    options => options.MapFrom(
                        user => $"{user.LastName} {user.FirstName}"));

            CreateMap<UserWriteModel, User>();
        }
    }
}
