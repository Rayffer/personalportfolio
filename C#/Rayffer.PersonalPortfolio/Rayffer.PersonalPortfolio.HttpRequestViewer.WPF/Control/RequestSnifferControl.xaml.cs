using Newtonsoft.Json.Linq;
using Rayffer.PersonalPortfolio.HttpRequestViewer.WPF.DocumentParsers;
using Rayffer.PersonalPortfolio.HttpRequestViewer.WPF.DTOs;
using Rayffer.PersonalPortfolio.HttpRequestViewer.WPF.Types;
using Rayffer.PersonalPortfolio.HttpRequestViewer.WPF.Validators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Rayffer.PersonalPortfolio.HttpRequestViewer.WPF.Control
{
    /// <summary>
    /// Interaction logic for RequestSnifferControl.xaml
    /// </summary>
    public partial class RequestSnifferControl : UserControl
    {
        #region Fields and properties

        private List<RequestInformation> receivedRequests;
        private Dictionary<string, List<ResponseInformation>> methodsResponseDictionary = new Dictionary<string, List<ResponseInformation>>();
        private int receivedRequestIndex;
        private Regex isNumericRegex;
        private TcpListener tcpListener;
        private Thread listenThread;
        private Thread clientCommunicationHandlerThread;
        private bool KillThreads = false;
        private string validationNumber;
        private string validationDateTime;
        private readonly ASCIIEncoding encoder;
        private readonly BrushConverter brushConverter;
        private readonly string snifferPath;
        private readonly System.Windows.Media.ColorConverter colorConverter;
        private readonly System.Windows.Media.Color controlBackGroundColor;

        #endregion Fields and properties

        public RequestSnifferControl()
        {
            InitializeComponent();
            SetHostUri();
            receivedRequests = new List<RequestInformation>();
            isNumericRegex = new Regex("[^0-9.-]+");
            GenerateAndBindResponseBodyTypes();
            HandlePagingButtonEnabling();
            UpdatePagingLabel();
            brushConverter = new BrushConverter();
            colorConverter = new System.Windows.Media.ColorConverter();
            controlBackGroundColor = (this.Background as SolidColorBrush).Color;
            responseBodyTextBox.Background = new SolidColorBrush(Colors.White);
            encoder = new ASCIIEncoding();
        }

        #region Public methods

        public RequestSnifferControl(string snifferName) : this()
        {
            snifferPath = Path.Combine(Directory.GetCurrentDirectory(), "Sniffers", snifferName);
            Name = snifferName;
            if (Directory.Exists(snifferPath))
            {
                var httpPortFile = Path.Combine(snifferPath, "httpPortFile.config");
                if (File.Exists(httpPortFile))
                {
                    portNumberTextBox.Text = System.IO.File.ReadAllText(httpPortFile);
                }
                foreach (var fileName in Directory.GetFiles(snifferPath, "*.json"))
                {
                    string responseName = Path.GetFileNameWithoutExtension(fileName);
                    try
                    {
                        methodsResponseDictionary.Add(responseName, JsonParser.ReadFromJsonFile<List<ResponseInformation>>(fileName));
                        responseBodiesComboBox.Items.Add(responseName);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"A response body is not valid, response name: {fileName}");
                    }
                }
                responseBodiesComboBox.SelectedIndex = 0;
            }
            else
            {
                Directory.CreateDirectory(snifferPath);
            }
        }

        public void ControlShutdown()
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

        #endregion Public methods

        #region Private methods

        private void GenerateAndBindResponseBodyTypes()
        {
            var responseBodyTypes = new List<ResponseBodyTypes>() { ResponseBodyTypes.Ok, ResponseBodyTypes.Error };
            responseBodyTypeComboBox.ItemsSource = responseBodyTypes;
            responseBodyTypeComboBox.SelectedIndex = 0;
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
            this.textBoxHostURL.Text = $"http://{(string.IsNullOrEmpty(textBoxBaseAddress.Text) ? GetHostName("127.0.0.1") : textBoxBaseAddress.Text)}:{portNumberTextBox.Text}/{textBoxEndpointName.Text}";
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
            clientCommunicationHandlerThread?.Abort();
        }

        private void HandleClientComm(object client)
        {
            using (TcpClient tcpClient = (TcpClient)client)
            using (NetworkStream clientStream = tcpClient.GetStream())
            {
                List<byte> message = new List<byte>();
                RequestInformation request = null;
                int retries = 0;
                while (request == null && retries < 3)
                {
                    while (!message.Any() || ((tcpClient.Available > 0 || clientStream.DataAvailable) && tcpClient.Connected && !KillThreads))
                    {
                        try
                        {
                            int byteToAdd = clientStream.ReadByte();
                            if (byteToAdd == -1)
                                break;
                            message.Add((byte)byteToAdd);
                        }
                        catch
                        {
                            break;
                        }
                        if (!clientStream.DataAvailable)
                            Thread.Sleep(100);
                    }
                    try
                    {
                        request = ProcessRequest(message.ToArray());
                    }
                    catch (Exception)
                    {
                        retries++;
                        Thread.Sleep(200);
                        continue;
                        // Reintentamos el parseo del método
                    }
                }
                
                if (!tcpClient.Connected || KillThreads)
                    return;
                if (request != null)
                {
                    string body = string.Empty;
                    body =
                        responseBodyTypeComboBox.Dispatcher.Invoke(() =>
                        {
                            string requestBody = string.Empty;
                            try
                            {
                                if (!(forceCurrentResponseCheckBox.IsChecked ?? false) && methodsResponseDictionary.ContainsKey(request.PublishedMethod?.Split('/').Last()?.Split('?').First() ?? string.Empty))
                                {
                                    requestBody =
                                        methodsResponseDictionary[request.PublishedMethod.Split('/').Last()?.Split('?').First()]
                                        .FirstOrDefault(responseBody => responseBody.ResponseBodyType == ((ResponseBodyTypes)responseBodyTypeComboBox.SelectedItem))
                                        ?.ResponseBody;
                                }
                                if (string.IsNullOrEmpty(requestBody))
                                {
                                    requestBody = responseBodyTextBox.Dispatcher.Invoke(() => responseBodyTextBox.Text);
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                                throw;
                            }

                            return requestBody;
                        });
                    body = body.Replace("InfoResponse", "ValidationResponse");
                    body = body.Replace("ValidationNumber\":$", string.Format("ValidationNumber\":{0}", validationNumber));
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
                }
                else
                {
                    string body = "The request has no content";
                    string statusLine = "HTTP/1.1 204 NoContent\r\n";
                    string contentType = "Content-Type: application/json\r\n";
                    string contentLength = $"Content-Length: {body.Length}\r\n\r\n";

                    clientStream.Write(encoder.GetBytes(statusLine), 0, statusLine.Length);
                    clientStream.Write(encoder.GetBytes(contentType), 0, contentType.Length);
                    clientStream.Write(encoder.GetBytes(contentLength), 0, contentLength.Length);
                    clientStream.Write(encoder.GetBytes(body), 0, body.Length);

                    statusLine = null;
                    contentType = null;
                    contentLength = null;
                    body = null;
                }
                message.Clear();
                message = null;
                tcpClient.Close();
            }
            GC.Collect();
        }

        private RequestInformation ProcessRequest(byte[] message)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();

            string text = encoder.GetString(message.ToArray(), 0, message.Length) + Environment.NewLine;

            validationNumber = getValidationNumber(text);
            validationDateTime = getValidationDateTime(text);
            var lines = text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            string contentLengthInfo = lines.Where(line => line.ToUpper().Contains("CONTENT-LENGTH")).FirstOrDefault();
            if (string.IsNullOrEmpty(contentLengthInfo))
                throw new ArgumentException();
            var requestBody = lines.Skip(lines.ToList().IndexOf(string.Empty)).Where(line => !string.IsNullOrEmpty(line));
            if (!requestBody.Any())
                throw new ArgumentException();
            dynamic jsonFields;
            try
            {
                jsonFields = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(string.Join("\r\n", requestBody));
            }
            catch (Exception)
            {
                // Si el json está mal formateado y no se puede deserializar
                return null;
            }
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
                RequestImageBytes = (imageBytes != null && new ImageConverter().IsValid(imageBytes)) ? imageBytes : null
            };
            if (receivedRequests.Count >= 50)
            {
                receivedRequests.Remove(receivedRequests.First());
            }
            receivedRequests.Add(request);

            if (receivedRequestIndex >= receivedRequests.Count() - 1)
            {
                receivedRequestIndex = receivedRequests.Count();
                ShowRequest(request);
            }

            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Render,
                (Action)(() =>
                {
                    ColorAnimation colorAnimationUserControl = new ColorAnimation(Colors.LawnGreen, controlBackGroundColor, new Duration(TimeSpan.FromSeconds(0.4F)));
                    this.Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimationUserControl);

                    HandlePagingButtonEnabling();
                }));
            UpdatePagingLabel();
            return request;
        }

        private string getValidationNumber(string text)
        {
            try
            {
                if (text.IndexOf("ValidationNumber") >= 0)
                {
                    var txt = text.Substring(text.IndexOf("ValidationNumber")).Split(',')[0];
                    txt = txt.Split(':')[1];

                    return txt;
                }
            }
            catch (Exception ex)
            {
            }
            return "";
        }

        private string getValidationDateTime(string text)
        {
            try
            {
                if (text.IndexOf("ValidationDateTime") >= 0)
                {
                    var txt = text.Substring(text.IndexOf("ValidationDateTime")).Split(',')[0];
                    txt = txt.Split('\"')[2];

                    return "\"" + txt + "\"";
                }
            }
            catch (Exception ex)
            {
            }
            return "";
        }

        private void ShowRequest(RequestInformation request)
        {
            if (request == null)
                return;
            textBoxPublishedMethod.Dispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Render,
                (Action)(() =>
                {
                    textBoxPublishedMethod.Text = request.PublishedMethod;
                    textBoxReceptionDate.Text = request.ReceptionDate.ToString("dd/MM/yyyy HH:mm:ss");
                    textBoxRequestBody.Text = request.RequestBody;
                    textBoxRequestHeader.Text = request.RequestHeader;
                    Bitmap emptyImage = new Bitmap((int)Math.Max(requestImage.ActualWidth, 1), (int)Math.Max(requestImage.ActualHeight, 1));
                    if (request.RequestImageBytes != null)
                    {
                        MemoryStream memoryStream = new MemoryStream(request.RequestImageBytes);

                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = memoryStream;
                        bitmap.EndInit();
                        bitmap.Freeze();

                        requestImage.Source = bitmap;
                    }
                    else
                    {
                        requestImage.Source = new BitmapImage();
                    }
                }));
        }

        private FrameworkElement FindTopMostParentRecursive(FrameworkElement elementToFindParent)
        {
            if (elementToFindParent.Parent == null)
                return elementToFindParent;
            else
                return FindTopMostParentRecursive(elementToFindParent.Parent as FrameworkElement);
        }

        private void HandlePagingButtonEnabling()
        {
            if (receivedRequestIndex <= 1)
            {
                firstReceivedRequestButton.IsEnabled = false;
                previousReceivedRequestButton.IsEnabled = false;
            }
            else
            {
                firstReceivedRequestButton.IsEnabled = true;
                previousReceivedRequestButton.IsEnabled = true;
            }
            if (receivedRequestIndex >= receivedRequests.Count())
            {
                nextReceivedRequestButton.IsEnabled = false;
                lastReceivedRequestButton.IsEnabled = false;
            }
            else
            {
                nextReceivedRequestButton.IsEnabled = true;
                lastReceivedRequestButton.IsEnabled = true;
            }
        }

        private void UpdatePagingLabel()
        {
            receivedRequestsPagingLabel.Dispatcher.Invoke(() =>
            {
                receivedRequestsPagingLabel.Content = $"{receivedRequestIndex}/{receivedRequests.Count()}";
            });
        }

        private void PerformPaging()
        {
            ShowRequest(receivedRequests.ElementAt(receivedRequestIndex - 1));
            UpdatePagingLabel();
            HandlePagingButtonEnabling();
        }

        private bool IsTextAllowed(string text)
        {
            return !isNumericRegex.IsMatch(text);
        }

        #endregion Private methods

        #region Control events

        private void CreateOrSaveResponseButton_Click(object sender, RoutedEventArgs e)
        {
            if (!JsonStringValidator.IsJsonCompliant(responseBodyTextBox.Text))
            {
                MessageBox.Show($"The json in the textbox cannot be parsed, please fix the following error and try again: \r\n {JsonStringValidator.ErrorMessage}");
                return;
            }
            string filePath = Path.Combine(snifferPath, $"{ responseNameToSaveTextBox.Text}.json");
            if (File.Exists(filePath))
            {
                if (MessageBox.Show("A file with the same method response name already exists, do you want to overwrite it?", string.Empty, MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }
            if (!methodsResponseDictionary.ContainsKey(responseNameToSaveTextBox.Text))
            {
                methodsResponseDictionary[responseNameToSaveTextBox.Text] = new List<ResponseInformation>() { };
            }
            if (methodsResponseDictionary[responseNameToSaveTextBox.Text].Any(responseBody => responseBody.ResponseBodyType == (ResponseBodyTypes)responseBodyTypeComboBox.SelectedItem))
            {
                methodsResponseDictionary[responseNameToSaveTextBox.Text].Remove(methodsResponseDictionary[responseNameToSaveTextBox.Text].FirstOrDefault(responseBody => responseBody.ResponseBodyType == (ResponseBodyTypes)responseBodyTypeComboBox.SelectedItem));
            }
            methodsResponseDictionary[responseNameToSaveTextBox.Text].Add(new ResponseInformation() { ResponseBodyType = (ResponseBodyTypes)responseBodyTypeComboBox.SelectedItem, ResponseBody = responseBodyTextBox.Text });
            if (!responseBodiesComboBox.Items.OfType<string>().Any(item => item.Equals(responseNameToSaveTextBox.Text)))
            {
                responseBodiesComboBox.Items.Add(responseNameToSaveTextBox.Text);
            }
            JsonParser.WriteToJsonFile<List<ResponseInformation>>(filePath, methodsResponseDictionary[responseNameToSaveTextBox.Text]);
            MessageBox.Show("Response saved succesfully");
        }

        private void DeleteResponseButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = Path.Combine(snifferPath, $"{ responseNameToSaveTextBox.Text}.json");
            File.Delete(filePath);
            responseBodiesComboBox.Items.Remove(responseNameToSaveTextBox.Text);
            MessageBox.Show("Response deleted succesfully");
        }

        private void StartLogButton_Click(object sender, RoutedEventArgs e)
        {
            portNumberTextBox.IsEnabled = false;
            startLogButton.IsEnabled = false;
            textBoxBaseAddress.IsEnabled = false;
            textBoxEndpointName.IsEnabled = false;

            int portNumber = int.Parse(portNumberTextBox.Text);
            while (IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners().Any(info => info.Port.Equals(portNumber)))
            {
                portNumber++;
            }
            if (!int.Parse(portNumberTextBox.Text).Equals(portNumber))
            {
                MessageBox.Show($"The port was occupied, so the free port {portNumber} has been automatically selected");
                portNumberTextBox.Text = portNumber.ToString();
            }
            System.IO.File.WriteAllText(Path.Combine(snifferPath, "httpPortFile.config"), portNumberTextBox.Text);
            this.tcpListener = new TcpListener(IPAddress.Any, portNumber);
            this.listenThread = new Thread(new ThreadStart(ThreadProcSafe));
            this.listenThread.Start();
        }

        private void ResponseBodiesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (responseBodiesComboBox.Items.Count > 0)
            {
                if (responseBodiesComboBox.SelectedItem != null)
                {
                    responseBodyTextBox.Text = methodsResponseDictionary[responseBodiesComboBox.SelectedItem as string]
                        .FirstOrDefault(responseBody => responseBody.ResponseBodyType == ((ResponseBodyTypes)responseBodyTypeComboBox.SelectedItem))
                        ?.ResponseBody;
                    responseNameToSaveTextBox.Text = responseBodiesComboBox.SelectedItem as string;
                }
                else
                {
                    responseBodiesComboBox.SelectedIndex = 0;
                }
            }
        }

        private void TextBoxBaseAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetHostUri();
        }

        private void TextBoxEndpointName_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetHostUri();
        }

        private void PortNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void PortNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).IsInitialized)
            {
                SetHostUri();
            }
        }

        private void FirstReceivedRequestButton_Click(object sender, RoutedEventArgs e)
        {
            receivedRequestIndex = 1;
            PerformPaging();
        }

        private void PreviousReceivedRequestButton_Click(object sender, RoutedEventArgs e)
        {
            if (--receivedRequestIndex < 1)
                receivedRequestIndex = 1;
            PerformPaging();
        }

        private void NextReceivedRequestButton_Click(object sender, RoutedEventArgs e)
        {
            if (++receivedRequestIndex > receivedRequests.Count)
                receivedRequestIndex = receivedRequests.Count;
            PerformPaging();
        }

        private void LastReceivedRequestButton_Click(object sender, RoutedEventArgs e)
        {
            receivedRequestIndex = receivedRequests.Count();
            PerformPaging();
        }

        private void ResponseBodyTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (responseBodiesComboBox.Items.Count > 0)
            {
                responseBodyTextBox.Text = methodsResponseDictionary[responseBodiesComboBox.SelectedItem as string]
                .FirstOrDefault(responseBody => responseBody.ResponseBodyType == ((ResponseBodyTypes)responseBodyTypeComboBox.SelectedItem))
                ?.ResponseBody;
            }
        }

        #endregion Control events

        public IEnumerable<T> FindVisualChildren<T>(DependencyObject obj) where T : DependencyObject
        {
            if (obj != null)
            {
                if (obj is T)
                    yield return obj as T;

                foreach (DependencyObject child in LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>())
                    foreach (T c in FindVisualChildren<T>(child))
                        yield return c;
            }
        }

        private void ResponseBodyTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (JsonStringValidator.IsJsonCompliant((sender as TextBox)?.Text ?? string.Empty))
            {
                ColorAnimation colorAnimationUserControl = new ColorAnimation(((sender as TextBox).Background as SolidColorBrush).Color, (System.Windows.Media.Color)colorConverter.ConvertFromInvariantString("#FFFFFFFF"), new Duration(TimeSpan.FromSeconds(0.2F)));
                (sender as TextBox).Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimationUserControl);
            }
            else
            {
                ColorAnimation colorAnimationUserControl = new ColorAnimation(((sender as TextBox).Background as SolidColorBrush).Color, (System.Windows.Media.Color)colorConverter.ConvertFromInvariantString("#55B22222"), new Duration(TimeSpan.FromSeconds(0.2F)));
                (sender as TextBox).Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimationUserControl);
            }
        }
    }
}
