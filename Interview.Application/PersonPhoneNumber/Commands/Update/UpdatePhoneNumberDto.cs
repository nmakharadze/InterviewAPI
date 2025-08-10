using System.ComponentModel.DataAnnotations;

namespace Interview.Application.PersonPhoneNumber.Commands.Update
{
    /// <summary>
    /// DTO ტელეფონის ნომრის განახლებისთვის
    /// </summary>
    public class UpdatePhoneNumberDto
    {
        /// <summary>
        /// ტელეფონის ნომრის ტიპის ID (PhoneType ცნობარიდან) (optional)
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "ტელეფონის ნომრის ტიპი უნდა იყოს დადებითი რიცხვი")]
        public int? PhoneTypeId { get; set; }

        /// <summary>
        /// ტელეფონის ნომერი (optional)
        /// </summary>
        [StringLength(50, ErrorMessage = "ტელეფონის ნომერი არ უნდა აღემატებოდეს 50 სიმბოლოს")]
        public string? Number { get; set; }
    }
}
