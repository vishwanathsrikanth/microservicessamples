using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using System.Text;
using System.Linq;

namespace PaymentService
{
    public static class ChargeAmount
    {
        const string serviceBusConnectionString = "";
        const string cancelOrderTopicName = "CancelOrder";
        static ITopicClient cancelOrderTopicClient;

        static ChargeAmount()
        {
            cancelOrderTopicClient = new TopicClient(serviceBusConnectionString, cancelOrderTopicName);
        }

        [FunctionName("ChargeAmount")]
        public static void Run([ServiceBusTrigger("ChargeAmount", "chargeamountsubscription", 
            Connection = "ServiceBusConnection")]Message message, ILogger log)
        {
            var order = JsonConvert.DeserializeObject<OrderModel>(Encoding.UTF8.GetString(message.Body));
            var customerAccount = PaymentRepository.customerAccountModels.FirstOrDefault(c => c.CustomerId == order.CustomerId);
            var orderValue = order.Price * order.Quantity;
            if(orderValue <= customerAccount.MaximumOrderLimit)
            {
                log.LogInformation("Payment successful");
            }
            else
            {
                var messageBody = new CancelOrderModel();
                messageBody.TrackingId = order.TrackingId;
                messageBody.Message = "Order cannot be process as it exceeded the maximum order set for customer";
                var cancelOrderMessage = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageBody)));
                // Send the message to the topic and assume it will be successfull - eventual consistency
                cancelOrderTopicClient.SendAsync(cancelOrderMessage).GetAwaiter().GetResult();
            }
        }
    }
}
