using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OrderService
{
    public static class GetOrders
    {
        [FunctionName("GetOrders")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var orders = OrdersRepository.Orders;
            var confirmedOrders = OrdersRepository.ConfirmedOrders;
            var cancelledOrders = OrdersRepository.CancelledOrders;
            return new JsonResult(new
            {
                orders = orders,
                confirmedOrders = confirmedOrders, 
                cancelledOrders = cancelledOrders
            });
        }
    }
}
