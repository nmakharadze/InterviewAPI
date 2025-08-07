using Interview.Domain.Entities.Base;
using Interview.Domain.Entities.Dictionary;
using Interview.Domain.ValueObjects;

namespace Interview.Domain.Entities.Person
{
    public class PersonPhoneNumber : BaseEntity
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int PhoneTypeId { get; set; }
        public PhoneType PhoneType { get; set; }
        public PhoneNumber Number { get; set; }
    }
}
