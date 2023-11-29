using NB4DC23_HFT_2023241.Models;
using System.Linq;

namespace NB4DC3_HFT_2023241.Logic
{
    public interface IOrderLogic
    {
        double AvgWaitingDays();
        void Create(Order item);
        void Delete(int id);
        Order Read(int id);
        IQueryable<Order> ReadAll();
        double TotalWaitingDays();
        void Update(Order item);
    }
}