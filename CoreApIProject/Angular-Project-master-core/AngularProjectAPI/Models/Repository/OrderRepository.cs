using AngularProjectAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models.Repository
{
    public class OrderRepository : IRepository<Order, int, string>
    {
        ApplicationDbContext Context;
        public OrderRepository(ApplicationDbContext _Context)
        {
            this.Context = _Context;
        }
        public Order GetSpesificOrderID(string userid)
        {
            Order order = Context.Orders.Where(o => o.OrderOwnerID == userid && o.checkout == false).FirstOrDefault();
            if (order != null)
                return order;
            return null;
        }

        public int GetTotalQuantity(string UserID)
        {
            Order order = GetSpesificOrderID(UserID);
            if (order != null)
            {
                int sum = Convert.ToInt32(Context.OrderDetails.Where(o => o.OrderID == order.OrderID&&o.IsCanceled==false).Sum(o => o.Quantity));
                return sum;
            }
            return 0;
        }

        public void Add(Order order)
        {
            Context.Orders.Add(order);
            Context.SaveChanges();
        }

        public void Delete(Order order)
        {
            order.IsCanceled = true;
            Context.Orders.Update(order);
            Context.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            var Orders= Context.Orders.Include(o => o.OrderOwner).Include(o => o.OrderDetails).ThenInclude(o => o.Product)
                .Where(o => o.IsCanceled == false).ToList();
            foreach (var item in Orders)
            {
                var orderDetails = item.OrderDetails.Where(o => o.IsCanceled == true).ToList();
                foreach (var itemCanceled in orderDetails)
                {
                    item.OrderDetails.Remove(itemCanceled);
                }
            }
            return Orders.OrderBy(o=>o.OrderOwnerID).ThenBy(o=>o.State);
        }
        public List<Order> GetAllPending(string id)
        {
            var OrdersList = Context.Orders.Include(o => o.OrderDetails).ThenInclude(o => o.Product).Where(o => o.State == "Pending" && o.IsCanceled == false && o.checkout==true && o.OrderOwnerID==id).ToList();
            
            foreach (var item in OrdersList)
            {
                var orderDetails = item.OrderDetails.Where(o => o.IsCanceled == true).ToList();
                foreach (var itemCanceled in orderDetails)
                {
                    item.OrderDetails.Remove(itemCanceled);
                }               
            }
            return OrdersList; 
        }
        public List<Order> GetAllAccepted(string id)
        {
            var OrdersList = Context.Orders.Include(o => o.OrderDetails).ThenInclude(o => o.Product).Where(o => o.State == "accepted" && o.IsCanceled == false && o.checkout == true && o.OrderOwnerID == id).ToList();

            foreach (var item in OrdersList)
            {
                var orderDetails = item.OrderDetails.Where(o => o.IsCanceled == true).ToList();
                foreach (var itemCanceled in orderDetails)
                {
                    item.OrderDetails.Remove(itemCanceled);
                }
            }
            return OrdersList;
        }
        public List<Order> GetAllRejected(string id)
        {
            var OrdersList = Context.Orders.Include(o => o.OrderDetails).ThenInclude(o => o.Product).Where(o => o.State == "rejected" && o.IsCanceled == false && o.checkout == true && o.OrderOwnerID == id).ToList();

            foreach (var item in OrdersList)
            {
                var orderDetails = item.OrderDetails.Where(o => o.IsCanceled == true).ToList();
                foreach (var itemCanceled in orderDetails)
                {
                    item.OrderDetails.Remove(itemCanceled);
                }
            }
            return OrdersList;
        }
        public Order GetById(int id)
        {
            CalculateToTalPrice(id);
            var order = Context.Orders.Include(o => o.OrderDetails).ThenInclude(o => o.Product).Where(o => o.OrderID == id && o.checkout == false && o.IsCanceled == false).FirstOrDefault();
            if (order != null)
            {
                var orderr = order.OrderDetails.Where(o => o.IsCanceled == true).ToList();
                foreach (var item in orderr)
                {
                    order.OrderDetails.Remove(item);
                }
                return order;
            }
            return null;
        }
        public Order FindById(int id)
        {
            var order = Context.Orders.Find(id);
            return order;
        }
        public void CalculateToTalPrice(int id)
        {

            //var query = from x in Context.OrderDetails.Include(o=>o.Product)
            //            where x.OrderID == id && x.IsCanceled==false
            //            select new {OrderID=x.OrderID,ProductID=x.ProductID, OrderSubtTotalPrice = (x.Quantity) * (x.Product.Price)};

            var query = Context.OrderDetails.Include(o => o.Product).Where(o => o.OrderID == id && o.IsCanceled == false).ToList();

            foreach(var orderd in query)
            {
                orderd.SubTotal = Convert.ToDouble(orderd.Quantity * orderd.Product.Price);
                Context.OrderDetails.Update(orderd);
                
            }
            Context.SaveChanges();
            //foreach (var q in query)
            //{
            //    OrderDetails orderDetails = Context.OrderDetails.Where(o => o.ProductID == q.ProductID && o.OrderID == q.OrderID).FirstOrDefault();
            //    //orderDetails.SubTotal = Convert.ToDouble(q.OrderSubtTotalPrice);
            //    //Context.OrderDetails.Update(orderDetails);
            //}
            //Context.SaveChanges();
            Order o = Context.Orders.Find(id);
            o.TotalPrice = Context.OrderDetails.Where(o => o.IsCanceled == false && o.OrderID == id).Sum(o => o.SubTotal);
            Context.Orders.Update(o);
            Context.SaveChanges();
        }

        public Order GetByName(string OrderName)
        {
            throw new Exception("Not Implemented");
        }

        public void Update(Order order)
        {
            var Currorder = Context.Orders.Find(order.OrderID);
            Currorder.State = order.State;
            Context.Orders.Update(Currorder);
            Context.SaveChanges();
        }

        public int GetProductQuantity(int ID1, int ID2)
        {
            throw new NotImplementedException();
        }

        public int GetTotalQuantity(int UserID)
        {
            throw new NotImplementedException();
        }

        public void CheckOut(int id)
        {
            Order o=Context.Orders.Find(id);
            o.checkout = true;
            Context.Orders.Update(o);
            Context.SaveChanges();
        }

        public void RemoveProductfromOrder(int orderID, int productID)
        {
            throw new NotImplementedException();
        }

        public void Cancel(int id)
        {
            Order o = Context.Orders.Find(id);
            o.IsCanceled = true;
            Context.Orders.Update(o);
            Context.SaveChanges();
        }
    }
}
