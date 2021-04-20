using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Validation
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person { };

            var type = person.GetType();

            //foreach (var property in type.GetProperties())
            //{
            //    var attributes = property.CustomAttributes;

            //    foreach (var attribute in attributes)
            //    {
            //        var attributeType = attribute.AttributeType;
            //        var attributeInstance = Activator.CreateInstance(attributeType);

            //        if (attributeInstance is ValidationAttribute validationAttribute)
            //        {
            //            var validationResult = validationAttribute.IsValid(property.GetValue(person));

            //            Console.WriteLine($"{property.Name}[{attribute}]:{validationResult}");
            //        }
            //    }
            //}

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(person);
            var isValid = Validator.TryValidateObject(person, validationContext, validationResults);
        }
    }

    public class Person
    {
        public int Age { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
