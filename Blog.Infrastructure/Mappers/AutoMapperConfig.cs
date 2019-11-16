using AutoMapper;
using Blog.Core.Domain;
using Blog.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Infrastructure.Mappers
{
    public class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<Category, CategoryDto>();
                cfg.CreateMap<Post, PostDto>();
            })
            .CreateMapper();
    }
}
