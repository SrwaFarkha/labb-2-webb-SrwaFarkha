using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Webapi.Data.DataModels
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailsId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
