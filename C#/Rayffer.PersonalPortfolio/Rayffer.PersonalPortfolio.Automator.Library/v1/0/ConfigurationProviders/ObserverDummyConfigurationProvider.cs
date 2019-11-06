using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.Collections.Generic;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.ConfigurationProviders
{
    public class ObserverDummyConfigurationProvider : IObserverConfigurationProvider
    {
        public ObserverDummyConfigurationProvider()
        {
        }

        public event EventHandler ConfigurationChanged;

        public List<ServiceConfigurationInformation> GetServiceConfigurationInformation()
        {
            return new List<ServiceConfigurationInformation>()
            {
                new ServiceConfigurationInformation()
                {
                    Actions = new List<ActionInformation>()
                    {
                        new ActionInformation()
                        {
                            DataSenderType = Types.ActionDataSenderTypes.SendMemoryStreamToUploader,
                            DataProvidersType = Types.ActionDataProvidersTypes.FileMemoryStream,
                            DataTransformType = Types.ActionDataTransformTypes.CompressMemoryStream,
                            SourceFilePath = @"C:\perrymeison\sourceFile.txt",
                            Identifier = Guid.NewGuid()
                        }
                    },
                    Checks = new List<CheckInformation>()
                    {
                        new CheckInformation()
                        {
                            CheckType = Types.CheckTypes.None,
                            Identifier = Guid.NewGuid(),
                            HashType = Types.HashingTypes.None
                        },
                        new CheckInformation()
                        {
                            CheckType = Types.CheckTypes.CheckHash,
                            Identifier = Guid.NewGuid(),
                            FilePath = @"C:\Cajero\Prueba.txt",
                            HashType = Types.HashingTypes.Md5,
                        }
                    },
                    Events = new List<EventInformation>()
                    {
                        new EventInformation()
                        {
                            EventTriggerType = Types.EventTriggerTypes.NewFilesInDirectory,
                            EventType = Types.EventTypes.NewFilesInDirectory,
                            Identifier = Guid.NewGuid(),
                            DirectoryToWatch = @"C:\perrymeison",
                            FileToWatch = "sourceFile.txt"
                        }
                    }
                }
            };
        }
    }
}