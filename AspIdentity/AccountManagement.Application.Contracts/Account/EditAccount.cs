using System.ComponentModel.DataAnnotations;
using _01_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.Account
{
    public class EditAccount
    {
        public string Id { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = ValidationMessages.EmailIsNotValid)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Email { get; set; }

        [RegularExpression("[0]{1}[9]{1}[0-9]{9}", ErrorMessage = ValidationMessages.PhoneNumberIsNotValid)]
        public string PhoneNumber { get; set; }

        public IFormFile ProfilePhoto { get; set; }
    }
}