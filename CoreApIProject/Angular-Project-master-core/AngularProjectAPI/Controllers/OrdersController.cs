using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AngularProjectAPI.Data;
using AngularProjectAPI.Models;
using AngularProjectAPI.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepository<Order, int, string> OrderRepository;
        private readonly UserManager<User> UserManagerr;
        private readonly ApplicationDbContext Context;

        public OrdersController(IRepository<Order, int, string> _OrderRepository, UserManager<User> _UserManager,ApplicationDbContext _context)
        {
            OrderRepository = _OrderRepository;
            UserManagerr = _UserManager;
            Context = _context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            IEnumerable<Order> orders = OrderRepository.GetAll();
            if (orders.Count() > 0)
                return orders.ToList();
            return NotFound();
        }
        [Route("GetDetails")]
        [HttpGet]
        public ActionResult<Order> GetDetails()
        {
            var UserClaims = HttpContext.User.Claims.ToList();
            var UserID = UserClaims[4].Value;
            var order = OrderRepository.GetSpesificOrderID(UserID);
            if (order != null)
            {
                var CurrOrder = OrderRepository.GetById(order.OrderID);
                return CurrOrder;
            }
            return null;
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = OrderRepository.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }
        
        [Route("GetCurrentOrder/{Userid}")]
        [HttpGet]
        public ActionResult<int> GetCurrentOrder(string Userid)
        {
            var order = OrderRepository.GetSpesificOrderID(Userid);
            if (order == null)
            {
                return 0;
            }
            return order.OrderID;
        }
            
        [Route("GetPendingOrders")]
        [HttpGet]
        public ActionResult<List<Order>> GetPendingOrders()
        {
            var UserClaims = HttpContext.User.Claims.ToList();
            var UserID = UserClaims[4].Value;
            var orders = OrderRepository.GetAllPending(UserID);
            if (orders == null)
                return null;                      
            return orders;
        }
        [Route("GetRejectedOrders")]
        [HttpGet]
        public ActionResult<List<Order>> GetRejectedOrders()
        {
            var UserClaims = HttpContext.User.Claims.ToList();
            var UserID = UserClaims[4].Value;
            var orders = OrderRepository.GetAllRejected(UserID);
            if (orders == null)
                return null;
            return orders;
        }
        [Route("GetAcceptedOrders")]
        [HttpGet]
        public ActionResult<List<Order>> GetAcceptedOrders()
        {
            var UserClaims = HttpContext.User.Claims.ToList();
            var UserID = UserClaims[4].Value;
            var orders = OrderRepository.GetAllAccepted(UserID);
            if (orders == null)
                return null;
            return orders;
        }
        [Route("CheckOut/{id}")]
        [HttpGet]
        public ActionResult<int> CheckOut(int id)
        {
            OrderRepository.CheckOut(id);
            return id;
        }
        [Route("CancelOrder/{id}")]
        [HttpGet]
        public ActionResult<int> CancelOrder(int id)
        {
            OrderRepository.Cancel(id);
            return id;
        }
        [Route("GetTotalQuantity/{Userid}")]
        [HttpGet]
        public async Task<ActionResult<int>> GetTotalQuantity(string Userid)
        {
            var user= await UserManagerr.GetUserAsync(HttpContext.User);
            //var userIdd = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var sum = OrderRepository.GetTotalQuantity(Userid);
            return sum;
        }

        [HttpPut("{id}")]
        public IActionResult PutOrder(int id, Order order)
        {
            if (id != order.OrderID)
            {
                return BadRequest();
            }
            var existOrder=Context.Orders.Find(id);
            if (existOrder == null)
                return NotFound();
            OrderRepository.Update(order);
            return NoContent();
        }

        
        [HttpPost]
        public ActionResult<Order> PostOrder(Order order)
        {
            OrderRepository.Add(order);
            return CreatedAtAction("GetOrder", new { id = order.OrderID }, order);
        }

        [HttpDelete("{id}")]
        public ActionResult<Order> DeleteOrder(int id)
        {
            var order = OrderRepository.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            OrderRepository.Delete(order);
            return order;
        }

        private bool OrderExists(int id)
        {
            if (OrderRepository.GetById(id) == null)
                return false;
            return true;
        }
    }
}