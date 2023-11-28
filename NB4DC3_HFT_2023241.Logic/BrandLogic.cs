using NB4DC23_HFT_2023241.Models;
using NB4DC3_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NB4DC3_HFT_2023241.Logic
{
    public class BrandLogic
    {
        IRepository<Brand> repo;

        public BrandLogic(IRepository<Brand> repo)
        {
            this.repo = repo;
        }

        public void Create(Brand item)
        {
            if(item.BrandCountry.Contains("Romania"))
            {
                throw new ArgumentException("It's not a valid country");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Brand Read(int id)
        {
            var brand = this.repo.Read(id);
            if(brand == null)
            {
                throw new ArgumentException("Brand not exists");
            }
            return this.repo.Read(id);
        }

        public IQueryable<Brand> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Brand item)
        {
            this.repo.Update(item);
        }


        //non cruds

        public List<Brand> ItalyBrands()
        {
            return this.repo.ReadAll().Where(t => t.BrandCountry=="Italy").ToList();
        }
        public List<Brand> OneBrand(int id) 
        {
            return this.repo.ReadAll().Where(t=>t.BrandID==id).ToList();
        }
    }
}
