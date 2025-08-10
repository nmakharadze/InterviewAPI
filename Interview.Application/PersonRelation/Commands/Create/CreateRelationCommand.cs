using System;
using Interview.Application.PersonRelation.Commands.Create;
using MediatR;

namespace Interview.Application.PersonRelation.Commands.Create
{
    /// <summary>
    /// კავშირის დამატება
    /// </summary>
    public class CreateRelationCommand : IRequest<int>
    {
        /// <summary>
        /// ფიზიკური პირის ID
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// დაკავშირებული პირის ID
        /// </summary>
        public int RelatedPersonId { get; set; }

        /// <summary>
        /// კავშირის ტიპის ID (RelationType ცნობარიდან)
        /// </summary>
        public int RelationTypeId { get; set; }

        public CreateRelationCommand() { }

        public CreateRelationCommand(int personId, CreateRelationDto dto)
        {
            PersonId = personId;
            RelatedPersonId = dto.RelatedPersonId;
            RelationTypeId = dto.RelationTypeId;
        }
    }
}
