using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using scheduler.core;
using scheduler.core.Dtos.Responses;
using scheduler.core.Entities;

namespace scheduler.core.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, GetUserResponseDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                    .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.FirstName))
                    .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.LastName))
                    ;
        }
    }
}
