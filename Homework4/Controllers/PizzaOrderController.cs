using Homework4.Data;
using Homework4.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homework4.Controllers
{
    public class PizzaOrderController : Controller
    {
        private readonly PizzaDeliveryDbContext _pizzaDeliveryDbContext;

        public PizzaOrderController(PizzaDeliveryDbContext pizzaDeliveryDbContext)
        {
            _pizzaDeliveryDbContext = pizzaDeliveryDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders = await _pizzaDeliveryDbContext.Orders.ToListAsync();
            return View(orders);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Order placeOrderRequest)
        {
            
            var order = new Order()
            {
                CustomerName = placeOrderRequest.CustomerName,
                Address = placeOrderRequest.Address,
                Pizza = placeOrderRequest.Pizza,
                Phone = placeOrderRequest.Phone,
                Email = placeOrderRequest.Email,
                AdditionalOrderInfo = placeOrderRequest.AdditionalOrderInfo
            };

            if (ModelState.IsValid)
            {
                await _pizzaDeliveryDbContext.Orders.AddAsync(order);
                await _pizzaDeliveryDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
