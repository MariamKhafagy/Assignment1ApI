using System.ComponentModel.DataAnnotations;

namespace TalabatDemo.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string  Name { get; set; }
    }
}
