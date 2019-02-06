using Glorius.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Glorius.Models.ViewModels.Shop
{
    public class GalleryVM
    {
        public GalleryVM()
        {

        }
        public GalleryVM(GalleryDTO row)
        {
            Id = row.Id;
            ProductId = row.ProductId;
            Img = row.Img;
            ImgType = row.ImgType;
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public byte[] Img { get; set; }
        public string ImgType { get; set; }

    }
}