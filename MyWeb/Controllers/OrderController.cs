using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Models;

namespace MyWeb.Controllers
{
    public class OrderController : Controller
    {
        public IList<Food> Foods=new List<Food>();

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
            var food = Foods.First(f => f.Id == order.FoodId);
            order.Amount = food.Price * order.Count;
            order.TimeCreated = DateTime.Now;

            return Ok(order);
        }
    }
}