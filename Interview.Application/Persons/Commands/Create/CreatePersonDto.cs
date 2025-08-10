using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Interview.Application.PersonPhoneNumber.Commands.Create;
using Interview.Application.PersonRelation.Commands.Create;

namespace Interview.Application.Persons.Commands.Create
{
    /// <summary>
    /// DTO ფიზიკური პირის შექმნა სრული მონაცემებით (პირი + ტელეფონები + კავშირები)
    /// </summary>
    public class CreatePersonDto
    {
        /// <summary>
        /// ფიზიკური პირის სახელი
        /// </summary>
        [Required(ErrorMessage = "სახელი სავალდებულოა")]
        [StringLength(50, ErrorMessage = "სახელი არ უნდა აღემატებოდეს 50 სიმბოლოს")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// პირის გვარი
        /// </summary>
        [Required(ErrorMessage = "გვარი სავალდებულოა")]
        [StringLength(50, ErrorMessage = "გვარი არ უნდა აღემატებოდეს 50 სიმბოლოს")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// სქესის ID (Gender ცნობარიდან)
        /// </summary>
        [Required(ErrorMessage = "სქესი სავალდებულოა")]
        [Range(1, int.MaxValue, ErrorMessage = "სქესი უნდა იყოს დადებითი რიცხვი")]
        public int GenderId { get; set; }

        /// <summary>
        /// პირადი ნომერი
        /// </summary>
        [Required(ErrorMessage = "პირადი ნომერი სავალდებულოა")]
        [StringLength(11, ErrorMessage = "პირადი ნომერი არ უნდა აღემატებოდეს 11 სიმბოლოს")]
        public string PersonalNumber { get; set; } = string.Empty;

        /// <summary>
        /// დაბადების თარიღი
        /// </summary>
        [Required(ErrorMessage = "დაბადების თარიღი სავალდებულოა")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// ქალაქის ID (City ცნობარიდან)
        /// </summary>
        [Required(ErrorMessage = "ქალაქი სავალდებულოა")]
        [Range(1, int.MaxValue, ErrorMessage = "ქალაქი უნდა იყოს დადებითი რიცხვი")]
        public int CityId { get; set; }

        /// <summary>
        /// სურათის მისამართი
        /// </summary>
        [Required(ErrorMessage = "სურათის მისამართი სავალდებულოა")]
        [StringLength(500, ErrorMessage = "სურათის მისამართი არ უნდა აღემატებოდეს 500 სიმბოლოს")]
        public string ImagePath { get; set; } = string.Empty;

        /// <summary>
        /// ტელეფონის ნომრების სია (optional)
        /// </summary>
        public List<CreatePhoneNumberDto> PhoneNumbers { get; set; } = new();

        /// <summary>
        /// კავშირების სია (optional)
        /// </summary>
        public List<CreateRelationDto> Relations { get; set; } = new();
    }
}
