using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryClassLibrary
{
    public class Ingredient : Item
    {
        private int quantity;
        private string units;
        
        public Ingredient(string pItemName, int pStockAlertLevel) : base(pItemName, pStockAlertLevel)
        {
        }
    }
}
