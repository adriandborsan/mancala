using MySql.Data.MySqlClient;
namespace dasp
{
    public class DaspDatabase
    {
        private readonly MySqlConnection _connection;
        public DaspDatabase(string server, string database, string userId, string password)
        {
            string connectionString = $"Server={server};Database={database};Uid={userId};Pwd={password};";
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
        }
        public void CloseConnection()
        {
            _connection.Close();
        }
        public bool UserExists(string username)
        {
            const string query = "SELECT COUNT(*) FROM account WHERE username = @username;";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@username", username);
            object result = command.ExecuteScalar();
            return result != null && Convert.ToInt32(result) > 0;
        }
        public string Register(string username, string password)
        {
            if (UserExists(username))
            {
                return "Username already exists.";
            }

            const string query = "INSERT INTO account (username, password) VALUES (@username, @password);";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            int affectedRows = command.ExecuteNonQuery();
            return affectedRows > 0 ? DaspConstants.SUCCESS : "no row affected";
        }
        public string Login(string username, string password)
        {
            if (!UserExists(username))
            {
                return "Username not found";
            }
            const string query = "SELECT password FROM account WHERE username = @username;";
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@username", username);
            object result = command.ExecuteScalar();

            if (result == null)
            {
                return "Error: Null result when fetching password.";
            }
            string storedPassword = result.ToString();
            return password == storedPassword ? DaspConstants.SUCCESS : "wrong password";
        }
    }
}
