using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DapperBasicCrud.Models
{
    public class Friend
    {
        public int Id { get; set; }

        public string Name { get; set; }
        [Required]

        public string City { get; set; }
        [Required]

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

    }
}