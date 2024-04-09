using NB4DC3_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NB4DC3_HFT_2023241.Repository
{
    public class OrderRepository : Repository<Order>, IRepository<Order>
    {
        public OrderRepository(CarRentalDbContext ctx) : base(ctx)
        {
            
        }
        public override Order Read(int id)
        {
            return ctx.Orders.FirstOrDefault(t => t.OrderID == id);
        }
        /*public override void Update(Order item)
        {
            var old = Read(item.OrderID);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }*/
        public override void Update(Order item)
        {
            var old = Read(item.OrderID);
            if (old == null)
            {
                throw new ArgumentException("Item not exist..");
            }
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
