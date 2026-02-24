using Microsoft.Data.SqlClient;
using System.Data;
namespace LearningDatabases.DataAccessLibrary;


public class AccountDataAccess
{
    public string ConnectionString { get; private set; }
    public AccountDataAccess(string connectionString)
    {
        ConnectionString = connectionString;
    }


    public IEnumerable<Account> GetAllAccounts()
    {
        //Instantiate a connection object which points to the correct server and database, based on the connection string
        IDbConnection connection = new SqlConnection(ConnectionString);
        //create a command object, which has a reference to the connection object and the SQL to perform
        IDbCommand command = new SqlCommand("select * from account", (SqlConnection)connection);

        //get a datareader, with acccess to the rows in the table which match the SQL query
        var datareader = command.ExecuteReader();
        //create an empty list to hold the Account objects
        List<Account> accounts = new();
        //as long as there are more tuples (based on our SQL), continue this loop
        while (datareader.Read())
        {
            //create an account object
            var account = new Account()
            {
                Id = (int)datareader["Id"],
                Name = (string)datareader["Name"],
                Balance = (decimal)datareader["Balance"]
            };
            accounts.Add(account); //add it to the list
        }
        return accounts; //return the list
    }

}
