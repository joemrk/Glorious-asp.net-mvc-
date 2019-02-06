using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Glorius.Models.Data
{
    public class Db : DbContext
    {
        public DbSet<CategoryDTO> Categories { get; set; }
        public DbSet<MaterialDTO> Materials { get; set; }
        public DbSet<ProductDTO> Products { get; set; }
        public DbSet<GalleryDTO> ProductGallery { get; set; }
        public DbSet<LoginDTO> Logins { get; set; }
        public DbSet<OrderDTO> Orders { get; set; }
        public DbSet<SectionDTO> Sections { get; set; }

        public System.Data.Entity.DbSet<Glorius.Models.ViewModels.Shop.ProductVM> ProductVMs { get; set; }
    }
}