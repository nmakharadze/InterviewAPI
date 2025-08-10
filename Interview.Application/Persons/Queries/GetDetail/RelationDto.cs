using System;

namespace Interview.Application.Persons.Queries.GetDetail
{
    /// <summary>
    /// DTO კავშირის ინფორმაცია
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
