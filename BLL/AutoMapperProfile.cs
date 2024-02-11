using DAL.Models;
using AutoMapper;
using BLL.DTO;

namespace BLL
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FeatureRequest, Feature>().ReverseMap();
            CreateMap<CharacteristicRequest, Characteristic>().ReverseMap();
            CreateMap<ItemRequest, Item>().ReverseMap();

        }
    }
}