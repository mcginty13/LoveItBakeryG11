using System.Data.SqlClient;

namespace BakeryClassLibrary
{
    //each OrderLine has a product id, order id and quantity
    public class OrderLine
    {

        string orderLineID;
        string orderID;
        Product product;
        public int quantity { get; set; }
        public int linePrice { get; set; }
        
        
        //these are here for easy access for the basket
        public string productName { get; }  
        public int productPrice { get; }
        
        

        public OrderLine(Product pProduct, int pQuantity)
        {
            orderLineID = SqlTools.GenerateID(this);
            product = pProduct;
            quantity = pQuantity;
            productName = product.GetName();
            productPrice = product.GetPrice();
            CalculatePrice();
        }
        public string GetProductID() { return product.GetID(); }
        public void IncrementQuantity()
        {
            quantity++;
            
        }
        public void SetOrderID(string pOrderID)
        {
            orderID = pOrderID;
        }
        public void CalculatePrice()
        {
            linePrice = product.GetPrice() * quantity;
            
        }
        public bool SendToServer()
        {


            string query = "INSERT INTO OrderItem (Id, Order_Id, Product_Id, Quantity)";
            query += " VALUES (@Id, @Order_Id, @Product_Id, @Quantity)";

            using (SqlConnection myConnection = new SqlConnection(SqlTools.GetConnectionString()))
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand(query, myConnection);
                myCommand.Parameters.AddWithValue("@Id", orderLineID);
                myCommand.Parameters.AddWithValue("@Order_Id", orderID);
                myCommand.Parameters.AddWithValue("@Product_Id", product.GetID());
                myCommand.Parameters.AddWithValue("@Quantity", quantity);
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                return true;
            }



        }
    } 
}
