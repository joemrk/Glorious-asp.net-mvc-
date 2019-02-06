using Glorius.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Glorius.Models.ViewModels.Shop
{
    public class MaterialVM
    {
        public ICollection<ProductVM> Products { get; set; }
        public MaterialVM()
        {
            Products = new List<ProductVM>();
        }

        public MaterialVM(MaterialDTO row)
        {
            Id = row.Id;
            Name = row.Name;
            Slug = row.Slug;
            Sorting = row.Sorting;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Sorting { get; set; }


        
    }
}