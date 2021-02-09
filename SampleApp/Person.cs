using System;

namespace SampleApp
{
    public class Person
    {
        public int? Age { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public Person()
        {

        }

        public Person(string name, int? age, string address)
        {
            Name = name ?? throw new ArgumentNullException();
            Age = age ?? throw new ArgumentNullException();
            Address = address ?? throw new ArgumentNullException();
        }

        public override string ToString()
        {
            return $"{Name} from {Address} has Age: {Age}";
        }
    }
}