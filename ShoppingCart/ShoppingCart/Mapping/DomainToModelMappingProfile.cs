using AutoMapper;
using habahabamall.Models;
using ShoppingApp.Entities;
using System.Linq;

namespace habahabamall.Mapping
{
    public class DomainToModelMappingProfile : Profile
    {
        public DomainToModelMappingProfile()
        {
            _ = CreateMap<UserEntity, User>();
            _ = CreateMap<AddressEntity, Address>()
                .ForMember(s => s.Name, d => d.MapFrom(e => e.User != null ? e.User.Name : string.Empty));
            _ = CreateMap<CategoryEntity, Category>();
            _ = CreateMap<SubCategoryEntity, SubCategory>();
            //main/home products mapping section. 
            _ = CreateMap<ProductEntity, Product>()
                .ForMember(s => s.Category, d => d.MapFrom(e => e.SubCategory.Name))
                .ForMember(s => s.Images, d => d.MapFrom(e => e.Images.FirstOrDefault().SeoFilename))
               .ForMember(s => s.Images, d => d.MapFrom(e => e.Images))
               .ForMember(s => s.Attributes, d => d.MapFrom(e => e.Attributes))
                .ForMember(s => s.Reviews, d => d.MapFrom(e => e.Reviews));

            //Cart
            _ = CreateMap<CartOrWishListProductEntity, CartOrWishListProduct>()
                .ForMember(s => s.Category, d => d.MapFrom(e => e.SubCategory.Name))
                .ForMember(s => s.Images, d => d.MapFrom(e => e.Images.FirstOrDefault().SeoFilename))
               .ForMember(s => s.Images, d => d.MapFrom(e => e.Images))
               .ForMember(s => s.Attributes, d => d.MapFrom(e => e.Attributes))
                .ForMember(s => s.Reviews, d => d.MapFrom(e => e.Reviews));


            _ = CreateMap<ReviewEntity, ProductReviewm>()
                .ForMember(s => s.ReviewText, d => d.MapFrom(e => e.ReviewText));
            _ = CreateMap<ReviewImageEntity, ReviewImage>()
                .ForMember(s => s.Image, d => d.MapFrom(e => e.ReviewImage));
            _ = CreateMap<PreviewImageEntity, PreviewImage>()
                .ForMember(s => s.SeoFilename, d => d.MapFrom(e => e.SeoFilename));
            _ = CreateMap<UserCardEntity, UserCard>();
            _ = CreateMap<BannerEntity, Banner>();
            _ = CreateMap<PaymentEntity, Payment>();
            _ = CreateMap<UserCardEntity, UserCard>();
            _ = CreateMap<UserCartEntity, UserCart>()
                .ForMember(s => s.Product, d => d.MapFrom(e => e.Product));
        }
    }
}