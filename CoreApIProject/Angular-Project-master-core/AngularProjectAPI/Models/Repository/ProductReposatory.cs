using AngularProjectAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models.Repository
{
    public class ProductReposatory : IRepository<Product, int,string>
    {
        ApplicationDbContext Context;
        public ProductReposatory(ApplicationDbContext _Context)
        {
            this.Context = _Context;
        }

        public void Add(Product product)
        {
            Context.Products.Add(product);
            Context.SaveChanges();
        }

        public void Cancel(int id)
        {
            throw new NotImplementedException();
        }

        public void CheckOut(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product product)
        {
            product.IsDeleted = true;
            Context.Products.Update(product);
            Context.SaveChanges();            
        }

        public IEnumerable<Product> GetAll()
        {
            return Context.Products.Where(p=>p.IsDeleted == false).ToList();
        }

        public List<Product> GetAllAccepted(string id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllPending(string id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllRejected(string id)
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            return Context.Products.Find(id);
        }

        public Product GetByName(string title)
        {
            return Context.Products.Where(o => o.Title == title).FirstOrDefault();
        }

        public int GetProductQuantity(int ID1, int ID2)
        {
            throw new NotImplementedException();
        }

        public Product GetSpesificOrderID(string userid)
        {
            throw new NotImplementedException();
        }

        public int GetTotalQuantity(string UserID)
        {
            throw new NotImplementedException();
        }

        public void RemoveProductfromOrder(int orderID, int productID)
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            var product1 = Context.Products.Find(product.ProductID);
            product1.Title = product.Title;
            product1.Details = product.Details;
            product1.CategoryID = product.CategoryID;
            product1.Price = product.Price;
            product1.Image = product.Image;

            Context.Products.Update(product1);

            Context.SaveChanges();
        }
    }
}
