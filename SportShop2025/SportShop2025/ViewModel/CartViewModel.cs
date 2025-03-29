namespace SportShop2025.ViewModel
{
    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
        public decimal Total =>Items.Sum(item=>item.TotalPrice);
    }

    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string? Image {  get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }
}
