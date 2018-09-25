using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LaptopWebSite.Models.Entities
{
    [Table("tlProducts")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        [StringLength(4000)]
        [DataType(DataType.Html)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int Count { get; set; }
        public bool IsAvailable { get; set; }
        public virtual ICollection<ProductDescriptionImage> ProductDescriptionImage { get; set; }

    }
}