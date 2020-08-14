using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        List<Transfer> GetTransactionsByID(int id);
        Transfer GetTransactions(int account_from, int account_to, double amount);
    }
}
