using Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces
{
    public interface IAutoSyncUploaderService
    {
        void InsertUploadItem(UploadItem uploadItem);
    }
}