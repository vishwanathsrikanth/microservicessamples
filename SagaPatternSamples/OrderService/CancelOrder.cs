using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using System.Text;
using System.Linq;

namespace OrderService
{
    public static class CancelOrder
    {
        [FunctionName("CancelOrder")]
        public static void Run([ServiceBusTrigger("CancelOrder", "cancelordersubscription", Connection = "ServiceBusConnection")]Message message, ILogger log)
        {
            var cancelOrderModel = JsonConvert.DeserializeObject<CancelOrderModel>(Encoding.UTF8.GetString(message.Body));
            var order = (OrdersRepository.Orders.FirstOrDefault(o => o.TrackingId == cancelOrderModel.TrackingId));

            OrdersRepository.Orders.Remove(order);
            OrdersRepository.CancelledOrders.Add(new CancelOrderModel { Message = cancelOrderModel.Message,
                TrackingId = cancelOrderModel.TrackingId });
            
            // publish order successfully cancelled here. 
            log.LogInformation($"C# ServiceBus topic trigger function processed message: {JsonConvert.SerializeObject(cancelOrderModel)}");
        }
    }
}
