using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glorius.Models.Data
{
    [Table("tblProducts")]
    public class ProductDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string MaterialName { get; set; }
        public int MaterialId { get; set; }
        public byte[] Img { get; set; }
        public string ProductCode { get; set; }
        public string ImgType { get; set; }
        public int Discount { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }

        [ForeignKey("CategoryId")]
        public virtual CategoryDTO Category { get; set; }
        [ForeignKey("MaterialId")]
        public virtual MaterialDTO Material { get; set; }
        [ForeignKey("SectionId")]
        public virtual SectionDTO Section { get; set; }
    }
}