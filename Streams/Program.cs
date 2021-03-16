using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Streams
{
    class Program
    {
        static void Main(string[] args)
        {
            using var deserializer = new Deserializer("myFile0.csv");

            var people = deserializer.DeserializeCsv();
        }

        private static IEnumerable<Person> ReadPeopleFromFile(StreamReader textReader)
        {
            var people = new List<Person>();
            while (!textReader.EndOfStream)
            {
                var line = textReader.ReadLine();

                var properties = line.Split(',');

                var person = new Person
                {
                    Id = int.Parse(properties[0]),
                    FirstName = properties[1],
                    LastName = properties[2]
                };

                people.Add(person);
            }

            return people;
        }

        private static void TryFinallyRead()
        {
            var fileStream = new FileStream("test.txt", FileMode.Open);
            try
            {
                var buffer = new byte[4096];
                var offset = 0;
                while (fileStream.CanRead)
                {
                    var bytesCount = fileStream.Read(buffer, offset, buffer.Length);
                    var text = Encoding.UTF8.GetString(buffer, 0, bytesCount);

                    offset += buffer.Length;
                }
            }
            finally
            {
                ((IDisposable)fileStream).Dispose();
            }
        }
    }
}
