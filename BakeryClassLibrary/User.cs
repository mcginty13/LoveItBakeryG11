using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BakeryClassLibrary
{
    public class User : Person
    {
        private Role mRole;
        private string mPassword;
        public User(string pName, Role pRole, string pPassword) : base(pName)
        {
            mRole = pRole;
            mPassword = pPassword;
        }

        public User(string pName) : base(pName)
        {

        }
        public static User setCurrentUser(string tagId)
        {
            string id = tagId;
            string connectionString = SqlTools.GetConnectionString();
            string queryString = "SELECT * FROM dbo.Users WHERE Id = @idParam";
            User currentUser = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@idParam", id.ToString());
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        currentUser = new User(reader["Name"].ToString(), new Role(reader["Role"].ToString()), reader["Password"].ToString());
                    }
                }
                finally
                {
                    reader.Close();
                    connection.Close();
                }

            }
            return currentUser;
        }
    }
}
