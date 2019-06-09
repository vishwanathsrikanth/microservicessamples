using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService
{
    public static class CreateOrder
    {
        const string serviceBusConnectionString = "Endpoint=sb://srikssb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=bVQQW4ryKmFti0GTbX5YLPEbcsuGZCREOiN9ocGrzR8=";
        const string cancelOrderTopicName = "CancelOrder";
        const string chargeAmountTopicName = "ChargeAmount";
        static ITopicClient cancelOrderTopicClient;
        static ITopicClient chargeAmountTopicClient;

        static CreateOrder()
        {
            cancelOrderTopicClient = new TopicClient(serviceBusConnectionString, cancelOrderTopicName);
            chargeAmountTopicClient = new TopicClient(serviceBusConnectionString, chargeAmountTopicName);
        }

        [FunctionName("CreateOrder")]
        public static void Run([ServiceBusTrigger("CreateOrder",
            "CreateOrderSubscription",
            Connection = "ServiceBusConnection")]Message message,
            ILogger log)
        {
            var checkOutProcessModel = JsonConvert.DeserializeObject<CheckOutProcessModel>(Encoding.UTF8.GetString(message.Body));
            var order = new OrderModel()
            {
                TrackingId = checkOutProcessModel.TrackingId,
                CustomerId = checkOutProcessModel.CustomerId,
                Price = checkOutProcessModel.Price,
                Quantity = checkOutProcessModel.Quantity,
                ProductId = checkOutProcessModel.ProductId
            };
            OrdersRepository.Orders.Add(order);

            // Order service allows orders only with quantity less than 10
            if (checkOutProcessModel.Quantity < 10)
            {
                log.LogInformation($"Order Processed");
                order.OrderId = Guid.NewGuid();
                OrdersRepository.ConfirmedOrders.Add(order);
                chargeAmountTopicClient.SendAsync(new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(order))))
                    .GetAwaiter().GetResult();
            }
            else
            {
                log.LogInformation($"Order cannot be processed");

                var messageBody = new CancelOrderModel();
                messageBody.TrackingId = checkOutProcessModel.TrackingId;
                messageBody.Message = "Order cannot be process as it exceeded maximum quantity of 10";
                var cancelOrderMessage = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageBody)));
                // Send the message to the topic and assume it will be successfull - eventual consistency
                cancelOrderTopicClient.SendAsync(cancelOrderMessage).GetAwaiter().GetResult();
            }
        }
    }
}

