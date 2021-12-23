using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

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