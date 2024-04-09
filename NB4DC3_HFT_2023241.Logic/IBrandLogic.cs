using NB4DC3_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace NB4DC3_HFT_2023241.Logic
{
    public interface IBrandLogic
    {
        void Create(Brand item);
        void Delete(int id);
        List<Brand> ItalyBrands();
        List<Brand> OneBrand(int id);
        Brand Read(int id);
        IQueryable<Brand> ReadAll();
        void Update(Brand item);
    }
}