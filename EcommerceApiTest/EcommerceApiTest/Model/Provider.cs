using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JwtAuthntication.Model
{
    public class Provider:User
    {
        //[Key]
        public string Matricule { get; set; }
        public string Company { get; set; }
        public string Service { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
