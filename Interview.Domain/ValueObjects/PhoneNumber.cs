using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Interview.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public string Value { get; private set; }
        
        private PhoneNumber(string value)
        {
            Value = value;
        }
        
        public static PhoneNumber Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("ტელეფონის ნომერი არ შეიძლება იყოს ცარიელი");
                
            if (!IsValidGeorgianPhoneNumber(value))
                throw new ArgumentException("არასწორი ქართული ტელეფონის ნომერი");
                
            return new PhoneNumber(value);
        }
        
        private static bool IsValidGeorgianPhoneNumber(string phoneNumber)
        {
            //მოწმდება რომ ნომერი შეიცავდეს მხოლოდ + სიმბოლოს ნომრის დასაწყისში და რიცხვებს
            if (!Regex.IsMatch(phoneNumber, @"^\+?[\d]+$"))
                return false;
                
            // ქართული მობილურის ნომრების ვალიდაცია
            var patterns = new[]
            {
                // +995 5xx xxxxxx (მობილური ნომრები)
                @"^\+9955\d{8}$",
                @"^9955\d{8}$",
                @"^5\d{8}$",
                
                // +995 032 xxxxxx (ქალაქის ნომრები)
                @"^\+995032\d{6}$",
                @"^995032\d{6}$",
                @"^032\d{6}$",
                
                // +995 0322 xxxxx (ქალაქის ნომრები)
                @"^\+9950322\d{5}$",
                @"^9950322\d{5}$",
                @"^0322\d{5}$",
            };
            
            return patterns.Any(pattern => Regex.IsMatch(phoneNumber, pattern));
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
