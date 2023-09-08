namespace RetroFootballWeb.Models
{
    public class CartItem
    {
        public string productID { get; set; }
        public string productName { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public decimal Total { get; set; }
        public CartItem() { }
        public CartItem(string ID,string productName, decimal price, string size, int quantity, string image, decimal total)
        {
            productID = ID;
            this.productName = productName;
            Price = price;
            Size = size;
            Quantity = quantity;
            Image = image;
            Total = total;
        }
    }
}
