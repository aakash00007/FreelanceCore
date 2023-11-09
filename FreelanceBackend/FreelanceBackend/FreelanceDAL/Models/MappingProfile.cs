using AutoMapper;
using FreelanceDAL.Models;
using FreelanceDAL.Models.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceDAL.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterationModel, AppUser>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<ServiceDto, Service>();
        }
    }
}
