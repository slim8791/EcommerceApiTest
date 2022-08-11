using EcommerceApiTest.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtAuthntication.Model
{
    public class Product
    {
        [Key]
        public int Ref { get; set; }
        public double Price { get; set; }
        public string ?Descreption { get; set; }
        public ICollection<Picture> Gallery  { get; set; }
        public int Quantity { get; set; }
       // [ForeignKey("SubCatigore")]
        public int subCatigorie { get; set; }
        public virtual SubCatigorie SubCatigorie { get; set; }
        //[ForeignKey("Provider")]
        public int provider { get; set; }
        public virtual Provider Provider { get; set; }
    }
}
