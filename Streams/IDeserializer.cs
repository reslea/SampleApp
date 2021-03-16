using System;
using System.Collections.Generic;
using System.Text;

namespace Streams
{
    public interface IDeserializer
    {
        IEnumerable<Person> DeserializeCsv();
    }
}
