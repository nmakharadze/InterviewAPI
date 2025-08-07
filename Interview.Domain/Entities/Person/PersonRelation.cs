using Interview.Domain.Entities.Base;
using Interview.Domain.Entities.Dictionary;

namespace Interview.Domain.Entities.Person
{
    public class PersonRelation : BaseEntity
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int RelatedPersonId { get; set; }
        public Person RelatedPerson { get; set; }
        public int RelationTypeId { get; set; }
        public RelationType RelationType { get; set; }
    }
}
