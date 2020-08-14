using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using TenmoClient.Data;


namespace TenmoClient
{
    public class APIService
    {
        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private readonly static string Account_URL = API_BASE_URL + "account";        
        private readonly IRestClient client;
        private static API_User user = new API_User();
        private static Transfer transfer = new Transfer();

        public bool LoggedIn { get { return !string.IsNullOrWhiteSpace(user.Token); } }

        public string UNAUTHORIZED_MSG { get { return "Authorization is required for this endpoint. Please log in."; } }
        public string FORBIDDEN_MSG { get { return "You do not have permission to perform the requested action"; } }
        public string OTHER_4XX_MSG { get { return "Error occurred - received non-success response: "; } }

        public APIService()
        {
            client = new RestClient();
        }
        public APIService(IRestClient restClient)
        {
            client = restClient;
        }
        public Account GetBalance()
        {
            Account accounts = new Account();
            RestRequest request = new RestRequest(Account_URL);
            request.AddParameter("username", UserService.GetUser().Username);
            IRestResponse<Account> response = client.Get<Account>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            } else
            {
                return response.Data;
            }
            return null;
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            RestRequest request = new RestRequest(Account_URL + "/users");            
            IRestResponse<List<User>> response = client.Get<List<User>>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }

        public Account GetAccounts(int id)
        {
            Account accounts = new Account();
            RestRequest request = new RestRequest(Account_URL + "/users/" + id);
            IRestResponse<Account> response = client.Get<Account>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }

        public Transfer GetTransactions(int accountFrom, int accountTo, double amount)
        {
            User user = new User();
            RestRequest request = new RestRequest(Account_URL + "/users/transfers");
            IRestResponse<Transfer> response = client.Get<Transfer>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }

        public List<Transfer> GetTransactionsByID(int id) //two separate methods here and in the controller 
        {
            List<Transfer> transfers = new List<Transfer>();
            RestRequest request = new RestRequest(Account_URL + "/users/transfers/" + id);
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }


        public string ProcessErrorResponse(IRestResponse response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                return "Error occurred - unable to reach server.";
            }
            else if (!response.IsSuccessful)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return UNAUTHORIZED_MSG;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    return FORBIDDEN_MSG;
                }
                else
                {
                    return OTHER_4XX_MSG + (int)response.StatusCode;
                }
            }
            return "";
        }

    }
}
