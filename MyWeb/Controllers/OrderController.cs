using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace MyWeb.Controllers
{
    public class OrderController : Controller
    {
        public IList<Food> Foods = new List<Food>();
        private static int Number { get; set; }
        public OrderController()
        {
            Foods.Add(new Food { Id = 1, Name = "Hamburgur", Price = 15 });
            Foods.Add(new Food { Id = 2, Name = "Pizza", Price = 25 });
            Foods.Add(new Food { Id = 3, Name = "Onion Ring", Price = 5 });
            Foods.Add(new Food { Id = 4, Name = "French Fries", Price = 10 });
            Foods.Add(new Food { Id = 5, Name = "Chicken Fingers", Price = 15 });
            Foods.Add(new Food { Id = 6, Name = "Chicken Nugets", Price = 12 });
            Foods.Add(new Food { Id = 7, Name = "Waffles", Price = 10 });
            Foods.Add(new Food { Id = 8, Name = "Pancakes", Price = 10 });




        }

        [Route("api/order")]
        [HttpPost]
        public IActionResult Create(Order order)
        {
            Number += 1;

            var food = Foods.First(f => f.Id == order.FoodId);

            order.Id = Number;
            order.Amount = food.Price * order.Count;
            order.TimeCreated = DateTime.Now;

            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //channel.ExchangeDeclare("afra_direct_exchange", "direct");
                channel.QueueDeclare(queue: "orders",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonConvert.SerializeObject(order);

                var body = Encoding.UTF8.GetBytes(message);
                channel.ExchangeDeclare("Test", "direct");

                channel.BasicPublish(exchange: "",
                     routingKey: "orders",
                     basicProperties: null,
                     body: body);

                Console.WriteLine(" [x] Sent {0}", message);
            }

            return Ok(order);
        }
    }
}