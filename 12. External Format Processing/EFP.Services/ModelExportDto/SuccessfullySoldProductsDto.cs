namespace EFP.Services.ModelExportDto
{
    using System.Collections.Generic;

    public class SuccessfullySoldProductsDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<ProductExportDto> SoldProducts { get; set; }
    }
}