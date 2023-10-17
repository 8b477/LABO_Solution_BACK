
using LABO_DAL.Repositories;

using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DB_LABO;Integrated Security=True;";

using (SqlConnection connection = new())
{
    UserRepo repo = new(connection);
}