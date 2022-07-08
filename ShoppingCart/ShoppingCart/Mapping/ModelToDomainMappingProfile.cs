using AutoMapper;
using habahabamall.Models;
using ShoppingApp.Entities;

namespace habahabamall.Mapping
{
    public class ModelToDomainMappingProfile : Profile
    {
        public ModelToDomainMappingProfile()
        {
            _ = CreateMap<User, UserEntity>();
            _ = CreateMap<Address, AddressEntity>();
            _ = CreateMap<Category, CategoryEntity>();
            _ = CreateMap<SubCategory, SubCategoryEntity>();
            _ = CreateMap<Product, ProductEntity>();
            _ = CreateMap<CartOrWishListProduct, CartOrWishListProductEntity>();
            _ = CreateMap<ProductReviewm, ReviewEntity>()
                .ForMember(s => s.ReviewImages, d => d.MapFrom(e => e.ReviewImages));
            _ = CreateMap<ReviewImage, ReviewImageEntity>()
                .ForMember(s => s.ReviewImage, d => d.MapFrom(e => e.Image));
            _ = CreateMap<PreviewImage, PreviewImageEntity>()
                .ForMember(s => s.SeoFilename, d => d.MapFrom(e => e.SeoFilename));
            _ = CreateMap<UserCard, UserCardEntity>();
            _ = CreateMap<Banner, BannerEntity>();
            _ = CreateMap<Payment, PaymentEntity>();
            _ = CreateMap<UserCard, UserCardEntity>();
            _ = CreateMap<UserCart, UserCartEntity>();
        }
    }
}