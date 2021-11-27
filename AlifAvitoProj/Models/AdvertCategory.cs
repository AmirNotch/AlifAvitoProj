using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Models
{
    public class AdvertCategory
    {
        public int AdvertId { get; set; }
        public Advert Advert { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
