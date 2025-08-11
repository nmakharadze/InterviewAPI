using System;
using MediatR;

namespace Interview.Application.Persons.PhoneNumber.Commands.Update
{
    /// <summary>
    /// ტელეფონის ნომრის განახლება
    /// </summary>
    public class UpdatePhoneNumberCommand : IRequest<bool>
    {
        /// <summary>
        /// ტელეფონის ნომრის ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ტელეფონის ნომრის ტიპის ID (PhoneType ცნობარიდან) (optional)
        /// </summary>
        public int? PhoneTypeId { get; set; }

        /// <summary>
        /// ტელეფონის ნომერი (optional)
        /// </summary>
        public string? Number { get; set; }

        public UpdatePhoneNumberCommand() { }

        public UpdatePhoneNumberCommand(int id, UpdatePhoneNumberDto dto)
        {
            Id = id;
            PhoneTypeId = dto.PhoneTypeId;
            Number = dto.Number;
        }
    }
}
