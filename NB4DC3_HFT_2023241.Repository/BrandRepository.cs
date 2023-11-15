using NB4DC23_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NB4DC3_HFT_2023241.Repository
{
    public class BrandRepository : Repository<Brand>, IRepository<Brand>
    {
        public BrandRepository(CarRentalDbContext ctx) :  base(ctx)
        {
            
        }
        public override Brand Read(int id)
        {
            return this.ctx.Brands.First(t => t.BrandID == id);
        }
        public override void Update(Brand item)
        {
            var old = Read(item.BrandID);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
        }
    }
}
