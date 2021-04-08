using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DapperClone
{
    public static class SqlConnectionExtentions
    {
        public static IEnumerable<T> Query<T>(this SqlConnection connection, string query)
            where T : new()
        {
            EnsureConnectionOpened(connection);
            var command = new SqlCommand(query, connection);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return GetFromReader<T>(reader);
            }
        }
        public static async IAsyncEnumerable<T> QueryAsync<T>(this SqlConnection connection, string query)
            where T : new()
        {
            await EnsureConnectionOpenedAsync(connection);
            var command = new SqlCommand(query, connection);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                yield return GetFromReader<T>(reader);
            }
        }

        public static T QueryFirstOrDefault<T>(this SqlConnection connection, string query)
            where T : new()
        {
            EnsureConnectionOpened(connection);
            var command = new SqlCommand(query, connection);

            using var reader = command.ExecuteReader();

            return reader.Read() 
                ? GetFromReader<T>(reader)
                : default;
        }
        public static async Task<T> QueryFirstOrDefaultAsync<T>(this SqlConnection connection, string query)
            where T : new()
        {
            await EnsureConnectionOpenedAsync(connection);
            var command = new SqlCommand(query, connection);

            await using var reader = await command.ExecuteReaderAsync();

            return reader.Read()
                ? GetFromReader<T>(reader)
                : default;
        }

        public static T QueryFirstOrDefault<T>(this SqlConnection connection, string query, object parameters)
            where T : new()
        {
            EnsureConnectionOpened(connection);
            var command = new SqlCommand(query, connection);

            var parameterNames = GetParameterNames(query);

            var paramType = parameters.GetType();
            var paramProperties = paramType.GetProperties();

            foreach (var parameterName in parameterNames)
            {
                var property = paramProperties
                    .First(prop => prop.Name == parameterName);

                command.Parameters.AddWithValue(
                    $"@{property.Name}",
                    property.GetValue(parameters));
            }

            using var reader = command.ExecuteReader();

            return reader.Read()
                ? GetFromReader<T>(reader)
                : default;
        }

        public static async Task<T> QueryFirstOrDefaultAsync<T>(this SqlConnection connection, string query, object parameters)
            where T : new()
        {
            await EnsureConnectionOpenedAsync(connection);
            var command = new SqlCommand(query, connection);

            var parameterNames = GetParameterNames(query);

            var paramType = parameters.GetType();
            var paramProperties = paramType.GetProperties();

            foreach (var parameterName in parameterNames)
            {
                var property = paramProperties
                    .First(prop => prop.Name == parameterName);

                command.Parameters.AddWithValue(
                    $"@{property.Name}",
                    property.GetValue(parameters));
            }

            await using var reader = await command.ExecuteReaderAsync();

            return reader.Read()
                ? GetFromReader<T>(reader)
                : default;
        }

        public static int Execute(this SqlConnection connection, string query, object parameters)
        {
            EnsureConnectionOpened(connection);
            var command = new SqlCommand(query, connection);

            var parameterNames = GetParameterNames(query);

            var paramType = parameters.GetType();
            var paramProperties = paramType.GetProperties();

            foreach (var parameterName in parameterNames)
            {
                var property = paramProperties
                    .First(prop => prop.Name == parameterName);

                command.Parameters.AddWithValue(
                    $"@{property.Name}",
                    property.GetValue(parameters));
            }

            return command.ExecuteNonQuery();
        }

        public static async Task<int> ExecuteAsync(this SqlConnection connection, string query, object parameters)
        {
            await EnsureConnectionOpenedAsync(connection);
            var command = new SqlCommand(query, connection);

            var parameterNames = GetParameterNames(query);

            var paramType = parameters.GetType();
            var paramProperties = paramType.GetProperties();

            foreach (var parameterName in parameterNames)
            {
                var property = paramProperties
                    .First(prop => prop.Name == parameterName);

                command.Parameters.AddWithValue(
                    $"@{property.Name}",
                    property.GetValue(parameters));
            }

            return await command.ExecuteNonQueryAsync();
        }

        private static IEnumerable<string> GetParameterNames(string query)
        {
            var regex = new Regex(@"@([a-zA-Z]\w*)");

            var matches = regex.Matches(query);

            foreach (Match match in matches)
            {
                yield return match.Groups[1].Value;
            }
        }

        private static T GetFromReader<T>(SqlDataReader reader)
            where T : new()
        {
            var type = typeof(T);
            var result = new T();

            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var value = reader[property.Name];
                property.SetValue(result, value);
            }

            return result;
        }

        private static void EnsureConnectionOpened(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }
        private static async Task EnsureConnectionOpenedAsync(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }
        }
    }
}
