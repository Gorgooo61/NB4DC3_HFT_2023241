using Castle.Core.Resource;
using Moq;
using NB4DC23_HFT_2023241.Models;
using NB4DC3_HFT_2023241.Logic;
using NB4DC3_HFT_2023241.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NB4DC3_HFT_2023241.Test
{
    [TestFixture]
    public class Test
    {
        OrderLogic logicO;
        Mock<IRepository<Order>> mockOrderRepo;

        CarLogic logicC;
        Mock<IRepository<Car>> mockCarRepo;

        BrandLogic logicB;
        Mock<IRepository<Brand>> mockBrandRepo;

        [SetUp]
        public void InitO()
        {
            mockOrderRepo = new Mock<IRepository<Order>>();
            mockOrderRepo.Setup(x => x.ReadAll()).Returns(new List<Order>()
            {
            new Order("1#1#4"),
            new Order("2#2#3"),
            new Order("3#3#2"),

        }.AsQueryable());
            logicO = new OrderLogic(mockOrderRepo.Object);
        }

        [SetUp]
        public void InitC()
        {
            mockCarRepo = new Mock<IRepository<Car>>();
            mockCarRepo.Setup(x => x.ReadAll()).Returns(new List<Car>()
            {
            new Car("1#1#159"),
            new Car("2#2#911"),
            new Car("3#3#Swift"),

        }.AsQueryable());
            logicC = new CarLogic(mockCarRepo.Object);
        }

        [SetUp]
        public void InitB()
        {
            mockBrandRepo = new Mock<IRepository<Brand>>();
            mockBrandRepo.Setup(x => x.ReadAll()).Returns(new List<Brand>()
            {
            new Brand("1#Alfa_Romeo#Italy"),
            new Brand("2#Porsche#Germany"),
            new Brand("3#Suzuki#Japan"),

        }.AsQueryable());
            logicB = new BrandLogic(mockBrandRepo.Object);
        }

        [Test]
        public void BrandCreateTest()
        {
            var brand= new Brand("1#Alfa_Romeo#Italy");
            logicB.Create(brand);

            mockBrandRepo.Verify(x=> x.Create(brand), Times.Once());
        }
        [Test]
        public void InvalidBrandCreateTest()
        {
            var brand = new Brand("1#Dacia#Romania");
            try
            {
                logicB.Create(brand);
            }
            catch (Exception ex) { }
            mockBrandRepo.Verify(x=>x.Create(brand), Times.Never());
        }



        [Test]
        public void CarCreateTest()
        {
            var car = new Car("1#1#159");
            logicC.Create(car);

            mockCarRepo.Verify(x => x.Create(car), Times.Once());
        }
        [Test]
        public void InvalidCarCreateTest()
        {
            var car = new Car("5#1#159");
            try
            {
                logicC.Create(car);
            }
            catch (Exception ex) { }
            mockCarRepo.Verify(x => x.Create(car), Times.Never());
        }



        [Test]
        public void OrderCreateTest()
        {
            var order = new Order("1#1#4");
            logicO.Create(order);

            mockOrderRepo.Verify(x => x.Create(order), Times.Once());
        }
        [Test]
        public void InvalidOrderCreateTest()
        {
            var order = new Order("1#5#4");
            try
            {
                logicO.Create(order);
            }
            catch (Exception ex) { }
            mockOrderRepo.Verify(x => x.Create(order), Times.Never());
        }


        [Test]
        public void AvgWaitingDaysTest()
        {
            double? avg= logicO.AvgWaitingDays();
            Assert.That(avg, Is.EqualTo(3));
        }

        [Test]
        public void TotalWaitingDaysTest()
        {
            double? sum = logicO.TotalWaitingDays();
            Assert.That(sum, Is.EqualTo(9));
        }

        [Test]
        public void SwiftIsTheGoatTest()
        {
            var cars=logicC.SwiftIsTheGoat().ToList<Car>();
            List<Car> result = new List<Car>()
            {
                new Car("3#3#Swift")
            };
            Assert.AreEqual(result, cars);
        }

        [Test]
        public void ItalyBrandsTest()
        {
            var brands= logicB.ItalyBrands().ToList<Brand>();
            List<Brand> result = new List<Brand>()
            { 
                new Brand("1#Alfa_Romeo#Italy")
            };
            Assert.AreEqual(result,brands);
        }

        [Test]
        public void OneBrandTest()
        {
            var brands= logicB.OneBrand(1).ToList<Brand>();
            List<Brand> res = new List<Brand>()
            {
                new Brand("1#Alfa_Romeo#Italy")
            };
            Assert.AreEqual(res,brands);
        }

    }
}
