using System.Text;

namespace PlayingSpectre.Models;

public class Order
{
	public int Id { get; set; }
	public DateTime CreatedDate { get; set; }
	public decimal TotalAmount { get; set; }
	public List<OrderProd> OrderProd { get; set; }


    public override string ToString()
    {
		//var sb = new StringBuilder();
		//sb.Append("\tOrder Details\n");
		//sb.Append($"{Id} - {CreatedDate.ToString("dd-MM-yyyy")}");
		//sb.Append($"CreatedDate: {CreatedDate}\n");
		//sb.Append($"TotalAmount: {TotalAmount}\n");
		//sb.Append("");

		/*sb.Append("\tProducts");
        foreach (var ord in OrderProd)
		{
            sb.Append($"Product: {ord.CoffeeId}");
            sb.Append($"Quantity: {ord.ProductQuantity}");
			sb.Append("");
        }*/
		//return sb.ToString();
		return $"{Id} - {CreatedDate:dd-MM-yyyy}";
    }

}
