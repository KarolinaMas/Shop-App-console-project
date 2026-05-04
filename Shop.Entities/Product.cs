namespace Shop.Entities;

using System.Text.Json.Serialization;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CountInStock { get; set; }

    // [JsonIgnore]
    public ICollection<ProductInBasket>? ProductInBaskets { get; set; }
}
