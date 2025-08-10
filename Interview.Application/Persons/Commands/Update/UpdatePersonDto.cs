using System;
using System.ComponentModel.DataAnnotations;

namespace Interview.Application.Persons.Commands.Update
{
    /// <summary>
    /// DTO ფიზიკური პირის განახლებისთვის (მხოლოდ ძირითადი ინფორმაცია)
    /// </summary>
    public class UpdatePersonDto
    {
        /// <summary>
        /// ფიზიკური პირის სახელი (optional)
        /// </summary>
        [StringLength(50, ErrorMessage = "სახელი არ უნდა აღემატებოდეს 50 სიმბოლოს")]
        public string? FirstName { get; set; }

        /// <summary>
        /// ფიზიკური პირის გვარი (optional)
        /// </summary>
        [StringLength(50, ErrorMessage = "გვარი არ უნდა აღემატებოდეს 50 სიმბოლოს")]
        public string? LastName { get; set; }

        /// <summary>
        /// სქესის ID (Gender ცნობარიდან) (optional)
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "სქესი უნდა იყოს დადებითი რიცხვი")]
        public int? GenderId { get; set; }

        /// <summary>
        /// პირადი ნომერი (optional)
        /// </summary>
        [StringLength(11, ErrorMessage = "პირადი ნომერი არ უნდა აღემატებოდეს 11 სიმბოლოს")]
        public string? PersonalNumber { get; set; }

        /// <summary>
        /// დაბადების თარიღი (optional)
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// ქალაქის ID (City ცნობარიდან) (optional)
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "ქალაქი უნდა იყოს დადებითი რიცხვი")]
        public int? CityId { get; set; }
    }
}
