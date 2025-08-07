using System;
using System.Collections.Generic;
using System.IO;

namespace Interview.Domain.ValueObjects
{
    public class ImagePath : ValueObject
    {
        public string Value { get; private set; }
        
        private ImagePath(string value)
        {
            Value = value;
        }
        
        public static ImagePath Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("სურათის მისამართი არ შეიძლება იყოს ცარიელი");
                
            if (value.Length > 500)
                throw new ArgumentException("სურათის მისამართი არ უნდა აღემატებოდეს 500 სიმბოლოს");
                
            // შემოწმება რომ არის ვალიდური ფაილის გაფართოება
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var extension = Path.GetExtension(value)?.ToLowerInvariant();
            
            if (!string.IsNullOrEmpty(extension) && !allowedExtensions.Contains(extension))
                throw new ArgumentException("არასწორი ფაილის ფორმატი. დაშვებულია: jpg, jpeg, png, gif, bmp");
                
            return new ImagePath(value);
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
