using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CarPool.DataValidations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class NotNullAttribute : ValidationAttribute
    {
        // Checks whether string is Null
        public override bool IsValid(object value)
        {
            return string.IsNullOrEmpty(value.ToString()) ? false : true;
        }
    }


    public class EmailAttribute : NotNullAttribute
    {
        // Returns true if it is a valid email address
        public override bool IsValid(object value)
        {
            return new RegularExpressionAttribute(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4})(\]?)$").IsValid(value);
        }

    }

    public class PhoneNumberAttribute : NotNullAttribute
    {
        // Returns true if it is a valid phone number
        public override bool IsValid(object value)
        {
            return new Regex(@"^(\+[1-9][0-9])?[6-9](0-9){9}").IsMatch(value.ToString());
        }
    }

    //public class DataValidater
    //{
    //    // Returns true if the object(object of any data model) satisfies all the validation attributes.
    //    public bool Validate(object model, out List<string> validationResults)
    //    {
    //        validationResults = new List<string>();
    //        foreach (var propertyInfo in model.GetType().GetProperties())
    //        {
    //            foreach (var attribute in propertyInfo.GetCustomAttributes(true))
    //            {
    //                var notNullAttribute = attribute as NotNullAttribute;
    //                if (notNullAttribute != null)
    //                {
    //                    if (!notNullAttribute.IsValid(propertyInfo.GetValue(model)))
    //                    {
    //                        validationResults.Add(notNullAttribute.ErrorMessage);
    //                    }
    //                }
    //            }
    //        }
    //        return validationResults.Count == 0 ? true : false;
    //    }
    //}

    public class PresentOrFutureDateAttribute : NotNullAttribute
    {
        public override bool IsValid(object value)
        {
            if (Convert.ToDateTime(value) >= DateTime.Today)
            {
                return true;
            }
            return false;
        }
    }

    public class GenericValidator
    {
        public static bool Validate(object obj, out ICollection<ValidationResult> results)
        {
            var context = new ValidationContext(obj);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(
                obj, context, results,
                validateAllProperties: true
            );

        }
    }
}
