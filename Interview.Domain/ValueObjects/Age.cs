using System;
using System.Collections.Generic;

namespace Interview.Domain.ValueObjects
{
    public class Age : ValueObject
    {
        public int Value { get; private set; }
        
        private Age(int value)
        {
            Value = value;
        }
        
        public static Age Create(int value)
        {
            if (value < 18)
                throw new ArgumentException("ასაკი უნდა იყოს მინიმუმ 18 წელი");
                
            return new Age(value);
        }
        
        public static Age FromBirthDate(DateTime birthDate)
        {
            var age = DateTime.Today.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Today.AddYears(-age)) age--;
            
            return Create(age);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
