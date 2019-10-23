using Rayffer.PersonalPortfolio.HttpRequestViewer.WPF.Types;

namespace Rayffer.PersonalPortfolio.HttpRequestViewer.WPF.DTOs
{
    public class ResponseInformation
    {
        public ResponseBodyTypes ResponseBodyType { get; set; }
        public string ResponseBody { get; set; }
    }
}