using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;


namespace BakeryClassLibrary
{
    public abstract class Item
    {
        private string mItemID;
        private string mItemName;
        private int qtyInStock;
        private int stockAlertLevel;
        

        public Item(string pItemName, int pStockAlertLevel)
        {
            mItemID = SqlTools.GenerateID(this);
            mItemName = pItemName;
            stockAlertLevel = pStockAlertLevel;
        }
       
        public Item(string pId)
        {
            mItemID = pId;
        }

        public string GetID() { return mItemID; }
        public string GetName() { return mItemName; }
        public void SetName(string pName)
        {
        }
        public void SetParentVars(string pName, int alert, int instock) //please someone think of a better way of doing this
        {
            mItemName = pName;
            stockAlertLevel = alert;
            qtyInStock = instock;

        }
        public int GetQtyInStock() { return qtyInStock; }
        public int GetAlertLevel() { return stockAlertLevel; }
        public bool UpdateStock(int qty)        //can be used to add or remove items 
        {
            if (qtyInStock + qty < 0)
            {
                return false;
            }
            else
            {
                qtyInStock = qtyInStock + qty;
                return true;
            }
        }
    }
}