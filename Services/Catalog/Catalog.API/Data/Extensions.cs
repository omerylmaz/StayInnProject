using Npgsql;

namespace Catalog.API.Data
{
    public static class Extensions
    {
        public static void CreateDatabaseIfNotExists(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Database")!;
            var builder = new NpgsqlConnectionStringBuilder(connectionString);
            builder.Database = "template1";
            connectionString = builder.ConnectionString;
            string databaseName = "CatalogDb";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string checkDbExistsQuery = $"SELECT 1 FROM pg_database WHERE datname = '{databaseName}'";
                using (var checkDbCommand = new NpgsqlCommand(checkDbExistsQuery, connection))
                {
                    var exists = checkDbCommand.ExecuteScalar();
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
