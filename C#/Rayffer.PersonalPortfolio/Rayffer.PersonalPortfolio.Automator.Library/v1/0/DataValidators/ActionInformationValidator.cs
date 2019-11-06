using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.DataValidators
{
    public class ActionInformationValidator : IDataValidator
    {
        private readonly IActionMembersCompatibilityProvider actionMembersCompatibilityProvider;

        public IList<DataError> DataErrors { get; set; }

        public ActionInformationValidator(IActionMembersCompatibilityProvider actionMembersCompatibilityProvider)
        {
            DataErrors = new List<DataError>();
            this.actionMembersCompatibilityProvider = actionMembersCompatibilityProvider;
        }

        public string GetErrorString()
        {
            return $"{string.Join(", ", DataErrors.Select(err => err.ToString()))}";
        }
        public bool ValidateData<T>(T dataToValidate)
        {
            if (dataToValidate is ActionInformation actionInformationToValidate)
            {
                if (!actionMembersCompatibilityProvider
                    .GetCompatibleDataSenderTypes(actionInformationToValidate.DataProvidersType)
                    .Contains(actionInformationToValidate.DataSenderType))
                {
                    DataErrors.Add(new DataError()
                    {
                        ErrorType = Types.DataErrorTypes.IncompatibleActionMembers,
                        Description = $"The data sender {actionInformationToValidate.DataSenderType.ToString()} is not valid for {actionInformationToValidate.DataProvidersType.ToString()} provider"
                    });

                    return false;
                }
                if (!actionMembersCompatibilityProvider
                    .GetCompatibleDataTransformTypes(actionInformationToValidate.DataProvidersType)
                    .Contains(actionInformationToValidate.DataTransformType))
                {
                    DataErrors.Add(new DataError()
                    {
                        ErrorType = Types.DataErrorTypes.IncompatibleActionMembers,
                        Description = $"The data transformer {actionInformationToValidate.DataTransformType.ToString()} is not valid for {actionInformationToValidate.DataProvidersType.ToString()} provider"
                    });

                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}