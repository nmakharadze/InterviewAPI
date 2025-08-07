using System;
using System.Collections.Generic;
using System.Linq;

namespace Interview.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public string Value { get; private set; }
        
        private Name(string value)
        {
            Value = value;
        }
        
        public static Name Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("სახელი არ შეიძლება იყოს ცარიელი");
                
            if (value.Length < 2 || value.Length > 50)
                throw new ArgumentException("სახელი უნდა შეიცავდეს 2-50 სიმბოლოს");
                
            // ქართული ან ლათინური ასოების შემოწმება
            var hasGeorgian = value.Any(c => c >= 'ა' && c <= 'ჰ');
            var hasLatin = value.Any(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'));
            
            if (hasGeorgian && hasLatin)
                throw new ArgumentException("სახელი არ შეიძლება შეიცავდეს ქართულ და ლათინურ ასოებს ერთდროულად");
                
            if (!hasGeorgian && !hasLatin)
                throw new ArgumentException("სახელი უნდა შეიცავდეს ქართულ ან ლათინურ ასოებს");
                
            return new Name(value);
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
