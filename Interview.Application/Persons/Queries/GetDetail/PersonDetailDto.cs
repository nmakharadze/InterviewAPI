using System;
using System.Collections.Generic;

namespace Interview.Application.Persons.Queries.GetDetail
{
    /// <summary>
    /// DTO ფიზიკური პირის სრული ინფორმაცია (ძირითადი ინფორმაცია + ტელეფონები + კავშირები)
    /// </summary>
    public class PersonDetailDto
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

        /// <summary>
        /// ტელეფონის ნომრების სია
        /// </summary>
        public List<PhoneNumberDto> PhoneNumbers { get; set; } = new();

        /// <summary>
        /// კავშირების სია
        /// </summary>
        public List<RelationDto> Relations { get; set; } = new();
    }
}
