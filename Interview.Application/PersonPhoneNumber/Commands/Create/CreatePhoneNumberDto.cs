using System.ComponentModel.DataAnnotations;

namespace Interview.Application.PersonPhoneNumber.Commands.Create
{
    /// <summary>
    /// DTO ტელეფონის ნომრის შექმნისთვის
    /// </summary>
    public class CreatePhoneNumberDto
    {
        /// <summary>
        /// ტელეფონის ნომრის ტიპის ID (PhoneType ცნობარიდან)
        /// </summary>
        [Required(ErrorMessage = "ტელეფონის ნომრის ტიპი სავალდებულოა")]
        [Range(1, int.MaxValue, ErrorMessage = "ტელეფონის ნომრის ტიპი უნდა იყოს დადებითი რიცხვი")]
        public int PhoneTypeId { get; set; }

        /// <summary>
        /// ტელეფონის ნომერი
        /// </summary>
        [Required(ErrorMessage = "ტელეფონის ნომერი სავალდებულოა")]
        [StringLength(50, ErrorMessage = "ტელეფონის ნომერი არ უნდა აღემატებოდეს 50 სიმბოლოს")]
        public string Number { get; set; } = string.Empty;
    }
}
