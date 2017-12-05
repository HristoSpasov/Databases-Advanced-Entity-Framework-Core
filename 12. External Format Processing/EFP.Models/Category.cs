namespace EFP.Models
{
    using System.Collections.Generic;
    using EFP.Models.Validators;

    public class Category
    {
        public int Id { get; set; }

        [CategoryName(3, 15, ErrorMessage = "Category name length must be between 3 & 15 symbols.")]
        public string Name { get; set; }

        public ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}