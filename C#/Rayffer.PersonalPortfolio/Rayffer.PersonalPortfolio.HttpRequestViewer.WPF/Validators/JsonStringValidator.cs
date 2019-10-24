using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayffer.PersonalPortfolio.HttpRequestViewer.WPF.Validators
{
    public static class JsonStringValidator
    {
        public static string ErrorMessage;
        public static bool IsJsonCompliant(string textToCheck)
        {
            try
            {
                var jObject = JObject.Parse(textToCheck ?? string.Empty);
                ErrorMessage = string.Empty;
                foreach (var jo in jObject)
                {
                    string name = jo.Key;
                    JToken value = jo.Value;

                    //if the element has a missing value, it will be Undefined - this is invalid
                    if (value.Type== JTokenType.Undefined)
                    {
                        ErrorMessage = $"The element \"{name}\" is missing a value";
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

    }
}
