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
                //  .ForMember(dest => dest.FeatureName, opt => opt.MapFrom(src => src.Name))
                    //.ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.FeatureItem.Where(x=> x.Id == x.ItemId).Select(x=>x.Item.Name)))
                  //  .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Items.Where(x => src.Id == 1).Select(x => x.Name)))
                  .IgnoreAllPropertiesWithAnInaccessibleSetter()
             .ReverseMap();
            CreateMap<Feature, FeatureModel>()
                .ForMember(dest => dest.FeatureName, opt => opt.MapFrom(src => src.Name));

            CreateMap<Item, FeatureModel>();
            //    .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Features.Select(f => f.Name))); //eto

            CreateMap<ItemRespond, Item>()
                    .ForMember(dest => dest.FeatureItem, opt => opt.MapFrom(src => src.FeatureItem))
                    .ReverseMap();

            



            //CreateMap<FeatureItem, FeatureModel>()
            //     //  .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Items.Select(x => x.Name)))
            //     .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src..Name))
            //     .ForMember(dest => dest.FeatureName, opt => opt.MapFrom(src => src.Feature.Name))
            // .ForMember(dest => dest.CharasticName, opt => opt.MapFrom(src => src.Characteristic))
            CreateMap<CharacteristicRequest, Characteristic>().ReverseMap();
            CreateMap<CharacteristicRespond, Characteristic>().ReverseMap();
            CreateMap<Feature, FeatureRespond>()
                .ForMember(dest => dest.featureName, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
            CreateMap<ItemRequest, Item>().ReverseMap();
            CreateMap<ItemRespond, Item>().ReverseMap();
            CreateMap<ItemFeatureRequest, FeatureItem>().ReverseMap();
                //.ForMember(dest => dest.FeaturesName, opt => opt.MapFrom(src => src.Features.Select(x => x.Name))).ReverseMap();
              //  .ForMember(dest => dest.FeatureName, opt => opt.MapFrom(src => src.Name));


        }
    }
}