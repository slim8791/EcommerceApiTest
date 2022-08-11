using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtAuthntication.Model
{
    public class SubCatigorie
    {
        [Key]
        public int SubCatigorieId { get; set; }
        public string SubCatigorieName { get; set; }
        public string SubCatigorieDescription { get; set; }
        //[ForeignKey("Catigorie")]
        public int catigorie { get; set; }
        public Catigorie Catigorie { get; set; }
        public virtual ICollection<Product> products { get; set; }
    }
}
