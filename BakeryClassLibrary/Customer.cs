using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryClassLibrary
{
    public class Customer: Person
    {
        private string mAddressL1;
        private string mAddressL2;
        private string mAddressL3;
        private string mAddressCity;
        private string mAddressCounty;
        private string mAddressPostCode;
        private string mEmail;
        private List<Order> mPrevOrders;
        private string mContactNo;

        public Customer(string pName,string pEmail,string pContactNo, string[] pAddress) :base(pName)
        {
            mAddressL1 = pAddress[0];
            mAddressL2 = pAddress[1];
            mAddressL3 = pAddress[2];
            mAddressCity = pAddress[3];
            mAddressCounty = pAddress[4];
            mAddressPostCode = pAddress[5];
            mEmail = pEmail;
            mContactNo = pContactNo;
            mPrevOrders = new List<Order>();
        }

        public bool SendToServer()
        {
            try
            {

                string query = "INSERT INTO Customers (Id, Name, Address1, Address2, Address3, City, County, PostCode, ContactNo, Email)";
                query += " VALUES (@Id, @Name, @Address1, @Address2, @Address3, @City, @County, @PostCode, @ContactNo, @Email)";

                using (SqlConnection myConnection = new SqlConnection(SqlTools.GetConnectionString()))
                {
                    myConnection.Open();
                    SqlCommand myCommand = new SqlCommand(query, myConnection);
                    myCommand.Parameters.AddWithValue("@Id", GetID());
                    myCommand.Parameters.AddWithValue("@Name", GetName());
                    myCommand.Parameters.AddWithValue("@Address1", mAddressL1);
                    myCommand.Parameters.AddWithValue("@Address2", mAddressL2);
                    myCommand.Parameters.AddWithValue("@Address3", mAddressL3);
                    myCommand.Parameters.AddWithValue("@City", mAddressCity);
                    myCommand.Parameters.AddWithValue("@County", mAddressCounty);
                    myCommand.Parameters.AddWithValue("@PostCode", mAddressPostCode);
                    myCommand.Parameters.AddWithValue("@ContactNo", mContactNo);
                    myCommand.Parameters.AddWithValue("@Email", mEmail);
                    myCommand.ExecuteNonQuery();
                    myConnection.Close();
                    return true;
                }
                
            }
            catch
            {
                return false;
            }
           
        }
    }
}
