using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferDAO : ITransferDAO
    {
        private readonly string connectionString;

        public TransferDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Transfer GetTransactions(int account_from, int account_to, double amount)
        {
            Transfer transfers = new Transfer();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("insert into transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount) values (2, 2, @accountfrom, @accountto, @amount)", conn);
                    cmd.Parameters.AddWithValue("@accountfrom", account_from);
                    cmd.Parameters.AddWithValue("@accountto", account_to);
                    cmd.Parameters.AddWithValue("@amount", amount);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Transfer a = GetTransfersFromReader(reader);

                    }
                }
            }
            catch (SqlException)
            {

            }
            return transfers;
        }
        public List<Transfer> GetTransactionsByID(int id)
        {
            List<Transfer> transfers = new List<Transfer>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM transfers WHERE account_from = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Transfer a = GetTransfersFromReader(reader);
                            transfers.Add(a);
                        }

                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return transfers;
        }
        private Transfer GetTransfersFromReader(SqlDataReader reader)
        {
            Transfer t = new Transfer()
            {
                TransferId = Convert.ToInt32(reader["transfer_id"]),
                TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]),
                TransferStatusId = Convert.ToInt32(reader["transfer_status_id"]),
                AccountFrom = Convert.ToInt32(reader["account_from"]),
                AccountTo = Convert.ToInt32(reader["account_to"]),
                Amount = Convert.ToDecimal(reader["amount"]),
            };
            return t;
        }
    }
}

