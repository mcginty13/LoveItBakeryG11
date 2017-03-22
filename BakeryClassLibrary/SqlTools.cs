using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;


namespace BakeryClassLibrary
{
    public  class SqlTools
    {
        private static string connectionString = "Server=tcp:bakerymanagement11sql.database.windows.net,1433;Initial Catalog=BakeryManagement11;Persist Security Info=False;User ID=bm11sql;Password=Baking11;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static string GetConnectionString()
        {
            return connectionString;
        }

        public static int GetMaxID(string tableName) //Possibly redundant
        {

            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                SqlCommand myCommand = myConnection.CreateCommand();
                myConnection.Open();
                myCommand.CommandText = "SELECT MAX(Id) FROM " + tableName;
                int maxId = Convert.ToInt32(myCommand.ExecuteScalar());
                myConnection.Close();
                return maxId;
            }
        }

        static public string GenerateID(object obj)
        {
            string Idcode;
            string objectname = obj.GetType().Name.ToString();
            if (objectname == "OrderLine")
            {
                Idcode = "OL";
            }
            else
            {
                Idcode = objectname.Substring(0,2).ToUpper();
            }

            string id = Idcode + "-" + RandomString(5);
            return id;
        }
        


        //I got this code from stackoverflow. It guarantees a bit more randomness, Removed the capital letteres. - Dan (i assume)
        //I'll rewrite this method in code we understand. - Sam
        static string RandomString(int length, string alphabet = "abcdefghijklmnopqrstuvwxyz0123456789")
        {
            var outOfRange = byte.MaxValue + 1 - (byte.MaxValue + 1) % alphabet.Length;

            return string.Concat(
                Enumerable
                    .Repeat(0, int.MaxValue)
                    .Select(e => RandomByte())
                    .Where(randomByte => randomByte < outOfRange)
                    .Take(length)
                    .Select(randomByte => alphabet[randomByte % alphabet.Length])
            );
        }

        static byte RandomByte()
        {
            using (var randomizationProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[1];
                randomizationProvider.GetBytes(randomBytes);
                return randomBytes.Single();
            }
        }


    }
}
