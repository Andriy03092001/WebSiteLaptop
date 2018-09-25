using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LaptopWebSite.ViewModels
{

    public class ProductItemViemModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public bool IsAvailable { get; set; }

    }

    public class ProductViemModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        [Required]
        public string Description { get; set; }
    }

    public class DeleteViewModel
    {
        public int Id { get; set; }
    }

    public class ProductAddViewModel
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        [Required]
        [StringLength(4000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string[] DescriptionImages { get; set; }

    }

    public class ListProductViewModel
    {
        public List<ProductItemViemModel> listProduct { get; set; }
    }

    public class ProductDeleteViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
    }

    public class ListProductDeleteViewModel
    {
        public List<ProductDeleteViewModel> listProduct { get; set; }
    }
}