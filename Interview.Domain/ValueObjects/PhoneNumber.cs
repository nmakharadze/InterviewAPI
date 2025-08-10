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
                
            // ნომრის გასუფთავება ზედმეტი სიმბოლოებისგან
            var cleanedNumber = Regex.Replace(value, @"[^\d+]", "");
            
            // ავტომატური პრეფიქსის დამატება ქართული მობილურის ნომრებისთვის
            var normalizedNumber = NormalizeGeorgianPhoneNumber(cleanedNumber);
                
            if (!IsValidGeorgianPhoneNumber(normalizedNumber))
                throw new ArgumentException($"არასწორი ქართული ტელეფონის ნომერი: {value}");
                
            return new PhoneNumber(normalizedNumber);
        }
        
        private static string NormalizeGeorgianPhoneNumber(string phoneNumber)
        {
            // თუ ნომერი იწყება +995-ით, დავაბრუნოთ როგორც არის
            if (phoneNumber.StartsWith("+995"))
                return phoneNumber;
                
            // თუ ნომერი იწყება 995-ით, დავამატოთ +
            if (phoneNumber.StartsWith("995"))
                return "+" + phoneNumber;
                
            // თუ ნომერი იწყება 5-ით და არის 9 ციფრი (მობილური ნომერი)
            if (phoneNumber.StartsWith("5") && phoneNumber.Length == 9)
                return "+995" + phoneNumber;
                
            // თუ ნომერი იწყება 032-ით (ქალაქის ნომერი)
            if (phoneNumber.StartsWith("032"))
                return "+995" + phoneNumber;
                
            // თუ ნომერი იწყება 0322-ით (ქალაქის ნომერი)
            if (phoneNumber.StartsWith("0322"))
                return "+995" + phoneNumber;
                
            // სხვა შემთხვევაში დავაბრუნოთ როგორც არის
            return phoneNumber;
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
                
                // +995 032 xxxxxx (ქალაქის ნომრები)
                @"^\+995032\d{6}$",
                
                // +995 0322 xxxxx (ქალაქის ნომრები)
                @"^\+9950322\d{5}$",
                
                // 995 5xx xxxxxx (მობილური ნომრები)
                @"^9955\d{8}$",
                
                // 995 032 xxxxxx (ქალაქის ნომრები)
                @"^995032\d{6}$",
                
                // 995 0322 xxxxx (ქალაქის ნომრები)
                @"^9950322\d{5}$",
                
                // 5xx xxxxxx (მობილური ნომრები)
                @"^5\d{8}$",
                
                // 032 xxxxxx (ქალაქის ნომრები)
                @"^032\d{6}$",
                
                // 0322 xxxxx (ქალაქის ნომრები)
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
