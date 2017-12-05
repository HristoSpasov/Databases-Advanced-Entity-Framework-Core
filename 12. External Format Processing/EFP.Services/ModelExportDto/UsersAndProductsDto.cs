namespace EFP.Services.ModelExportDto
{
    using System.Collections.Generic;

    public class UsersAndProductsDto
    {
        public int UsersCount { get; set; }

        public ICollection<UserDto> Users { get; set; }
    }
}