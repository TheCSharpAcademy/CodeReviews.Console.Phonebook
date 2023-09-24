using System.ComponentModel.DataAnnotations;

namespace PlayingSpectre.Models;

public class Category
{
    [Key]
    public int categoryId { get; set; }

    [Required]
    public string categoryName { get; set; }

    public List<Product> Coffees { get; set; }

    public Category(string categoryName)
    {
        this.categoryName = categoryName;
    }
    
    public override string ToString()
    {
        return this.categoryName;
    }

}
