using System;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.DTOs
{
    public class UploadItem
    {
        public DateTime UploadTime { get; set; }
        public byte[] FileBytes { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}