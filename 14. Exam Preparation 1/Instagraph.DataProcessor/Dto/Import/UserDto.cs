using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Instagraph.DataProcessor.Dto.Import
{
    public class UserDto
    {
        [MaxLength(30)]
        public string Username { get; set; }

        [MaxLength(20)]
        public string Password { get; set; }

        public string  ProfilePicture { get; set; }
    }
}
