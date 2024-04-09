using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace NB4DC3_HFT_2023241.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarID { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandID { get; set; }


        public int OrderID { get; set; }

        [StringLength(240)]
        public string Model { get; set; }



        public virtual Brand Brand { get; set; }



        //uj v
        [JsonIgnore]
        public virtual Order Order { get; set; }


        public Car()
        {

        }
        public Car(string line)
        {
            string[] split = line.Split('#');
            CarID = int.Parse(split[0]);
            BrandID = int.Parse(split[1]);
            OrderID = int.Parse(split[2]);
            Model = split[3];
        }


        public override bool Equals(object obj)
        {
            Car C = obj as Car;

            if (C == null)
            {
                return false;
            }
            else
            {
                return this.CarID == C.CarID;
            }
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.CarID);
        }

    }
}
