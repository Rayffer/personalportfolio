using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayffer.PersonalPortfolio.HttpRequestViewer.WPF.DTOs
{
    public class RequestInformation
    {
        public DateTime ReceptionDate { get; set; }
        public string PublishedMethod { get; set; }
        public string RequestHeader { get; set; }
        public string RequestBody { get; set; }
        public byte[] RequestImageBytes { get; set; }
    }
}