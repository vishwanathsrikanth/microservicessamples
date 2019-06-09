using System;

namespace Models
{
    public class OrderModel
    {
        public Guid TrackingId { get; set; }
        public Guid OrderId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    [Serializable]
    public class CheckOutProcessModel
    {
        public Guid TrackingId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    [Serializable]
    public class CancelOrderModel
    {
        public Guid TrackingId { get; set; }

        public string Message { get; set; }
    }
}
