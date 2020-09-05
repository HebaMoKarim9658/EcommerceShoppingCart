using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularProjectAPI.Models
{
    public class Order
    {
        public Order() 
        {
            OrderDate = DateTime.Now;
        }
        [Key]
        public int OrderID { get; set; }
        public string State { get; set; }
        public DateTime OrderDate { get; set; }
        public double? TotalPrice { get; set; }
        public bool IsCanceled { get; set; } = false;

        [ForeignKey("User")]
        public string OrderOwnerID { get; set; }
        public virtual User OrderOwner { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public bool checkout { get; set; } = false;
    }
}