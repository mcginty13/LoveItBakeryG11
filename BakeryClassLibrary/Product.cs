using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryClassLibrary
{
    public class Product : Item
    {
        private int mPrice;
        private string mDescription;
        private List<Ingredient> ingredients;

        public Product(string pItemName, string pDesc, int pStockAlertLevel, int pPrice) : base(pItemName, pStockAlertLevel)
        {
            mDescription = pDesc;
            mPrice = pPrice;
        }

        public Product(string pID) :base(pID)
        {
            string connectionString = SqlTools.GetConnectionString();
            string queryString = "SELECT * FROM dbo.Product WHERE Id = @IdParam";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@idParam", pID.ToString());
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        string tId = reader["Id"].ToString();
                        string tName = reader["Name"].ToString();
                        int tPrice = int.Parse(reader["Price"].ToString());
                        string tDesc = reader["Description"].ToString();
                        int tQtyInStock = int.Parse(reader["QtyInStock"].ToString());
                        int tStockAlertLevel = int.Parse(reader["StockAlertLevel"].ToString());
                        mDescription = tDesc;
                        mPrice = tPrice;
                        this.SetParentVars(tName,tStockAlertLevel,tQtyInStock); //please someone think of a better way of doing this
                    }
                    
                }
                finally
                {
                    reader.Close();
                    connection.Close();
                }

            }
        }

        
        public int GetPrice() { return mPrice; }
        public string GetDesc() { return mDescription; }

        public bool SendToServer()
        {
           

                string query = "INSERT INTO Product (Id, Name, Description, QtyInStock, StockAlertLevel, Price)";
                query += " VALUES (@Id, @Name, @Description, @QtyInStock, @StockAlertLevel, @Price)";

                using (SqlConnection myConnection = new SqlConnection(SqlTools.GetConnectionString()))
                {
                    myConnection.Open();
                    SqlCommand myCommand = new SqlCommand(query, myConnection);
                    myCommand.Parameters.AddWithValue("@Id", GetID());
                    myCommand.Parameters.AddWithValue("@Name", GetName());
                    myCommand.Parameters.AddWithValue("@Description", mDescription);
                    myCommand.Parameters.AddWithValue("@QtyInStock", GetQtyInStock());
                    myCommand.Parameters.AddWithValue("@StockAlertLevel", GetAlertLevel());
                    myCommand.Parameters.AddWithValue("@Price", mPrice);
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                    return true;
                }

            

        }
    }
}
