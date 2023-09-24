namespace PlayingSpectre.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderProd> OrderProd { get; set; }

    public override string ToString()
    {
        return $"{Id} - {CreatedDate:dd-MM-yyyy}";
    }

}
