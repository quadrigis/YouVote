using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using YouVote.Models.Plugin;
using YouVote.Resources.Models;

namespace YouVote.Models.User
{
    public class UserModel
    {
        public int? Id { get; set; }

        public UserLiteModel Login { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidRes))]
        [RegularExpression(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$", ErrorMessageResourceName = "Type", ErrorMessageResourceType = typeof(ValidRes))]
        [LocalizedDisplayName("Email", NameResourceType = typeof(UserRes))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidRes))]
        [StringLength(100, MinimumLength = 5, ErrorMessageResourceName = "Length", ErrorMessageResourceType = typeof(ValidRes))]
        [LocalizedDisplayName("Password", NameResourceType = typeof(UserRes))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidRes))]
        [Compare("Password", ErrorMessageResourceName = "Compare", ErrorMessageResourceType = typeof(ValidRes))]
        [LocalizedDisplayName("ConfirmPassword", NameResourceType = typeof(UserRes))]
        public string ConfirmPassword { get; set; }

        [LocalizedDisplayName("FirstName", NameResourceType = typeof(UserRes))]
        public string FirstName { get; set; }
        [LocalizedDisplayName("LastName", NameResourceType = typeof(UserRes))]
        public string LastName { get; set; }
        [LocalizedDisplayName("Address", NameResourceType = typeof(UserRes))]
        public string Address { get; set; }
        [LocalizedDisplayName("Age", NameResourceType = typeof(UserRes))]
        public int? Age { get; set; }
        [LocalizedDisplayName("PersonalityNumber", NameResourceType = typeof(UserRes))]
        public string PersonalityNumber { get; set; }

        public int? PersonalityNumberTypeId { get; set; }
    }

    public class UserLiteModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidRes))]
        [RegularExpression(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$", ErrorMessageResourceName = "Type", ErrorMessageResourceType = typeof(ValidRes))]
        [LocalizedDisplayName("Email", NameResourceType = typeof(UserRes))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidRes))]
        [StringLength(100, MinimumLength = 5, ErrorMessageResourceName = "Length", ErrorMessageResourceType = typeof(ValidRes))]
        [LocalizedDisplayName("Password", NameResourceType = typeof(UserRes))]
        public string Password { get; set; }

        [LocalizedDisplayName("RememberMe", NameResourceType = typeof(UserRes))]
        public bool RememberMe { get; set; }
    }
}