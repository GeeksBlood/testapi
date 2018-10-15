using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aguila.Models
{
    public partial class RegisterAppUserDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string EmailID { get; set; }
    }

    public partial class SuccessDto
    {
        public bool Success { get; set; }
    }
}
