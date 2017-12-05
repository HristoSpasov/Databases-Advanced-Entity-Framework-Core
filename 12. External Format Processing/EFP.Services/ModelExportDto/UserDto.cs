namespace EFP.Services.ModelExportDto
{
    public class UserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }

        public SoldProductsDto SoldProducts { get; set; }
    }
}