using AutoMapper;
using Jobs.Identity.Models;

namespace Jobs.Identity.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Jobs.Identity.Pages.Register.InputModel, User>()
            .ForMember(e => e.UserName, opt => opt.MapFrom(e => e.Email));
    }
}
