using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Models;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {

        const string ServiceBusConnectionString = "";
        const string TopicName = "CreateOrder";
        static ITopicClient topicClient;

        public CheckoutController()
        {
            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);
        }

        [HttpPost]
        public IActionResult CheckoutOrder(CheckOutProcessModel messageBody)
        {
            messageBody.TrackingId = Guid.NewGuid();
            var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageBody)));
            // Send the message to the topic and assume it will be successfull - eventual consistency
            topicClient.SendAsync(message).GetAwaiter().GetResult();
            return Ok($"Order successfully processed, trackingId {messageBody.TrackingId}");
        }
    }
}