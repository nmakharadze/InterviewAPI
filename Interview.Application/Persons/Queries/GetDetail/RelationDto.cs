using System;

namespace Interview.Application.Persons.Queries.GetDetail
{
    /// <summary>
    /// DTO კავშირის ინფორმაცია დაკავშირებული პირის სრული ინფორმაციით
    /// </summary>
    public class RelationDto
    {
        /// <summary>
        /// კავშირის ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// დაკავშირებული ფიზიკური პირის ID
        /// </summary>
        public int RelatedPersonId { get; set; }

        /// <summary>
        /// დაკავშირებული ფიზიკური პირის სახელი
        /// </summary>
        public string RelatedPersonName { get; set; } = string.Empty;

        /// <summary>
        /// დაკავშირებული ფიზიკური პირის გვარი
        /// </summary>
        public string RelatedPersonLastName { get; set; } = string.Empty;

        /// <summary>
        /// დაკავშირებული ფიზიკური პირის პირადი ნომერი
        /// </summary>
        public string RelatedPersonPersonalNumber { get; set; } = string.Empty;

        /// <summary>
        /// დაკავშირებული ფიზიკური პირის დაბადების თარიღი
        /// </summary>
        public DateTime RelatedPersonBirthDate { get; set; }

        /// <summary>
        /// დაკავშირებული ფიზიკური პირის სქესი
        /// </summary>
        public string RelatedPersonGender { get; set; } = string.Empty;

        /// <summary>
        /// დაკავშირებული ფიზიკური პირის ქალაქი
        /// </summary>
        public string RelatedPersonCity { get; set; } = string.Empty;

        /// <summary>
        /// დაკავშირებული ფიზიკური პირის სურათის მისამართი
        /// </summary>
        public string? RelatedPersonImagePath { get; set; }

        /// <summary>
        /// კავშირის ტიპის დასახელება
        /// </summary>
        public string RelationType { get; set; } = string.Empty;

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
