using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.DataValidators
{
    public class DataValidator : IDataValidator
    {
        public IList<DataError> DataErrors { get; set; }

        public DataValidator()
        {
            DataErrors = new List<DataError>();
        }

        public bool ValidateData<T>(T dataToValidate)
        {
            if (dataToValidate == null)
            {
                DataErrors.Add(new DataError() { ErrorType = Types.DataErrorTypes.ReferenceNull, Description = "" });
                return !DataErrors.Any();
            }
            if (dataToValidate is string stringToValidate)
            {
                if (stringToValidate == string.Empty)
                {
                    DataErrors.Add(new DataError() { ErrorType = Types.DataErrorTypes.EmptyString, Description = "" });
                }
                if (Regex.Match(stringToValidate, @"^(?:[\w]\:|\\)(\\[A-Za-z_\-\s0-9\.]+)+$").Success)
                {
                    if (!CheckDirectoryExistence(stringToValidate))
                    {
                        DataErrors.Add(new DataError() { ErrorType = Types.DataErrorTypes.DirectoryDoesNotExist, Description = "" });
                    }
                }
                else if (Regex.Match(stringToValidate, @"^(?:[\w]\:|\\)?(\\?[A-Za-z_\-\s0-9\.]+)+\.[\w]*$").Success)
                {
                    if (!CheckFileExistence(stringToValidate))
                    {
                        DataErrors.Add(new DataError() { ErrorType = Types.DataErrorTypes.FileDoesNotExist, Description = "" });
                    }
                }
                //TODO manejar filtros

                return !DataErrors.Any();
            }
            if (dataToValidate is int integerToValidate)
            {
                if (CheckIntegerIsZero(integerToValidate))
                {
                    DataErrors.Add(new DataError() { ErrorType = Types.DataErrorTypes.ValueCannotBeZero, Description = "" });
                }
                return !DataErrors.Any();
            }
            return !DataErrors.Any();
        }

        private static bool CheckIntegerIsZero(int integerToValidate)
        {
            return integerToValidate.Equals(0);
        }

        private static bool CheckDirectoryExistence(string stringToValidate)
        {
            return System.IO.Directory.Exists(stringToValidate);
        }

        private static bool CheckFileExistence(string stringToValidate)
        {
            return System.IO.File.Exists(stringToValidate);
        }

        public string GetErrorString()
        {
            return $"{string.Join(", ", DataErrors.Select(err => err.ToString()))}";
        }
    }
}