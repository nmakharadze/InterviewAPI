using System;
using Interview.Application.PersonPhoneNumber.Commands.Create;
using MediatR;

namespace Interview.Application.PersonPhoneNumber.Commands.Create
{
    /// <summary>
    /// ტელეფონის ნომრის დამატება არსებულ ფიზიკურ პირზე
    /// </summary>
    public class CreatePhoneNumberCommand : IRequest<int>
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

        public CreatePhoneNumberCommand() { }

        public CreatePhoneNumberCommand(int personId, CreatePhoneNumberDto dto)
        {
            PersonId = personId;
            PhoneTypeId = dto.PhoneTypeId;
            Number = dto.Number;
        }
    }
}
