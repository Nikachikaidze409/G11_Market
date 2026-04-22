using System;
using System.Collections.Generic;
using System.Text;

namespace MarketDTO
{
    internal class Categories
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string[]? Picture { get; set; }
    }
}
