using AutoMapper;
using SampleAPI.Data.Entities;
using SampleApi.Web.Models;

namespace SampleApi.Web.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(model => model.Username,
                    options => options.MapFrom(
                        user => $"{user.LastName} {user.FirstName}"));
        }
    }
}
