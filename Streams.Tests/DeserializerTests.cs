using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Streams.Tests
{
    public class DeserializerTests
    {
        [Fact]
        public void Ensure_Person_Exists()
        {
            var toTest = new Person()
            {
                Id = 1, FirstName = "F", LastName = "L", IsAdult = false
            };

            const string fileName = "test.csv";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            File.WriteAllText(fileName, 
                "header\n" +
                $"{toTest.Id},{toTest.FirstName},{toTest.LastName},{toTest.IsAdult}");
            
            using var deserializer = new Deserializer(fileName);
            var deserialized = deserializer
                .DeserializeCsv()
                .First();
            
            Assert.True(toTest.Id == deserialized.Id && 
                        toTest.FirstName == deserialized.FirstName &&
                        toTest.LastName == deserialized.LastName &&
                        toTest.IsAdult == deserialized.IsAdult);
        }
    }
}
