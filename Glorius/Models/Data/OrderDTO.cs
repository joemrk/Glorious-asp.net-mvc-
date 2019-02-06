using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Glorius.Models.Data
{
    [Table("tblOrder")]
    public class OrderDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Mail { get; set; }
        public string City { get; set; }
        public string Addres { get; set; }
        public string Post { get; set; }
        public string Note { get; set; }
        public bool Complete { get; set; }

        //cart
    }
}