using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Action.Senders
{
    public class ObserverSendFileToUploaderAction : IObserverActionSender<string>
    {
        private UploadItem uploadItem;
        private readonly IAutoSyncUploaderService uploaderServiceClient;

        public ObserverSendFileToUploaderAction(
            IAutoSyncUploaderService uploaderServiceClient,
            UploadItem uploadItem)
        {
            this.uploaderServiceClient = uploaderServiceClient;
            this.uploadItem = uploadItem;
        }

        public void Send(string filePathToSend)
        {
            if (System.IO.File.Exists(filePathToSend))
            {
                uploadItem.FileBytes = System.IO.File.ReadAllBytes(filePathToSend);
                if (filePathToSend.ToLower().StartsWith(@"c:\temp"))
                {
                    System.IO.File.Delete(filePathToSend);
                }
                uploadItem.UploadTime = DateTime.Now;
                uploaderServiceClient.InsertUploadItem(uploadItem);
            }
        }
    }
}