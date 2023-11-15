using NB4DC23_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NB4DC3_HFT_2023241.Repository
{
    public class CarRepository : Repository<Car>, IRepository<Car>
    {
        public CarRepository(CarRentalDbContext ctx) : base(ctx)
        {
            
        }
        public override Car Read(int id)
        {
            return this.ctx.Cars.First(t => t.CarID == id);
        }
        public override void Update(Car item)
        {
            var old = Read(item.CarID);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
        }
    }
}
