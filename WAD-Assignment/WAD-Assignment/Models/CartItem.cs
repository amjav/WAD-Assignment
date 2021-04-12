using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WAD_Assignment.Models
{
    public class CartItem
    {
        public int FilmID { get; set; }

        public string FilmTitle { get; set; }

        public int OrderQuantity { get; set; }

        public decimal FilmPrice { get; set; }

        public decimal RentPrice { get; set; }

        public DateTime OrderDate { get; set; }

        public string PurchaseType { get; set; }
    }
}
