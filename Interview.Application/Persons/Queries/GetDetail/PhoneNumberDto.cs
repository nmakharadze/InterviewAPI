using System;

namespace Interview.Application.Persons.Queries.GetDetail
{
    /// <summary>
    /// DTO ტელეფონის ნომრის ინფორმაცია
    /// </summary>
    public class PhoneNumberDto
    {
        /// <summary>
        /// ტელეფონის ნომრის ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ტელეფონის ნომრის ტიპის დასახელება
        /// </summary>
        public string PhoneType { get; set; } = string.Empty;

        /// <summary>
        /// ტელეფონის ნომერი
        /// </summary>
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// შექმნის თარიღი
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// განახლების თარიღი
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}
