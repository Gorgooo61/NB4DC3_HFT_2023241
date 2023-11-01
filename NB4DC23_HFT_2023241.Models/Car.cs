﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NB4DC23_HFT_2023241.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarID { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandID { get; set; }

        [StringLength(240)]
        public string Model { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public Car()
        {

        }
        public Car(string line)
        {
            string[] split = line.Split('#');
            CarID = int.Parse(split[0]);
            BrandID = int.Parse(split[1]);
            Model = split[2];
        }
    }
}
