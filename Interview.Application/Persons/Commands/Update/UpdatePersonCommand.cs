using System;
using Interview.Application.Persons.Commands.Update;
using MediatR;

namespace Interview.Application.Persons.Commands.Update
{
    /// <summary>
    /// ფიზიკური პირის განახლება (მხოლოდ ძირითადი ინფორმაცია)
    /// </summary>
    public class UpdatePersonCommand : IRequest<bool>
    {
        /// <summary>
        /// ფიზიკური პირის ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ფიზიკური პირის სახელი (optional)
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// ფიზიკური პირის გვარი (optional)
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// სქესის ID (Gender ცნობარიდან) (პარამეტრი არის აუცილებელი)
        /// </summary>
        public int? GenderId { get; set; }

        /// <summary>
        /// პირადი ნომერი (პარამეტრი არის აუცილებელი)
        /// </summary>
        public string? PersonalNumber { get; set; }

        /// <summary>
        /// დაბადების თარიღი (პარამეტრი არის აუცილებელი)
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// ქალაქის ID (City ცნობარიდან) (პარამეტრი არის აუცილებელი)
        /// </summary>
        public int? CityId { get; set; }

        public UpdatePersonCommand() { }

        public UpdatePersonCommand(int id, UpdatePersonDto dto)
        {
            Id = id;
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            GenderId = dto.GenderId;
            PersonalNumber = dto.PersonalNumber;
            BirthDate = dto.BirthDate;
            CityId = dto.CityId;
        }
    }
}
