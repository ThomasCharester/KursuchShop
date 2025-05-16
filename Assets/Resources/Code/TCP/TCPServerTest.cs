using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VContainer;


namespace Resources.Code
{
    public class SimpleTCPClient : MonoBehaviour
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private Thread _receiveThread;
        private bool _isConnected = false;

        private static SimpleTCPClient _instance;

        public static SimpleTCPClient Instance
        {
            get { return _instance; }
            private set { }
        }

        private void Start()
        {
            _instance = this;
            ConnectToServer("127.0.0.1", 8888); //127.0.0.1
        }

        private void OnDestroy()
        {
            Disconnect();
        }

        private void ConnectToServer(string serverIP, int serverPort)
        {
            try
            {
                _client = new TcpClient();
                _client.Connect(serverIP, serverPort);
                _stream = _client.GetStream();
                _isConnected = true;

                Debug.Log("Connected to server");

                // Запускаем поток для получения данных
                _receiveThread = new Thread(ReceiveData);
                _receiveThread.IsBackground = true;
                _receiveThread.Start();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Connection error: {e.Message}");
            }
        }

        private void Disconnect()
        {
            if (!_isConnected) return;

            _isConnected = false;

            try
            {
                if (_stream != null) _stream.Close();
                if (_client != null) _client.Close();
                if (_receiveThread != null && _receiveThread.IsAlive) _receiveThread.Abort();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Disconnection error: {e.Message}");
            }

            Debug.Log("Disconnected from server");
        }

        public void SendQuery(string query)
        {
            if (!_isConnected) return;

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(query);
                _stream.Write(data, 0, data.Length);
                Debug.Log($"Sent: {query}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Send error: {e.Message}");
                Disconnect();
            }
        }

        private void ReceiveData()
        {
            byte[] buffer = new byte[1024];
            int bytesRead;

            while (_isConnected)
            {
                try
                {
                    bytesRead = _stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        Disconnect();
                        break;
                    }

                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Debug.Log($"Received: {receivedMessage}");
                    var queryType = receivedMessage.Split(DataParsingExtension.QuerySplitter)[0];
                    switch (queryType[0])
                    {
                        case 'l':
                        {
                            switch (queryType[1])
                            {
                                case 'o':
                                    switch (queryType[2])
                                    {
                                        case 'f':
                                            UIQuerySender.Instance.AddCommand(new UICommand(
                                                receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                                UICommandType.ShowException));
                                            break;
                                        case 'a':
                                            UserSessionService.UserAccount.sv_cheats = true;
                                            
                                            UIQuerySender.Instance.AddCommand(new UICommand(
                                                receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                                UICommandType.AuthoriseDeactivate));
                                            break;
                                        case 'd':
                                            UserSessionService.UserAccount.IsSeller = true;
                                            
                                            UIQuerySender.Instance.AddCommand(new UICommand(
                                                receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                                UICommandType.AuthoriseDeactivate));
                                            break;
                                        case 'v':
                                            UIQuerySender.Instance.AddCommand(new UICommand(
                                                receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                                UICommandType.AuthoriseDeactivate));
                                            break;
                                        case 's':
                                            UserSessionService.SetAccount(receivedMessage.Split(DataParsingExtension.QuerySplitter)[1].StringSToAccount());
                                            UIQuerySender.Instance.AddCommand(new UICommand(
                                                receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                                UICommandType.AuthoriseDeactivate));
                                            UIQuerySender.Instance.AddCommand(new UICommand(
                                                receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                                UICommandType.ActivateShop));
                                            break;
                                    }

                                    break;
                                case 't':
                                    UIQuerySender.Instance.AddCommand(new UICommand(
                                        receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                        UICommandType.ShowGoodList));
                                    break;
                                case 'a':
                                    UIQuerySender.Instance.AddCommand(new UICommand(
                                        receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                        UICommandType.ShowAccountList));
                                    break;
                            }
                        }
                            break;
                        case 'r':
                        {
                            switch (queryType[1])
                            {
                                case 'f':
                                    UIQuerySender.Instance.AddCommand(new UICommand(
                                        receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                        UICommandType.ShowException));
                                    break;
                                case 's':
                                    UIQuerySender.Instance.AddCommand(new UICommand(
                                        receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                        UICommandType.AuthoriseActivate));
                                    break;
                            }
                        }
                            break;
                        case 'e':
                            UIQuerySender.Instance.AddCommand(new UICommand(
                                receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                UICommandType.ShowException));
                            break;
                        case 'a':
                            switch (queryType[1])
                            {
                                case 's':
                                    UserSessionService.UserAccount.Login =
                                        receivedMessage.Split(DataParsingExtension.QuerySplitter)[1]
                                            .Split(DataParsingExtension.ValueSplitter)[0];
                                    UserSessionService.UserAccount.Password =
                                        receivedMessage.Split(DataParsingExtension.QuerySplitter)[1]
                                            .Split(DataParsingExtension.ValueSplitter)[1];
                                    
                                    UIQuerySender.Instance.AddCommand(new UICommand(
                                        receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                        UICommandType.RefreshAccountInfo));
                                    break;
                            }

                            break;
                    }
                }
                catch (System.Exception ex)
                {
                    if (_isConnected)
                    {
                        Debug.Log("Connection lost");
                        Disconnect();
                    }

                    break;
                }
            }
        }

        private void Update()
        {
            // Пример: отправка сообщения по нажатию пробела
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SendQuery("ppl;");
            }
        }
    }
}