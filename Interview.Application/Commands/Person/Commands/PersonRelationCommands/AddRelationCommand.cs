using System;
using Interview.Application.DTOs.Person;
using MediatR;

namespace Interview.Application.Commands.Person
{
    /// <summary>
    /// კავშირის დამატება
    /// </summary>
    public class AddRelationCommand : IRequest<int>
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

        public AddRelationCommand() { }

        public AddRelationCommand(int personId, CreateRelationDto dto)
        {
            PersonId = personId;
            RelatedPersonId = dto.RelatedPersonId;
            RelationTypeId = dto.RelationTypeId;
        }
    }
}
