using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Streams
{
    public class Deserializer : IDeserializer, IDisposable
    {
        private readonly FileStream _fileStream;
        private readonly StreamReader _textReader;

        public Deserializer(string fileName)
        {
            _fileStream = new FileStream(fileName, FileMode.OpenOrCreate);
            _textReader = new StreamReader(_fileStream, Encoding.UTF8);
        }

        public IEnumerable<Person> DeserializeCsv()
        {
            if (!_textReader.EndOfStream)
            {
                var header = _textReader.ReadLine();
            }
            else
            {
                return new List<Person>();
            }
            var allPeopleText = _textReader.ReadToEnd();
            var allPeopleLines = allPeopleText.Split("\r\n");
            var allPeopleProperties = allPeopleLines.Select(l => l.Split(','));
            var people = allPeopleProperties
                .Select(properties => new Person
                {
                    Id = int.Parse(properties[0]),
                    FirstName = properties[1],
                    LastName = properties[2],
                    IsAdult = bool.Parse(properties[3])
                });

            return people;
        }

        public void Dispose()
        {
            _fileStream.Dispose();
            _textReader.Dispose();
        }
    }
}
