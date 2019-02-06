using Glorius.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Glorius.Models.ViewModels.Shop
{
    public class CategoryVM
    {
        public ICollection<ProductVM> Products { get; set; }
        public CategoryVM()
        {
            Products = new List<ProductVM>();

        }
        public CategoryVM(CategoryDTO row)
        {
            Id = row.Id;
            Name = row.Name;
            Slug = row.Slug;
            Sorting = row.Sorting;
            SectionId = row.SectionId;
            SectionName = row.SectionName;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Sorting { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }

        public IEnumerable<SelectListItem> Sections { get; set; }

    }
}