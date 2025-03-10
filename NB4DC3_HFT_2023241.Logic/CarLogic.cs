﻿using NB4DC3_HFT_2023241.Models;
using NB4DC3_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NB4DC3_HFT_2023241.Logic
{
    public class CarLogic : ICarLogic
    {
        IRepository<Car> repo;

        public CarLogic(IRepository<Car> repo)
        {
            this.repo = repo;
        }

        public void Create(Car item)
        {
            if (item.CarID > 99)
            {
                throw new ArgumentException("too many...");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Car Read(int id)
        {
            var car = this.repo.Read(id);
            if (car == null)
            {
                throw new ArgumentException("Brand not exists");
            }
            return this.repo.Read(id);
        }

        public IQueryable<Car> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Car item)
        {
            this.repo.Update(item);
        }

        //non cruds

        public List<Car> SwiftIsTheGoat()
        {
            return this.repo.ReadAll().Where(t => t.Model == "Swift").ToList();
        }
    }
}
