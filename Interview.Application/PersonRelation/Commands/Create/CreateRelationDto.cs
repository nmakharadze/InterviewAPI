using System.ComponentModel.DataAnnotations;

namespace Interview.Application.PersonRelation.Commands.Create
{
    /// <summary>
    /// DTO კავშირის შექმნისთვის
    /// </summary>
    public class CreateRelationDto
    {
        /// <summary>
        /// დაკავშირებული ფიზიკური პირის ID
        /// </summary>
        [Required(ErrorMessage = "დაკავშირებული ფიზიკური პირი სავალდებულოა")]
        [Range(1, int.MaxValue, ErrorMessage = "დაკავშირებული ფიზიკური პირი უნდა იყოს დადებითი რიცხვი")]
        public int RelatedPersonId { get; set; }

        /// <summary>
        /// კავშირის ტიპის ID (RelationType ცნობარიდან)
        /// </summary>
        [Required(ErrorMessage = "კავშირის ტიპი სავალდებულოა")]
        [Range(1, int.MaxValue, ErrorMessage = "კავშირის ტიპი უნდა იყოს დადებითი რიცხვი")]
        public int RelationTypeId { get; set; }
    }
}
