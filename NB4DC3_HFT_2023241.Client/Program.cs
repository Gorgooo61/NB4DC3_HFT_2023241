using ConsoleTools;
using NB4DC3_HFT_2023241.Client;
using NB4DC3_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace MovieDbApp.Client
{
    internal class Program
    {
        static RestService rest;
        static void Create(string entity)
        {
            if (entity == "Car")
            {
                Console.Write("Enter Actor Name: ");
                string name = Console.ReadLine();
                rest.Post(new Car() { Model = name }, "car");
            }
        }
        static void List(string entity)
        {
            if (entity == "Car")
            {
                List<Car> actors = rest.Get<Car>("car");
                foreach (var item in actors)
                {
                    Console.WriteLine(item.CarID + ": " + item.Model);
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "Car")
            {
                Console.Write("Enter Car's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Car one = rest.Get<Car>(id, "car");
                Console.Write($"New name [old: {one.Model}]: ");
                string name = Console.ReadLine();
                one.Model = name;
                rest.Put(one, "car");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Car")
            {
                Console.Write("Enter Car's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "car");
            }
        }

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:61242/", "car");

            var carSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Car"))
                .Add("Create", () => Create("Car"))
                .Add("Delete", () => Delete("Car"))
                .Add("Update", () => Update("Car"))
                .Add("Exit", ConsoleMenu.Close);

            var brandSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Brand"))
                .Add("Create", () => Create("Brand"))
                .Add("Delete", () => Delete("Brand"))
                .Add("Update", () => Update("Brand"))
                .Add("Exit", ConsoleMenu.Close);

            var orderSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Order"))
                .Add("Create", () => Create("Order"))
                .Add("Delete", () => Delete("Order"))
                .Add("Update", () => Update("Order"))
                .Add("Exit", ConsoleMenu.Close);



            var menu = new ConsoleMenu(args, level: 0)
                .Add("Cars", () => carSubMenu.Show())
                .Add("Brands", () => brandSubMenu.Show())
                .Add("Orders", () => orderSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();

        }
    }
}

