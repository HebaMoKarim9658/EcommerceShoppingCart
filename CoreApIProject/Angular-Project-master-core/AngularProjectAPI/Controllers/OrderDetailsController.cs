using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularProjectAPI.Models;
using AngularProjectAPI.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IRepository<OrderDetails, int, string> OrderDetailsRepository;

        public OrderDetailsController(IRepository<OrderDetails, int, string> _OrderDetailsRepository)
        {
            OrderDetailsRepository = _OrderDetailsRepository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<OrderDetails>> GetOrders()
        {
            IEnumerable<OrderDetails> orderDetails = OrderDetailsRepository.GetAll();
            if (orderDetails.Count() > 0)
                return orderDetails.ToList();
            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult<OrderDetails> GetOrderDetails(int id)
        {
            var orderDetails = OrderDetailsRepository.GetById(id);

            if (orderDetails == null)
            {
                return NotFound();
            }

            return orderDetails;
        }

        //[HttpPut("{id}")]
        //public IActionResult PutOrderDetails(int id, OrderDetails orderDetails)
        //{
        //    if (id != orderDetails.OrderID)
        //    {
        //        return BadRequest();
        //    }
        //    if (!OrderExists(id))
        //    {
        //        return NotFound();
        //    }
        //    OrderRepository.Update(order);
        //    return NoContent();
        //}


        [HttpPost]
        public ActionResult<OrderDetails> PostOrderDetails(OrderDetails orderDeatils)
        {
            OrderDetailsRepository.Add(orderDeatils);
            return CreatedAtAction("PostOrderDetails", orderDeatils);
        }

        [HttpDelete("{id}")]
        public ActionResult<OrderDetails> DeleteOrder(int id)
        {
            var orderDetails = OrderDetailsRepository.GetById(id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            OrderDetailsRepository.Delete(orderDetails);
            return orderDetails;
        }

        private bool OrderDetailsExists(int id)
        {
            if (OrderDetailsRepository.GetById(id) == null)
                return false;
            return true;
        }
        [Route("GetProductQuantity/ProductID={productID}&OrderID={orderID}")]
        [HttpGet]
        public ActionResult<int> GetProductQuantity(int productID,int orderID)
        {
            return OrderDetailsRepository.GetProductQuantity(productID, orderID);
        }
        [Route("removeProduct/OrderID={orderID}&ProductID={productID}")]
        [HttpGet]
        public ActionResult<int> RemoveProductfromOrder(int orderID,int productID)
        {
             OrderDetailsRepository.RemoveProductfromOrder(orderID, productID);
            return Ok();
        }
    }
}