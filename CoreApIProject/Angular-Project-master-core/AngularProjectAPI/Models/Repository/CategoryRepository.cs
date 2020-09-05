using AngularProjectAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models.Repository
{
    public class CategoryRepository:IRepository<Category,int,string>
    {
        ApplicationDbContext Context;
        public CategoryRepository(ApplicationDbContext _Context)
        {
            this.Context = _Context;
        }

        public void Add(Category category)
        {
            Context.Categories.Add(category);
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

        public void Delete(Category category)
        {
            category.IsDeleted = true;
            Context.Categories.Update(category);
            Context.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            return Context.Categories.ToList();
        }

        public List<Category> GetAllAccepted(string id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAllPending(string id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAllRejected(string id)
        {
            throw new NotImplementedException();
        }

        public Category GetById(int id)
        {
            return Context.Categories.Find(id);
        }

        public Category GetByName(string CategoryName)
        {
            return Context.Categories.Where(o => o.Name == CategoryName).FirstOrDefault();
        }

        public int GetProductQuantity(int ID1, int ID2)
        {
            throw new NotImplementedException();
        }

        public Category GetSpesificOrderID(string userid)
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

        public void Update(Category category)
        {
            Context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
