using System;

namespace Interview.Application.Persons.Queries.SearchPersons
{
    /// <summary>
    /// DTO ძებნის შედეგებისთვის
    /// გამოიყენება SearchPersons Query-ში ფილტრირებული შედეგების მიღებისთვის
    /// </summary>
    public class SearchResultDto
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
