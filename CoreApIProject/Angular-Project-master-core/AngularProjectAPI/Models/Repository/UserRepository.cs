using AngularProjectAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models.Repository
{
    public class UserRepository:IRepository<User,string,string>
    {
        ApplicationDbContext Context;
        public UserRepository(ApplicationDbContext _Context)
        {
            this.Context = _Context;
        }

        public void Add(User user)
        {
            Context.Users.Add(user);
            Context.SaveChanges();
        }

        public void Cancel(string id)
        {
            throw new NotImplementedException();
        }

        public void CheckOut(string id)
        {
            throw new NotImplementedException();
        }

        public void Delete(User user)
        {
            Context.Users.Remove(user);
            Context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return Context.Users.ToList();
        }

        public List<User> GetAllAccepted(string id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllPending(string id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllRejected(string id)
        {
            throw new NotImplementedException();
        }

        public User GetById(string id)
        {
            return Context.Users.Find(id);
        }

        public User GetByName(string UserName)
        {
            return Context.Users.Where(o => o.UserName == UserName).FirstOrDefault();
        }

        public string GetProductQuantity(string ID1, string ID2)
        {
            throw new NotImplementedException();
        }

        public User GetSpesificOrderID(string userid)
        {
            throw new NotImplementedException();
        }

        public string GetTotalQuantity(string UserID)
        {
            throw new NotImplementedException();
        }

        public void RemoveProductfromOrder(string orderID, string productID)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            var user1 = Context.Users.Find(user.Id);
            user1.UserName = user.UserName;
            user1.Email = user.Email;
            Context.Users.Update(user1);

            //Context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
