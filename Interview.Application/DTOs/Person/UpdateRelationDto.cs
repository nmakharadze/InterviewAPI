using System.ComponentModel.DataAnnotations;

namespace Interview.Application.DTOs.Person
{
    /// <summary>
    /// DTO კავშირის განახლებისთვის
    /// </summary>
    public class UpdateRelationDto
    {
        /// <summary>
        /// დაკავშირებული ფიზიკური პირის ID (optional)
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "დაკავშირებული ფიზიკური პირი უნდა იყოს დადებითი რიცხვი")]
        public int? RelatedPersonId { get; set; }

        /// <summary>
        /// ურთიერთობის ტიპის ID (RelationType ცნობარიდან) (optional)
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "ურთიერთობის ტიპი უნდა იყოს დადებითი რიცხვი")]
        public int? RelationTypeId { get; set; }
    }
}
