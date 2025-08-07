using System;
using System.Collections.Generic;
using Interview.Domain.Entities.Base;
using Interview.Domain.Entities.Dictionary;
using Interview.Domain.ValueObjects;

namespace Interview.Domain.Entities.Person
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        public PersonalNumber PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public string ImagePath { get; set; }
        
        // Navigation properties
        public ICollection<PersonPhoneNumber> PhoneNumbers { get; set; }
        public ICollection<PersonRelation> Relations { get; set; }
        
        public Person()
        {
            PhoneNumbers = new List<PersonPhoneNumber>();
            Relations = new List<PersonRelation>();
        }
    }
}
