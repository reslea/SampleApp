using System;
using Xunit;

namespace Collections.Tests
{
    public class OurListTests
    {
        [Fact]
        public void Add_Test()
        {
            var numberToAdd = 1;
            var ourList = new OurList<int>();

            ourList.Add(numberToAdd);

            Assert.Contains(numberToAdd, ourList);
        }

        [Fact]
        public void Contains_Test()
        {
            var ourList = new OurList<int> { 5 };
            var result = ourList.Contains(5);
            Assert.True(result);
        }
    }
}
