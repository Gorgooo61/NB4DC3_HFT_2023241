using NB4DC3_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace NB4DC3_HFT_2023241.Logic
{
    public interface ICarLogic
    {
        void Create(Car item);
        void Delete(int id);
        Car Read(int id);
        IQueryable<Car> ReadAll();
        List<Car> SwiftIsTheGoat();
        void Update(Car item);
    }
}