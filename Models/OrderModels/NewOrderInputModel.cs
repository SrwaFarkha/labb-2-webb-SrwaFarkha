
using Models.AccountModels;

namespace Models.OrderModels
{
    public class NewOrderInputModel
    {
	    public int AccountId { get; set; }
		public DateTime OrderDate { get; set; }
        
        public IEnumerable<OrderDetailsInputModel> OrderDetails { get; set; }

    }
}
