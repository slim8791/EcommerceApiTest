using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtAuthntication.Model
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public DateTime OredrDate { get; set; }=DateTime.Now;
        //[ForeignKey("Product")]
        //public int product { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        //[ForeignKey("Customer")]
        public int customer { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
