using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webapi.Data.DataModels
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public bool Discontinued { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        public virtual Category Category { get; set; }
    }

   
}
