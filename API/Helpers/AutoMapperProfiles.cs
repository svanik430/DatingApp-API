using API.DTOs;
using API.Enitites;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(d => d.Age, O => O.MapFrom(s => s.DateOfBirth.CaluculateAge()))
                .ForMember(d=>d.PhotoUrl,O=>O.MapFrom(s=>s.photos.FirstOrDefault(x=>x.IsMain)!.Url));
            CreateMap<Photo, PhotoDto>();
        }
    }
}
