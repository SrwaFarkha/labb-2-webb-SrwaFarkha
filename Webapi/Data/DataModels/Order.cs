using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Webapi.Data.DataModels
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
