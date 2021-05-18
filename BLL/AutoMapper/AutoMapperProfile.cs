using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ArticleDTO, Article>().ReverseMap();
            CreateMap<CommentDTO, Comment>().ReverseMap();
            CreateMap<TagDTO, Tag>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
        }
        public static MapperConfiguration InitializeAutoMapper()
        {
            var mapperConfiguration = new MapperConfiguration(conf => conf.AddProfile(new AutoMapperProfile()));

            return mapperConfiguration;
        }
    }
}
