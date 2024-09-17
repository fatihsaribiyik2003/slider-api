using AutoMapper;
using MongoDB.Bson;
using SubuProtokol.Core.Extensions;
using SubuProtokol.Entities.EntityFramework.Database1;
using SubuProtokol.Entities.Mongo;
using SubuProtokol.Models;

namespace SubuProtokol.Services
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
           
            ProtokolMappings();
            UserMappings();
            UnitMappings();
        }

        private void ProtokolMappings()
        {
            CreateMap<ProtokolQuery, ProtokolModel>().ReverseMap();
            CreateMap<Protokol, ProtokolModel>().ReverseMap();
            CreateMap<Protokol, ProtokolQuery>().ReverseMap();
           
        }
        private void UserMappings()
        {
            CreateMap<User, UserLoginModel>().ReverseMap();
            CreateMap<User, UserProtokolModel>().ReverseMap();
        }
        private void UnitMappings()
        {
            CreateMap<Unit, UnitModel>().ReverseMap();
        }
      
    }
}
