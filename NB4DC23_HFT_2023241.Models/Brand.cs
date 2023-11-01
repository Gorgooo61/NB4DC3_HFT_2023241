using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NB4DC23_HFT_2023241.Models
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandID { get; set; }

        [StringLength(240)]
        public string BrandName { get; set; }

        [StringLength(240)]
        public string BrandCountry { get; set; }

        public virtual ICollection<Car> Cars { get; set; }

        public Brand()
        {
                
        }
        public Brand(string line)
        {
            string[] split = line.Split('#');
            BrandID = int.Parse(split[0]);
            BrandName = split[1];
            BrandCountry = split[2];
        }
    }
}
