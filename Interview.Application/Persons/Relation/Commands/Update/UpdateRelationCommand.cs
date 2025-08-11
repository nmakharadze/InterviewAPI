using System;
using MediatR;

namespace Interview.Application.Persons.Relation.Commands.Update
{
    /// <summary>
    /// კავშირის განახლება
    /// </summary>
    public class UpdateRelationCommand : IRequest<bool>
    {
        /// <summary>
        /// კავშირის ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// დაკავშირებული ფიზიკური პირის ID (პარამეტრი არის აუცილებელი)
        /// </summary>
        public int? RelatedPersonId { get; set; }

        /// <summary>
        /// კავშირის ტიპის ID (RelationType ცნობარიდან) (პარამეტრი არის აუცილებელი)
        /// </summary>
        public int? RelationTypeId { get; set; }

        public UpdateRelationCommand() { }

        public UpdateRelationCommand(int id, UpdateRelationDto dto)
        {
            Id = id;
            RelatedPersonId = dto.RelatedPersonId;
            RelationTypeId = dto.RelationTypeId;
        }
    }
}
