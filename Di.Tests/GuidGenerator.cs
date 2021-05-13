using System;

namespace Di.Tests
{
    public class GuidGenerator : IGuidGenerator
    {
        public Guid Guid { get; set; }

        public GuidGenerator()
        {
            Guid = Guid.NewGuid();
        }
    }

    public interface IGuidGenerator
    {
        Guid Guid { get; set; }
    }
}