using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NB4DC3_HFT_2023241.Repository
{
    public interface IRepository<T> where T: class 
    {
        void Create(T item);
        IQueryable<T> ReadAll();
        T Read(int id);
        void Update(T item);
        void Delete(int id);
    }
}
