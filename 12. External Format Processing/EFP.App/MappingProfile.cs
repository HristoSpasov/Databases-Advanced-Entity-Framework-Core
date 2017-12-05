namespace EFP.App
{
    using AutoMapper;
    using EFP.Models;
    using EFP.Services.ModelImportDto;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<User, UserImportDto>();
            this.CreateMap<UserImportDto, User>();

            this.CreateMap<Product, ProductImportDto>();
            this.CreateMap<ProductImportDto, Product>();

            this.CreateMap<Category, CategoryImportDto>();
            this.CreateMap<CategoryImportDto, Category>();
        }
    }
}