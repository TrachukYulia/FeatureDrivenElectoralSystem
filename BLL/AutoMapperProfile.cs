using DAL.Models;
using AutoMapper;
using BLL.DTO;
using DAL.Data.Configuration;

namespace BLL
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Feature, FeatureRequest>()
                .ForMember(dest => dest.FeatureName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CharacteristicId, opt => opt.MapFrom(src => src.CharacteristicsId))
                .ReverseMap();
            CreateMap<Feature, FeatureModel>()
                  .IgnoreAllPropertiesWithAnInaccessibleSetter()
             .ReverseMap();
            CreateMap<Feature, FeatureModel>()
                .ForMember(dest => dest.FeatureName, opt => opt.MapFrom(src => src.Name));
            CreateMap<FeatureNameRespond, Feature>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ReverseMap();
            CreateMap<Feature, FeatureRespond>()
    .ForMember(dest => dest.featureName, opt => opt.MapFrom(src => src.Name))
    .ReverseMap();


            // CreateMap<Item, FeatureModel>();
            //item

            CreateMap<ItemRespond, Item>()
                    .ReverseMap();
            CreateMap<ItemRequest, Item>()
                .ForMember(dest => dest.FeatureItem, opt => opt.MapFrom(src => src.FeatureItem))
                .ReverseMap();
            CreateMap<ItemRespond, Item>()
                 .ForMember(dest => dest.FeatureItem, opt => opt.MapFrom(src => src.FeatureItem))
                .ReverseMap();
            CreateMap<ItemFeatureRequest, FeatureItem>()
                .ReverseMap();

            //chatacteristics
            CreateMap<CharacteristicNameRespond, Characteristic>()
                .ReverseMap();
            CreateMap<CharacteristicRequest, Characteristic>().ReverseMap();
            CreateMap<CharacteristicRespond, Characteristic>().ReverseMap();

        }
    }
}