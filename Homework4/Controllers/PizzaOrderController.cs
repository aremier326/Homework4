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

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var order = await _pizzaDeliveryDbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (order == null) return RedirectToAction("Index");

            return await Task.Run(() => View("View", order));
        }

        [HttpPost]
        public async Task<ActionResult> View(Order editedOrder)
        {
            var order = await _pizzaDeliveryDbContext.Orders.FindAsync(editedOrder.Id);

            if (order == null) return RedirectToAction("Index");

            order.CustomerName = editedOrder.CustomerName;
            order.Address = editedOrder.Address;
            order.Pizza = editedOrder.Pizza;
            order.Phone = editedOrder.Phone;
            order.Email = editedOrder.Email;
            order.AdditionalOrderInfo = editedOrder.AdditionalOrderInfo;

            if (ModelState.IsValid)
            {
                await _pizzaDeliveryDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Order editedOrder)
        {
            var order = await _pizzaDeliveryDbContext.Orders.FindAsync(editedOrder.Id);

            if (order != null)
            {
                _pizzaDeliveryDbContext.Orders.Remove(order);
                await  _pizzaDeliveryDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
