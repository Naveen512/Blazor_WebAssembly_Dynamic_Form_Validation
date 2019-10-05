using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FormValidation.Validations;

namespace FormValidation.Model
{
    public class UserFormModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string ContactType { get; set; }
        [DynamicContactValidation(ParentFieldName: "ContactType",FieldType:"Email",ValidationTypes:new[] { "required","email" })]
        public string Email { get; set; }
        [DynamicContactValidation(ParentFieldName: "ContactType", FieldType: "Phone", ValidationTypes: new[] { "required", "phone" })]
        public string Phone { get; set; }
    }
}
