using System.Globalization;
using Models.AccountModels;

namespace Models.OrderModels
{
    public class OrderModel
    {
        public AccountModel Account { get; set; }
        public DateTime OrderDate { get; set; }


		public List<OrderDetailsModel> OrderDetails { get; set; }


		public string GetTotalOrderPrice()
		{
			decimal price = 0;
			foreach (var orderDetail in OrderDetails)
			{
				price += orderDetail.TotalProductsPrice;
			}

			return price.ToString("0.00") + " kr";

			//return OrderDetails.Sum(x => x.TotalProductsPrice).ToString("0.00") + " kr  ";
		}
	}
}
