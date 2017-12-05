namespace EFP.Services.ModelExportDto
{
    using System.Collections.Generic;

    public class SoldProductsDto
    {
        public int Count { get; set; }

        public ICollection<ProductDto> Products { get; set; }
    }
}