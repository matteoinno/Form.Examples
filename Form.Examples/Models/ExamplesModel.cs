using Form.Examples.Utils;
using Form.Examples.Views.Examples.App_LocalResources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Web;

namespace Form.Examples.Models
{
    public class ExampleOneViewModel
    {
        //The Required data annotation means that the field cannot be empty.
        //The Display data annotations show the label for the field, set in the View by the Html Helper "LabelFor".
        //Every attribute of the Model has a Display data annotation: the value of the name to display is set by the Local Resource ExampleOne.
        //This local resource can be found in the Views/Examples/App_LocalResources folder.
        [Required]
        [Display(ResourceType = typeof(ExampleOne), Name = "Email")]
        [EmailAddress]
        public String Email { get; set; }
        //Check if the VerifyEmail is equal to the Email member.
        [Required]
        [Compare(nameof(Email))]
        [Display(ResourceType = typeof(ExampleOne), Name = "VerifyEmail")]
        public String VerifyEmail { get; set; }
        [Required]
        [Display(ResourceType = typeof(ExampleOne), Name = "FirstName")]
        public String FirstName { get; set; }
        [Required]
        [Display(ResourceType = typeof(ExampleOne), Name = "LastName")]
        public String LastName { get; set; }
        [Required]
        //IsAdult is a custom data annotation to check if the user is an adult.
        [IsAdult(ErrorMessageResourceType = typeof(ExampleOne), ErrorMessageResourceName = "CheckAdult")]
        [Display(ResourceType = typeof(ExampleOne), Name = "Birthday")]
        public String Birthday { get; set; }
        [Required]
        [Display(ResourceType = typeof(ExampleOne), Name = "ZipCode")]
        public String ZipCode { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(ResourceType = typeof(ExampleOne), Name = "MobilePhone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessageResourceType = typeof(ExampleOne), ErrorMessageResourceName = "CheckMobileNumber")]
        public String MobilePhone { get; set; }
        [Required]
        //To check if the checkbox are selected, I created a custom data annotations that returns the value of the checkbox.
        //If the checkbox is unchecked, the value is false and the check fails.
        //If the checkbox is checked, the value is true and the check passes.
        [IsChecked(ErrorMessageResourceType = typeof(ExampleOne), ErrorMessageResourceName = "CheckPrivacy")]
        [Display(ResourceType = typeof(ExampleOne), Name = "Privacy")]
        public Boolean Privacy { get; set; }
        [Required]
        [IsChecked(ErrorMessageResourceType = typeof(ExampleOne), ErrorMessageResourceName = "CheckReleaseInfo")]
        [Display(ResourceType = typeof(ExampleOne), Name = "ReleaseInfo")]
        public Boolean ReleaseInfo { get; set; }

        public String CurrentCulture { get; set; }
        public String ErrorsList { get; set; }

        public void Init()
        {
            this.CurrentCulture = Thread.CurrentThread.CurrentCulture.EnglishName;
        }

        public void Insert()
        {
            //Here you can insert the code for the database insert operation.
        }

        public void ExtractErrors(System.Web.Mvc.ModelStateDictionary modelState)
        {
            //the dictionary has the name of the attribute as key and the number of errors as value
            Dictionary<string, int> errorList = modelState
                 .ToDictionary(
                     kvp => kvp.Key,
                     kvp => kvp.Value.Errors.Count
                 );
            //Serialize the dictionary and save it in the ErrorsList attribute of the Model.
            //The attribute will be rendered in the view in a input hidden tag.
            this.ErrorsList = JsonConvert.SerializeObject(errorList, Formatting.Indented);

        }
    }

    public class ExampleTwoViewModel
    {
        [Required]
        [Display(ResourceType = typeof(ExampleTwo), Name = "Email")]
        public String Email { get; set; }
        [Required]
        [Display(ResourceType = typeof(ExampleTwo), Name = "FirstName")]
        public String FirstName { get; set; }
        [Required]
        [Display(ResourceType = typeof(ExampleTwo), Name = "LastName")]
        public String LastName { get; set; }
        [Required]
        [Display(ResourceType = typeof(ExampleTwo), Name = "Company")]
        public String Company { get; set; }
        
    }

    public class ExampleThreeViewModel
    {
        [Required]
        public HttpPostedFileBase File { get; set; }

        public void ImportFile()
        {
            string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~"), ConfigurationManager.AppSettings["filesPath"], this.File.FileName);
            this.File.SaveAs(filePath);
        }

    }

    public class IsAdult : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            //To check if the user is an adult, the birthday value, needs to be parsed to a DateTime.
            //If the DateTime parse operation throws an exception, return false.
            try
            {
                //The field is
                DateTime birthDay = DateTime.Parse((string)value, CultureInfo.CurrentCulture);
                DateTime utcNow = DateTime.UtcNow;
                //Subtract the birthday value set in the form from the utcnow. The result, in days, is divided by 365, to get the years.
                Int32 age = ((Int32)(utcNow - birthDay).TotalDays) / 365;
                //Check if the age is greather or equal to the _ADULTAGE, set in the static class Tools.
                if (age >= Tools._ADULTAGE)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }

    public class IsChecked : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return (Boolean)value;
        }
    }
}