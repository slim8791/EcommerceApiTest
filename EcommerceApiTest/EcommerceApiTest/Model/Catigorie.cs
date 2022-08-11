using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtAuthntication.Model
{
    public class Catigorie
    {
        [Key]
        public int CatigorieId { get; set; }
        public string CatigorieName { get; set; }
        public string CatigorieDescription { get; set; }
        //[ForeignKey("Subcatigorie")]
        //public int subCatigorie { get; set; }
        public virtual ICollection<SubCatigorie> SubCatigories { get; set; }
    }
}
