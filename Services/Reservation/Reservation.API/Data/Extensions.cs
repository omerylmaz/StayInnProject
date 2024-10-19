using Npgsql;

namespace Reservation.API.Data
{
    public static class Extensions
    {
        public static async void CreateDatabaseIfNotExists(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Database")!;
            string databaseName = "ReservationDb";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string checkDbExistsQuery = $"SELECT 1 FROM pg_database WHERE datname = lower('{databaseName}')";

                using (var checkDbCommand = new NpgsqlCommand(checkDbExistsQuery, connection))
                {
                    var exists = await checkDbCommand.ExecuteScalarAsync();
                    if (exists == null)
                    {
                        string createDbQuery = $"CREATE DATABASE {databaseName}";
                        using (var createDbCommand = new NpgsqlCommand(createDbQuery, connection))
                        {
                            createDbCommand.ExecuteNonQuery();
                            Console.WriteLine($"Database '{databaseName}' created successfully.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Database '{databaseName}' already exists.");
                    }
                }

            }
        }
    }
}
