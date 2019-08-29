using Newtonsoft.Json.Linq;
using Rayffer.PersonalPortfolio.HttpRequestViewer.DTOs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace Rayffer.PersonalPortfolio.HttpRequestViewer
{
    public partial class WebApiExternalSimulator : Form
    {
        private string validationNumber="";
        private string validationDateTime = "";


        private TcpListener tcpListener;
        private Thread listenThread;
        private Thread clientCommunicationHandlerThread;
        private bool KillThreads = false;

        public WebApiExternalSimulator()
        {
            InitializeComponent();
            SetHostUri();
        }

        private string GetHostName(string ipAddress)
        {
            try
            {
                IPHostEntry entry = Dns.GetHostEntry(ipAddress);
                if (entry != null)
                {
                    return entry.HostName;
                }
            }
            catch (SocketException)
            {
                return "127.0.0.1";
            }
            return null;
        }

        private void SetHostUri()
        {
            this.textBoxHostURL.Text = $"{(string.IsNullOrEmpty(textBoxBaseAddress.Text) ? GetHostName("127.0.0.1") : textBoxBaseAddress.Text)}:{portNumberControl.Value}/{textBoxEndpointName.Text}";
        }

        private void portNumberControl_ValueChanged(object sender, EventArgs e)
        {
            SetHostUri();
        }

        private void StartLogButton_Click(object sender, EventArgs e)
        {
            portNumberControl.Enabled = false;
            StartLogButton.Enabled = false;
            textBoxBaseAddress.Enabled= false;
            textBoxEndpointName.Enabled = false;
            this.tcpListener = new TcpListener(IPAddress.Any, (int)portNumberControl.Value);
            this.listenThread = new Thread(new ThreadStart(ThreadProcSafe));
            this.listenThread.Start();
        }

        private void ThreadProcSafe()
        {
            this.tcpListener.Start();

            while (!KillThreads)
            {
                try
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    clientCommunicationHandlerThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                    clientCommunicationHandlerThread.Start(client);
                }
                catch (SocketException socketException)
                {
                    if ((socketException.SocketErrorCode == SocketError.Interrupted))
                        break;
                    else
                        throw;
                }
            }
            clientCommunicationHandlerThread.Abort();
        }

        private void HandleClientComm(object client)
        {
            using (TcpClient tcpClient = (TcpClient)client)
            using (NetworkStream clientStream = tcpClient.GetStream())
            {
                List<byte> message = new List<byte>();
                while (clientStream.DataAvailable && tcpClient.Connected && !KillThreads)
                {
                    try
                    {
                        byte byteToAdd = (byte)clientStream.ReadByte();
                        message.Add(byteToAdd);
                    }
                    catch
                    {
                        break;
                    }
                    if (!clientStream.DataAvailable)
                        Thread.Sleep(500);
                }
                if (!tcpClient.Connected || KillThreads)
                    return;
                try
                {
                    ProcessRequest(message.ToArray());
                }
                catch (Exception)
                {
                    // Para evitar que pete en directo
                }
                ASCIIEncoding encoder = new ASCIIEncoding();

                

                string body = responseBodyTextBox.Text;
                body = body.Replace("InfoResponse","ValidationResponse");
                body =body.Replace("ValidationNumber\":$", string.Format("ValidationNumber\":{0}", validationNumber));
                body = body.Replace("ValidationDateTime\":$", string.Format("ValidationDateTime\":{0}", validationDateTime));
                string statusLine = "HTTP/1.1 200 OK\r\n";
                string contentType = "Content-Type: application/json\r\n";
                string contentLength = $"Content-Length: {body.Length}\r\n\r\n";

                clientStream.Write(encoder.GetBytes(statusLine), 0, statusLine.Length);
                clientStream.Write(encoder.GetBytes(contentType), 0, contentType.Length);
                clientStream.Write(encoder.GetBytes(contentLength), 0, contentLength.Length);
                clientStream.Write(encoder.GetBytes(body), 0, body.Length);
                Thread.Sleep(100);

                statusLine = null;
                contentType = null;
                contentLength = null;
                body = null;

                message.Clear();
                message = null;
                encoder = null;
                tcpClient.Close();
            }
            GC.Collect();
        }

        private void ProcessRequest(byte[] message)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();

            string text = encoder.GetString(message.ToArray(), 0, message.Length) + Environment.NewLine;

            validationNumber=getValidationNumber(text);
            validationDateTime = getValidationDateTime(text);
            var lines = text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            string contentLengthInfo = lines.Where(line => line.ToUpper().Contains("CONTENT-LENGTH")).FirstOrDefault();
            if (string.IsNullOrEmpty(contentLengthInfo))
                return;
            var requestBody = lines.Skip(lines.ToList().IndexOf(string.Empty));
            var jsonFields = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(string.Join("\r\n", requestBody));
            byte[] imageBytes = null;
            IDictionary<string, JToken> Jsondata = JObject.Parse(string.Join("\r\n", requestBody));
            foreach (KeyValuePair<string, JToken> element in Jsondata)
            {
                string innerKey = element.Key;
                if (innerKey.Contains("Image"))
                {
                    imageBytes = Convert.FromBase64String(element.Value.ToString());
                }
            }

            var publishedMethod = (lines.Length > 0) ? lines[0].Split(' ') : null;

            RequestInformation request = new RequestInformation()
            {
                ReceptionDate = DateTime.Now,
                PublishedMethod = (publishedMethod != null && publishedMethod.Length > 2) ? publishedMethod[1] : string.Empty,
                RequestBody = string.Join(Environment.NewLine, (jsonFields != null) ? jsonFields : new List<string>() { string.Empty }),
                RequestHeader = string.Join(Environment.NewLine, lines.Take(lines.ToList().IndexOf(string.Empty))),
                RequestImage = imageBytes != null ? (new ImageConverter().IsValid(imageBytes) ? (Image)(new ImageConverter().ConvertFrom(imageBytes)) : null) : null
            };
            ShowRequest(request);
        }

        private string getValidationNumber(string text)
        {
            try
            {
                var txt = text.Substring(text.IndexOf("ValidationNumber")).Split(',')[0];
                txt= txt.Split(':')[1];

                  return txt;
            }
            catch(Exception ex)
            {

            }
            return "";
        }
        private string getValidationDateTime(string text)
        {
            try
            {
                var txt = text.Substring(text.IndexOf("ValidationDateTime")).Split(',')[0];
                txt = txt.Split('\"')[2];

                return "\"" + txt + "\"";
            }
            catch (Exception ex)
            {

            }
            return "";
        }

        private delegate void ShowRequestInvoke(RequestInformation request);

        private void ShowRequest(RequestInformation request)
        {
            if (textBoxPublishedMethod.InvokeRequired
                || textBoxReceptionDate.InvokeRequired
                || textBoxRequestBody.InvokeRequired
                || textBoxRequestHeader.InvokeRequired
                || pictureBoxRequestImage.InvokeRequired)
            {
                ShowRequestInvoke d = new ShowRequestInvoke(ShowRequest);
                Invoke(d, new object[] { request });
                d = null;
            }
            else
            {
                textBoxPublishedMethod.Text = request.PublishedMethod;
                textBoxReceptionDate.Text = request.ReceptionDate.ToString("dd/MM/yyyy HH:mm:ss");
                textBoxRequestBody.Text = request.RequestBody;
                textBoxRequestHeader.Text = request.RequestHeader;
                if (request.RequestImage != null)
                {
                    var aspectRatio = (double)request.RequestImage.Size.Width / (double)request.RequestImage.Size.Height;
                    var imageWidth = 0;
                    var imageHeight = 0;
                    if (aspectRatio > 1)
                    {
                        imageWidth = pictureBoxRequestImage.Width;
                        imageHeight = (int)(pictureBoxRequestImage.Height / aspectRatio);
                    }
                    else
                    {
                        imageWidth = (int)(pictureBoxRequestImage.Width * aspectRatio);
                        imageHeight = pictureBoxRequestImage.Height;
                    }
                    Bitmap emptyImage = new Bitmap(pictureBoxRequestImage.Width, pictureBoxRequestImage.Height);
                    pictureBoxRequestImage.Image = emptyImage;
                    using (var graphics = Graphics.FromImage(pictureBoxRequestImage.Image))
                    {
                        graphics.CompositingMode = CompositingMode.SourceCopy;
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        var wrapMode = new ImageAttributes();

                        wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                        graphics.DrawImage(request.RequestImage, new Rectangle(pictureBoxRequestImage.Width / 2 - imageWidth / 2, pictureBoxRequestImage.Height / 2 - imageHeight / 2, imageWidth, imageHeight), 0, 0, request.RequestImage.Width, request.RequestImage.Height, GraphicsUnit.Pixel, wrapMode);
                    }
                }
                else
                {
                    Bitmap emptyImage = new Bitmap(pictureBoxRequestImage.Width, pictureBoxRequestImage.Height);
                    request.RequestImage = emptyImage;
                }
            }
        }

        private void TestConnectionButton_Click(object sender, EventArgs e)
        {
            TcpClient tcpClient;

            try
            {
                tcpClient = new TcpClient("127.0.0.1", 3000);
            }
            catch (Exception ex)
            {
                //loggerTextBox.Text += "The listener is not started!" + Environment.NewLine + ex.Message + Environment.NewLine;
                return;
            }

            var stream = tcpClient.GetStream();
            string stringToSend = "Hello server!";
            stream.Write(new ASCIIEncoding().GetBytes(stringToSend), 0, stringToSend.Length);
            tcpClient.Close();
            tcpClient = null;
            GC.Collect();
        }

        private void WebApiExternalSimulator_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (tcpListener == null)
                return;
            KillThreads = true;
            tcpListener.Stop();
            while ((listenThread != null && listenThread.IsAlive) || (clientCommunicationHandlerThread != null && clientCommunicationHandlerThread.IsAlive))
            {
                Thread.Sleep(1000);
            }
            listenThread?.Abort();
            clientCommunicationHandlerThread?.Abort();
            tcpListener = null;
        }

        private void ClearLogButton_Click(object sender, EventArgs e)
        {
            //loggerTextBox.Clear();
            GC.Collect();
        }

        private void textBoxBaseAddress_TextChanged(object sender, EventArgs e)
        {
            SetHostUri();
        }

        private void textBoxEndpointName_TextChanged(object sender, EventArgs e)
        {
            SetHostUri();
        }
    }
}