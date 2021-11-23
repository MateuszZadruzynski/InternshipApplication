using AutoMapper;
using DataAcess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CompanyDTO, Company>().ReverseMap(); 

            CreateMap<DiaryDTO, Diary>().ReverseMap(); 

            CreateMap<KeeperDTO, Keeper>().ReverseMap(); 

            CreateMap<StudentDTO, Student>().ReverseMap(); 

            CreateMap<Internship, InternshipDTO>().ReverseMap(); 

            CreateMap<CompanyImage, CompanyImageDTO>().ReverseMap(); 

            CreateMap<StudentAvatars, StudentAvatarsDTO>().ReverseMap();

            CreateMap<KeeperAvatars, KeeperAvatarsDTO>().ReverseMap();

            CreateMap<StudentKeeper, StudentKeeperDTO>().ReverseMap();

            CreateMap<QuestionnaireModel, QuestionnaireModelDTO>().ReverseMap();
        }
    }
}
