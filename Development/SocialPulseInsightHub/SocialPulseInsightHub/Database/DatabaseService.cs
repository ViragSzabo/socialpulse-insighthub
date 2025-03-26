using System.Data;
using System.Data.SqlClient;

namespace SocialPulseInsightHub.Database
{
    public class DatabaseService
    {
        private readonly string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=SocialPulseDB;Integrated Security=True;";

        public async Task<bool> RegisterUserAsync(string userName, string passwordHash, string salt)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "INSERT INTO Users (UserName, PasswordHash, Salt) VALUES (@UserName, @PasswordHash, @Salt)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 50).Value = userName;
                    cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 256).Value = passwordHash;
                    cmd.Parameters.Add("@Salt", SqlDbType.NVarChar, 50).Value = salt;

                    int rowsAffected = await cmd.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }
    }
}