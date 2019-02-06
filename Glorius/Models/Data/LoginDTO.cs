using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Glorius.Models.Data
{
    [Table("tblUser")]
    public class LoginDTO
    {
        [Key]
        public int Id { get; set; }
        public string Log { get; set; }
        public string Pass { get; set; }

    }
}