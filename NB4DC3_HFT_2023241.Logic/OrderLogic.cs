using NB4DC3_HFT_2023241.Models;
using NB4DC3_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NB4DC3_HFT_2023241.Logic
{
    public class OrderLogic : IOrderLogic
    {
        IRepository<Order> repo;

        public OrderLogic(IRepository<Order> repo)
        {
            this.repo = repo;
        }

        /*public void Create(Order item)
        {
            if (item.CarID > 4)
            {
                throw new ArgumentException("we don't have that many cars, duh...");
            }
            this.repo.Create(item);
        } regi*/

        //uj v
        public void Create(Order item)
        {
            if (item.CarID > 4)
            {
                throw new ArgumentException("we don't have that many cars, duh...");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Order Read(int id)
        {
            var order = this.repo.Read(id);
            if (order == null)
            {
                throw new ArgumentException("Order not exists");
            }
            return this.repo.Read(id);
        }

        public IQueryable<Order> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Order item)
        {
            this.repo.Update(item);
        }

        //non cruds

        public double AvgWaitingDays()
        {
            return this.repo.ReadAll().Average(t => t.HowManyDays);
        }

        public double TotalWaitingDays()
        {
            return this.repo.ReadAll().Sum(t => t.HowManyDays);
        }
    }
}
