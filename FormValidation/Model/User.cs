using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FormValidation.NewFolder;

namespace FormValidation.Model
{
    public class User
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }

        private string _contactType = "-1";
        public string ContactType
        {
            get
            {
                return _contactType;
            }
            set
            {
                _contactType = value;
            }
        }

        [ConditionalContact(DependOnFieldName: "ContactType",ValueToMatch:"Email",ValidationType:new string[] { "required","email" })]
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
