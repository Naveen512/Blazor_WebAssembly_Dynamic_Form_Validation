using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FormValidation.Validations
{
    public class DynamicContactValidationAttribute:ValidationAttribute
    {
        private readonly string _parentFieldName;
        private readonly string _fieldType;
        private readonly string[] _validationTypes;

        public DynamicContactValidationAttribute(
            string ParentFieldName,
            string FieldType,
            string[] ValidationTypes)
        {
            _parentFieldName = ParentFieldName;
            _fieldType = FieldType;
            _validationTypes = ValidationTypes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(validationContext.ObjectInstance != null)
            {
                var parentFieldValueObject = validationContext.ObjectInstance.GetType()
                    .GetProperty(_parentFieldName).GetValue(validationContext.ObjectInstance, null);
                string parentFieldValue = parentFieldValueObject != null ? parentFieldValueObject as string : string.Empty;

                string currentFieldValue = value != null ? value as string : string.Empty;

                if (!string.IsNullOrEmpty(parentFieldValue) && parentFieldValue.ToLower() == _fieldType.ToLower())
                {
                    if (string.IsNullOrEmpty(currentFieldValue) &&
                        _validationTypes.Any(_ => _.ToLower() == "required"))
                    {
                        return new ValidationResult($"{validationContext.DisplayName} is requied", new[] { validationContext.MemberName });
                    }
                    else if (_validationTypes.Any(_ => _.ToLower() == "email"))
                    {
                        bool isEmail = Regex.IsMatch(currentFieldValue, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                        if (!isEmail)
                        {
                            return new ValidationResult($"{validationContext.DisplayName} is not a valid email", new[] { validationContext.MemberName });
                        }
                    }
                    else if(_validationTypes.Any(_ => _.ToLower() == "phone"))
                    {
                        bool isPhone = Regex.IsMatch(currentFieldValue, @"\+?[0-9]{10}");
                        if (!isPhone)
                        {
                            return new ValidationResult($"{validationContext.DisplayName} is not a valid phone", new[] { validationContext.MemberName });
                        }
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
