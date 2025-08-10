using System;
using System.Collections.Generic;
using Interview.Application.DTOs.Person;
using MediatR;

namespace Interview.Application.Commands.Person
{
    /// <summary>
    /// ფიზიკური პირის შექმნა სრული ინფორმაციით (ფიზიკური პირი + ტელეფონები + ურთიერთობები)
    /// </summary>
    public class CreatePersonCommand : IRequest<int>
    {
        /// <summary>
        /// ფიზიკური პირის სახელი
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// ფიზიკური პირის გვარი
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// სქესის ID (Gender ცნობარიდან)
        /// </summary>
        public int GenderId { get; set; }

        /// <summary>
        /// პირადი ნომერი
        /// </summary>
        public string PersonalNumber { get; set; } = string.Empty;

        /// <summary>
        /// დაბადების თარიღი
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// ქალაქის ID (City ცნობარიდან)
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// სურათის მისამართი
        /// </summary>
        public string ImagePath { get; set; } = string.Empty;

        /// <summary>
        /// ტელეფონის ნომრების სია (პარამეტრი არის აუცილებელი)
        /// </summary>
        public List<CreatePhoneNumberDto> PhoneNumbers { get; set; } = new();

        /// <summary>
        /// კავშირის სია (პარამეტრი არის აუცილებელი)
        /// </summary>
        public List<CreateRelationDto> Relations { get; set; } = new();

        public CreatePersonCommand() { }

        public CreatePersonCommand(CreatePersonDto dto)
        {
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            GenderId = dto.GenderId;
            PersonalNumber = dto.PersonalNumber;
            BirthDate = dto.BirthDate;
            CityId = dto.CityId;
            ImagePath = dto.ImagePath;
            PhoneNumbers = dto.PhoneNumbers;
            Relations = dto.Relations;
        }
    }
}
