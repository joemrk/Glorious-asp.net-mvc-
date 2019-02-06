using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Glorius.Models.Data
{
    [Table("tblGallery")]
    public class GalleryDTO
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public byte[] Img { get; set; }
        public string ImgType { get; set; }
    }
}