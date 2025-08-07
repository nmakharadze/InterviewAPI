using System;
using System.Collections.Generic;
using System.Linq;

namespace Interview.Domain.ValueObjects
{
    public class PersonalNumber : ValueObject
    {
        public string Value { get; private set; }
        
        private PersonalNumber(string value)
        {
            Value = value;
        }
        
        public static PersonalNumber Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("პირადი ნომერი არ შეიძლება იყოს ცარიელი");
                
            if (value.Length != 11)
                throw new ArgumentException("პირადი ნომერი უნდა შეიცავდეს ზუსტად 11 ციფრს");
                
            if (!value.All(char.IsDigit))
                throw new ArgumentException("პირადი ნომერი უნდა შეიცავდეს მხოლოდ ციფრებს");
                
            return new PersonalNumber(value);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        
        public override string ToString()
        {
            return Value;
        }
    }
}
