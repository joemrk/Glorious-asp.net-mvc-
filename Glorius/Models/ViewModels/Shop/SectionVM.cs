using Glorius.Models.Data;
using System.Collections.Generic;

namespace Glorius.Models.ViewModels.Shop
{
    public class SectionVM
    {
        public ICollection<ProductVM> Products { get; set; }
        public ICollection<CategoryVM> Categories { get; set; }

        public SectionVM()
        {
            Products = new List<ProductVM>();
            Categories = new List<CategoryVM>();

        }
        public SectionVM(SectionDTO row)
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