using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountDAO _accountDAO;
        private readonly IUserDAO _userDAO;
        private readonly ITransferDAO _transferDAO;

        public AccountController(IAccountDAO accountDAO, IUserDAO userDAO, ITransferDAO transferDAO)
        {
            if (_accountDAO == null)
            {
                _accountDAO = accountDAO;
            }
            if (_userDAO == null)
            {
                _userDAO = userDAO;
            }
            if (_transferDAO == null)
            {
                _transferDAO = transferDAO;
            }
        }

        [HttpGet("/users/transfers/{id}")]
        public Account GetAccount(int id)
        {
            Account accounts = _accountDAO.GetAccounts(id);
            return accounts;
        }


        [HttpGet]
        public Account GetBalance(string username)
        {
            User user = _userDAO.GetUser(username);
            Account accounts = new Account();
            accounts.Balance = _userDAO.GetBalance(user);
            return accounts;
        }

        [HttpGet("users")]
        public List<User> GetUsers()
        {
            List<User> returnUsers = new List<User>();
            returnUsers = _userDAO.GetUsers();
            return returnUsers;            
        }

        [HttpGet("users/{id}")]
        public Account GetUsersByID(int id)
        {
            Account accounts = _accountDAO.GetAccounts(id);
            return accounts;
        }

        [HttpPost("users/transfers")]
        public Transfer GetTransactions(int accountFrom, int accountTo, double amount)
        {
            Transfer transfers = _transferDAO.GetTransactions(accountFrom, accountTo, amount);
            return transfers;
        }

        [HttpGet("users/transfers/{id}")]
        public ActionResult<List<Transfer>> GetTransactionsByID(int id)
        {
           List<Transfer> transfers = _transferDAO.GetTransactionsByID(id);
           
            return transfers;
        }
    }
}