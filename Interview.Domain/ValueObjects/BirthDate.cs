using System;
using System.Collections.Generic;

namespace Interview.Domain.ValueObjects
{
    public class BirthDate : ValueObject
    {
        public DateTime Value { get; private set; }
        
        private BirthDate(DateTime value)
        {
            Value = value;
        }
        
        public static BirthDate Create(DateTime value)
        {
            if (value > DateTime.Today)
                throw new ArgumentException("დაბადების თარიღი არ შეიძლება იყოს მომავალში");
                
            var age = DateTime.Today.Year - value.Year;
            if (value.Date > DateTime.Today.AddYears(-age)) age--;
            
            if (age < 18)
                throw new ArgumentException("პირი უნდა იყოს მინიმუმ 18 წლის");
                
            return new BirthDate(value);
        }
        
        public int GetAge()
        {
            var age = DateTime.Today.Year - Value.Year;
            if (Value.Date > DateTime.Today.AddYears(-age)) age--;
            return age;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        
        public override string ToString()
        {
            return Value.ToString("yyyy-MM-dd");
        }
    }
}
