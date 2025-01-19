using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts.DTO;

namespace Services.Helpers
{
    public class ValidationHelper
    {   //static is used because we are not creating any object, it can be called diretly using class name 
        internal static void ModelValidation(object obj)
        {
            //Model validations
            ValidationContext validationcontext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            //validationcontext is a class that stores all the information regarding the validation operation
            //validationresults is a class that stores all the information regarding the results of validation, i.e. errors and success messages
            //true is mandatory for validating every validation; if true is not passed, the only required fields will be validated
            bool isValid = Validator.TryValidateObject(obj, validationcontext, validationResults, true);
            if (!isValid)
            {
                //he FirstOrDefault() method in ASP.NET Core, part of LINQ (Language Integrated Query), is used to
                //retrieve the first element of a collection or return a default value if no elements are found
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }

        }
    }
}
