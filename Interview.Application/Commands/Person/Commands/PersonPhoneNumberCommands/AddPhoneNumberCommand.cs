using System;
using Interview.Application.DTOs.Person;
using MediatR;

namespace Interview.Application.Commands.Person
{
    /// <summary>
    /// ტელეფონის ნომრის დამატება არსებულ ფიზიკურ პირზე
    /// </summary>
    public class AddPhoneNumberCommand : IRequest<int>
    {
        /// <summary>
        /// ფიზიკური პირის ID
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// ტელეფონის ნომრის ტიპის ID (PhoneType ცნობარიდან)
        /// </summary>
        public int PhoneTypeId { get; set; }

        /// <summary>
        /// ტელეფონის ნომერი
        /// </summary>
        public string Number { get; set; } = string.Empty;

        public AddPhoneNumberCommand() { }

        public AddPhoneNumberCommand(int personId, CreatePhoneNumberDto dto)
        {
            PersonId = personId;
            PhoneTypeId = dto.PhoneTypeId;
            Number = dto.Number;
        }
    }
}
