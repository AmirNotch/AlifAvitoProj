using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Dto
{
    public class AdvertDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Cost { get; set; }
        public string ReviewText { get; set; }
        public DateTime? DatePublished { get; set; }
    }
}
