using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Providers.ActionInformationProviders
{
    public class ActionMembersCompatibilityProvider : IActionMembersCompatibilityProvider
    {
        public IList<ActionDataSenderTypes> GetCompatibleDataSenderTypes(ActionDataProvidersTypes actionDataProvider)
        {
            switch (actionDataProvider)
            {
                case ActionDataProvidersTypes.FileMemoryStream:
                    return new List<ActionDataSenderTypes>()
                    {
                        ActionDataSenderTypes.SendMemoryStreamToUploader
                    };

                case ActionDataProvidersTypes.File:
                    return new List<ActionDataSenderTypes>()
                    {
                        ActionDataSenderTypes.SendFileToUploader
                    };

                default:
                case ActionDataProvidersTypes.NotDefined:
                    return new List<ActionDataSenderTypes>();
            }
        }

        public IList<ActionDataTransformTypes> GetCompatibleDataTransformTypes(ActionDataProvidersTypes actionDataProvider)
        {
            switch (actionDataProvider)
            {
                case ActionDataProvidersTypes.FileMemoryStream:
                    return new List<ActionDataTransformTypes>()
                    {
                        ActionDataTransformTypes.CompressMemoryStream,
                        ActionDataTransformTypes.PlainMemoryStream
                    };

                case ActionDataProvidersTypes.File:
                    return new List<ActionDataTransformTypes>()
                    {
                        ActionDataTransformTypes.CompressFileToDirectory,
                        ActionDataTransformTypes.CopyFileToDirectory
                    };

                default:
                case ActionDataProvidersTypes.NotDefined:
                    return new List<ActionDataTransformTypes>();
            }
        }
    }
}
