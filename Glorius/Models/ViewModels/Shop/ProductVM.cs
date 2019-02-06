using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Glorius.Models.Data;

namespace Glorius.Models.ViewModels.Shop
{
    public class ProductVM
    {
        public ProductVM()
        {

        }
        public ProductVM(ProductDTO row)
        {
            Id = row.Id;
            Name = row.Name;
            Slug = row.Slug;
            Description = row.Description;
            Price = row.Price;
            CategoryName = row.CategoryName;
            CategoryId = row.CategoryId;
            MaterialName = row.MaterialName;
            MaterialId = row.MaterialId;
            Img = row.Img;
            ProductCode = row.ProductCode;
            ImgType = row.ImgType;
            Discount = row.Discount;
            SectionId = row.SectionId;
            SectionName = row.SectionName;
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string MaterialName { get; set; }
        [Required]
        public int MaterialId { get; set; }
        public byte[] Img { get; set; }
        public string ProductCode { get; set; }
        public string ImgType { get; set; }
        public int Discount { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Materials { get; set; }
        public IEnumerable<SelectListItem> Sections { get; set; }

    }
}