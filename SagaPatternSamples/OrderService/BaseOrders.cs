using Microsoft.Azure.WebJobs.Host.Bindings;
using Models;
using System.Collections;
using System.Collections.Generic;

namespace OrderService
{
    public static class OrdersRepository
    {
        public static readonly IList<OrderModel> Orders;
        public static readonly IList<OrderModel> ConfirmedOrders;
        public static readonly IList<CancelOrderModel> CancelledOrders;

        static OrdersRepository()
        {
            Orders = new List<OrderModel>();
            CancelledOrders = new List<CancelOrderModel>();
            ConfirmedOrders = new List<OrderModel>();
        }
    }
}