using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LaptopWebSite.Models.Entities
{
  
    [Table("tblUserProfiler")]
    public class UserProfiler
    {
        [Key, ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        [StringLength(maximumLength: 255), Required]
        public string Name { get; set; }

        [StringLength(maximumLength: 255), Required]
        public string Email { get; set; }


        public ApplicationUser ApplicationUser { get; set; }

    }
}