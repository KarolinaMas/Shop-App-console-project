namespace Shop.Entities
{
    public class ProductInBasket
    {
        public int ProductId { get; set; }
        public int BasketId { get; set; }
        public int Count { get; set; }
    }
}
