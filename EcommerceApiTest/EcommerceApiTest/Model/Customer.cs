using EcommerceApiTest.Model;
using System.ComponentModel.DataAnnotations;

namespace JwtAuthntication.Model
{
    public class Customer:User
    {
        //[Key]
        //public int CustomerId { get; set; }
        public Picture? Picture { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Cin { get; set; }
    }
}
