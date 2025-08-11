using System;

namespace Interview.Application.Persons.Queries.GetById
{
    /// <summary>
    /// DTO ფიზიკური პირის ძირითადი ინფორმაცია
    /// </summary>
    public class PersonDto
    {
        /// <summary>
        /// ფიზიკური პირის ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ფიზიკური პირის სახელი
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// ფიზიკური პირის გვარი
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// სქესის დასახელება
        /// </summary>
        public string Gender { get; set; } = string.Empty;

        /// <summary>
        /// პირადი ნომერი
        /// </summary>
        public string PersonalNumber { get; set; } = string.Empty;

        /// <summary>
        /// დაბადების თარიღი
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// ქალაქის დასახელება
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// სურათის მისამართი
        /// </summary>
        public string? ImagePath { get; set; }

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
