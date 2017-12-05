namespace EFP.Services.ModelExportDto
{
    using System.Collections.Generic;

    public class SoldProductsXmlDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<ProductDto> SoldProducts { get; set; }
    }
}