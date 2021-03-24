using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataRequestSample
{
    public class Repository<T> : IRepository<T>, IDisposable where T : BaseEntity, new()
    {
        private readonly SqlConnection _connection;
        private readonly PropertyInfo[] _properties;
        private readonly string _tableName;

        public Repository(SqlConnection connection)
        {
            _connection = connection;

            var type = typeof(T);
            _tableName = $"{type.Name}s";
            _properties = type.GetProperties();
        }

        private T Get(SqlDataReader reader)
        {
            var result = new T();
            
            foreach (var property in _properties)
            {
                var value = reader[property.Name];
                property.SetValue(result, value);
            }

            return result;
        }

        void IDisposable.Dispose()
        {
            _connection?.Dispose();
        }

        public IEnumerable<T> Get()
        {
            var query = $"SELECT * FROM {_tableName}";
            var command = new SqlCommand(query, _connection);

            var result = new List<T>();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var receivedObject = Get(reader);
                result.Add(receivedObject);
            }

            return result;
        }

        public T Get(int id)
        {
            var query = $"SELECT * FROM {_tableName} WHERE Id = @id";
            var command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("id", id);

            using var reader = command.ExecuteReader();

            return reader.Read() ? Get(reader) : null;
        }

        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
