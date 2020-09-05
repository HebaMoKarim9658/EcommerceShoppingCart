using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProjectAPI.Models.Repository
{
    public interface IRepository<T, Tkey,TkeySec>
    {
        IEnumerable <T> GetAll();
        T GetById(Tkey id);
        T GetByName(TkeySec name);
        void Add(T Object);
        void Update(T Object);
        void Delete(T id);
        T GetSpesificOrderID(string userid);
        Tkey GetProductQuantity(Tkey ID1, Tkey ID2);
        Tkey GetTotalQuantity(TkeySec UserID);
        void CheckOut(Tkey id);
        void Cancel(Tkey id);
        void RemoveProductfromOrder(Tkey orderID, Tkey productID);
        List<T> GetAllPending(string id);
        List<T> GetAllAccepted(string id);
        List<T> GetAllRejected(string id);
    }
}
