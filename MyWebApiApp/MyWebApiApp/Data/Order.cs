namespace MyWebApiApp.Data
{
    public enum OrderStatus
    {
        New = 0, Payment = 1, Complete = 2, Cancelled = -1
    }
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public OrderStatus OrderStatus {  get; set; }
        public string Receiver { get; set; }
        public string ShipPlace { get; set; }
        public string TelNumer { get; set;}
        public ICollection<OrderDetail> OrderDetails { get; set; }  

        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }


    }
}
