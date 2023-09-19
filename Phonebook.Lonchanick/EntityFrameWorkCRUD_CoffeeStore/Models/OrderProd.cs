namespace PlayingSpectre.Models;

public class OrderProd
{
	public int Id { get; set; }
	public int CoffeeId { get; set; }
	public Product Coffee { get; set; }
	public int ProductQuantity { get; set; }
	public int OrderId { get; set; }
	public Order Order { get; set; }
}
