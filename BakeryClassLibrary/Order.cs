using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace BakeryClassLibrary
{
    
    //each order is made up of several orderlines. Linked together with Ids
    public class Order  
    {
        string mOrderID;
        string mCustomerID;
        DateTime mOrderDate;
        bool mPaid;
        int mPrice;
        List<OrderLine> items;

        public Order(string pCustomerId, List<OrderLine> pItems, bool pPaid)
        {
            mOrderID = SqlTools.GenerateID(this);
            mCustomerID = pCustomerId;
            mOrderDate = DateTime.Now;
            mPaid = pPaid;
            items = pItems;
            mPrice = CalculatePrice();
            foreach (OrderLine line in items)
            {
                line.SetOrderID(mOrderID);
            }
        }

        public int CalculatePrice()
        {
            int total = 0;
            foreach (OrderLine line in items)
            {
                line.CalculatePrice();
                int price = line.linePrice; 
                total = +price;
            }
            return total;
        }
        public bool SendToServer()
        {
            string query = "INSERT INTO Orders (Id, customer_Id, OrderDate, Price, Paid)";
            query += " VALUES (@Id, @customer_Id, @OrderDate, @Price, @Paid)";
            SqlDateTime sqlDate = new SqlDateTime(mOrderDate);
            using (SqlConnection myConnection = new SqlConnection(SqlTools.GetConnectionString()))
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand(query, myConnection);
                myCommand.Parameters.AddWithValue("@Id", mOrderID);
                myCommand.Parameters.AddWithValue("@customer_Id", mCustomerID);
                myCommand.Parameters.AddWithValue("@OrderDate", sqlDate);
                myCommand.Parameters.AddWithValue("@Price", mPrice);
                myCommand.Parameters.AddWithValue("@Paid", mPaid);
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                foreach (OrderLine line in items)
                {
                    line.SendToServer();
                }
                return true;
            }
        }
    }
}
