using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs;
using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.IO;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Action.Senders
{
    public class ObserverSendMemoryStreamToUploaderAction : IObserverActionSender<MemoryStream>
    {
        private UploadItem uploadItem;
        private readonly IAutoSyncUploaderService uploaderServiceClient;

        public ObserverSendMemoryStreamToUploaderAction(
            IAutoSyncUploaderService uploaderServiceClient,
            UploadItem uploadItem)
        {
            this.uploaderServiceClient = uploaderServiceClient;
            this.uploadItem = uploadItem;
        }

        public void Send(MemoryStream objectToSend)
        {
            if (objectToSend != null)
            {
                uploadItem.FileBytes = objectToSend.ToArray();
                uploadItem.UploadTime = DateTime.Now;
                uploaderServiceClient.InsertUploadItem(uploadItem);
                objectToSend.Dispose();
            }
        }
    }
}