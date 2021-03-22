using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Moq;
using Xunit;

namespace Streams.Tests
{
    public class PersonFilterTests
    {
        private readonly Mock<IDeserializer> _deserializer;

        public PersonFilterTests()
        {
            _deserializer = new Mock<IDeserializer>();
        }

        [Fact]
        public void Get_Adults()
        {
            var adult = new Person {Id = 1, IsAdult = true};
            var notAdult = new Person {Id = 2, IsAdult = false};

            _deserializer
                .Setup(d => d.DeserializeCsv())
                .Returns(() => new List<Person> { adult, notAdult });

            var personFilter = new PersonFilter(_deserializer.Object);
            var adults = personFilter.GetAdults();

            _deserializer.Verify(d => d.DeserializeCsv(),
                Times.Once);

            Assert.Contains(adult, adults);
            Assert.DoesNotContain(notAdult, adults);
        }

        class FakeDeserializer : IDeserializer
        {
            public IEnumerable<Person> DeserializeCsv()
            {
                return new List<Person>();
            }
        }
    }
}
