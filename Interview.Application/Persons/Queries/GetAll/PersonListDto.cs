using System;

namespace Interview.Application.Persons.Queries.GetAll
{
    /// <summary>
    /// DTO ფიზიკური პირების სია
    /// </summary>
    public class PersonListDto
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
        /// პირადი ნომერი
        /// </summary>
        public string PersonalNumber { get; set; } = string.Empty;

        /// <summary>
        /// ქალაქის დასახელება
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// შექმნის თარიღი
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
