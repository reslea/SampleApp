using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person
            {
                Name = "Alex",
                Age = 15,
                Address = new Address
                {
                    City = "NY",
                    Street = "148 ave",
                    Building = "15a"
                },
                BodyParams = new List<string> { "height: 184" }
            };

            Console.WriteLine(Serialize(person));
        }

        private static string Serialize(object obj)
        {
            var result = string.Empty;

            Type type = obj.GetType();

            PropertyInfo[] properties = type.GetProperties();

            result += $"{type.Name}\n";
            foreach (PropertyInfo property in properties)
            {
                MethodInfo getter = property.GetGetMethod();
                if (getter == null) continue;

                object value = getter.Invoke(obj, new object[] { });
                
                var propType = property.PropertyType;

                var isTypeSimple = propType.IsPrimitive || propType == typeof(string);
                if (isTypeSimple)
                {
                    var serializedValue = value?.ToString() ?? "null";
                    result += $"{property.Name}: {serializedValue}\n";
                }
                else if (value is IEnumerable enumerable)
                {
                    var list = new List<int>();
                    // TODO: fix enumerable output
                    //result += "[";
                    //foreach (object item in enumerable)
                    //{
                    //    result += $"{Serialize(item)}, ";
                    //}

                    //result += "]";
                }
                else
                {
                    result += Serialize(value);
                }
            }

            return result;
        }
    }

    class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Address Address { get; set; }

        public List<string> BodyParams { get; set; }

        //public Person Mother { get; set; }

        //public Person Father { get; set; }
    }

    public class Address
    {
        public string City { get; set; }

        public string Street { get; set; }

        public string Building { get; set; }
    }
}
