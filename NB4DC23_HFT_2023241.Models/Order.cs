using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace NB4DC23_HFT_2023241.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        [ForeignKey(nameof(Car))]
        public int CarID { get; set; }

        [Range(1, 7)]
        public int HowManyDays { get; set; }


        public virtual Car Car { get; set; }

        public Order()
        {

        }
        public Order(string line)
        {
            string[] split = line.Split('#');
            OrderID = int.Parse(split[0]);
            CarID = int.Parse(split[1]);
            HowManyDays = int.Parse(split[2]);
        }
    }
}
