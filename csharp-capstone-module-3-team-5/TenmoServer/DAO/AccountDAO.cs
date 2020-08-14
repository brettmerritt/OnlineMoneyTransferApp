using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountDAO : IAccountDAO
    {

        private readonly string connectionString;
        const double startingBalance = 1000;
        private double accountBalance = 0; 

        public AccountDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Account GetAccounts(int id)
        {
            Account returnAccounts = new Account();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT account_id, user_id, balance FROM accounts WHERE user_id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Account a = GetAccountsFromReader(reader);
                            return a;
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return returnAccounts;
        }
        private Account GetAccountsFromReader(SqlDataReader reader)
        {
            Account a = new Account()
            {
                AccountId = Convert.ToInt32(reader["account_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                Balance = Convert.ToDouble(reader["balance"]),
            };

            return a;
        }

    }
}
