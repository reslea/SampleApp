using System;
using System.ComponentModel.DataAnnotations;

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

        public override bool Equals(object? obj)
        {
            if (obj is Person person)
            {
                return Name == person.Name
                       && Age == person.Age;
            }
            return false;
        }
    }
}