namespace RetroFootballWeb.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public List<CartItem> Carts { get; set; }
        public DeliveryInfo Address { get; set; }
        public decimal Total
        {
            get
            {
                return Carts.Sum(c => c.Total) + 10;
            }
        }
        public decimal SubTotal
        {
            get
            {
                return Total + 10;
            }
        }
        public string PaymentMethod { get; set; }
        public string Note = "";
    }
}
