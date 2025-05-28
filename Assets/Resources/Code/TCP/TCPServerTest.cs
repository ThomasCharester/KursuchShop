using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.Serialization;
using UnityEngine.UI;


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
            ConnectToServer("127.0.0.1", 8080); //127.0.0.1
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
                                            SessionService.UserAccount.sv_cheats = true;

                                            UIQuerySender.Instance.AddCommand(
                                                new UICommand(UICommandType.AuthoriseDeactivate));
                                            UIQuerySender.Instance.AddCommand(new UICommand(UICommandType.MakeAdmin));
                                            break;
                                        case 'v':
                                            UIQuerySender.Instance.AddCommand(
                                                new UICommand(UICommandType.AuthoriseDeactivate));
                                            break;
                                        case 's':
                                            SessionService.SetAccount(
                                                receivedMessage.Split(DataParsingExtension.QuerySplitter)[1]
                                                    .StringSToAccount());

                                            UIQuerySender.Instance.AddCommand(
                                                new UICommand(UICommandType.AuthoriseDeactivate));
                                            UIQuerySender.Instance.AddCommand(
                                                new UICommand(UICommandType.ActivateShop));
                                            break;
                                    }

                                    break;
                                case 't':
                                {
                                    switch (queryType[2])
                                    {
                                        case 'd':
                                            UIQuerySender.Instance.AddCommand(new UICommand(
                                                receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                                UICommandType.ShowGoodList));
                                            break;
                                        case 'p':
                                            UIQuerySender.Instance.AddCommand(new UICommand(
                                                receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                                UICommandType.ShowGoodListAP));
                                            break;
                                        case 's':
                                            UIQuerySender.Instance.AddCommand(new UICommand(
                                                receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                                UICommandType.ShowGoodListEdit));
                                            break;
                                    }
                                }
                                    break;
                                case 'd':
                                    UIQuerySender.Instance.AddCommand(new UICommand(
                                        receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                        UICommandType.ShowDiseasesList));
                                    SessionService.UpdateDiseases(
                                        receivedMessage.Split(DataParsingExtension.QuerySplitter)[1]);
                                    break;
                                case 'a':
                                    UIQuerySender.Instance.AddCommand(new UICommand(
                                        receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                        UICommandType.ShowAccountList));
                                    break;
                                case 'm':
                                    if (queryType.Length < 3)
                                    {
                                        UIQuerySender.Instance.AddCommand(new UICommand(
                                            receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                            UICommandType.ShowMedicineList));
                                        SessionService.UpdateMedicines(
                                            receivedMessage.Split(DataParsingExtension.QuerySplitter)[1]);
                                    }
                                    else
                                        switch (queryType[2])
                                        {
                                            case 'd':
                                                UIQuerySender.Instance.AddCommand(new UICommand(
                                                    receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                                    UICommandType.ShowMedicinesDiseasesList));
                                                break;
                                            case 'm':
                                                UIQuerySender.Instance.AddCommand(new UICommand(
                                                    receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                                    UICommandType.ShowPlantsMedicinesList));
                                                break;
                                        }
                                    break;
                                case 'p':
                                    if (queryType.Length < 3)
                                    {
                                        UIQuerySender.Instance.AddCommand(new UICommand(
                                            receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                            UICommandType.ShowPlantsList));
                                        SessionService.UpdatePlants(
                                            receivedMessage.Split(DataParsingExtension.QuerySplitter)[1]);
                                    }
                                    else
                                        switch (queryType[2])
                                        {
                                            case 'd':
                                                UIQuerySender.Instance.AddCommand(new UICommand(
                                                    receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                                    UICommandType.ShowPlantsDiseasesList));
                                                break;
                                            case 'm':
                                                UIQuerySender.Instance.AddCommand(new UICommand(
                                                    receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                                    UICommandType.ShowPlantsMedicinesList));
                                                break;
                                        }
                                    break;
                                case 'k':
                                    UIQuerySender.Instance.AddCommand(new UICommand(
                                        receivedMessage.Split(DataParsingExtension.QuerySplitter)[1],
                                        UICommandType.ShowAdminKeysList));
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
                                    //UIQuerySender.Instance.AddCommand(new UICommand(UICommandType.AuthoriseActivate));
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
                                    SessionService.UserAccount.Login =
                                        receivedMessage.Split(DataParsingExtension.QuerySplitter)[1]
                                            .Split(DataParsingExtension.ValueSplitter)[0];
                                    SessionService.UserAccount.Password =
                                        receivedMessage.Split(DataParsingExtension.QuerySplitter)[1]
                                            .Split(DataParsingExtension.ValueSplitter)[1];

                                    UIQuerySender.Instance.AddCommand(new UICommand(UICommandType.RefreshAccountInfo));
                                    break;
                                case 'm':
                                case 'a':
                                case 'd':
                                    UIQuerySender.Instance.AddCommand(new UICommand(UICommandType.SendAccountRequest));
                                    break;
                                case 'k':
                                    switch (queryType[2])
                                    {
                                        case 'm':
                                        case 'a':
                                        case 'd':
                                            UIQuerySender.Instance.AddCommand(
                                                new UICommand(UICommandType.SendAdminKeysRequest));
                                            break;
                                    }

                                    break;
                            }

                            break;
                        case 'p':
                            switch (queryType[1])
                            {
                                case 'd':
                                    switch (queryType[2])
                                    {
                                        case 'm':
                                        case 'a':
                                        case 'd':
                                            UIQuerySender.Instance.AddCommand(
                                                new UICommand(UICommandType.SendDiseasesRequest));
                                            break;
                                    }

                                    break;
                                case 'm':
                                    switch (queryType[2])
                                    {
                                        case 'm':
                                            if (queryType.Length == 3)
                                                UIQuerySender.Instance.AddCommand(
                                                    new UICommand(UICommandType.SendMedicineRequest));
                                            else
                                                switch (queryType[3])
                                                {
                                                    case 'm':
                                                    case 'a':
                                                    case 'd':
                                                        UIQuerySender.Instance.AddCommand(
                                                            new UICommand(UICommandType.SendMedicinesDiseasesRequest));
                                                        break;
                                                }
                                            break;
                                        case 'a':
                                            UIQuerySender.Instance.AddCommand(
                                                new UICommand(UICommandType.SendMedicineRequest));
                                            break;
                                        case 'd':
                                            if (queryType.Length == 3)
                                                UIQuerySender.Instance.AddCommand(
                                                    new UICommand(UICommandType.SendMedicineRequest));
                                            else
                                                switch (queryType[3])
                                                {
                                                    case 'm':
                                                    case 'a':
                                                    case 'd':
                                                        UIQuerySender.Instance.AddCommand(
                                                            new UICommand(UICommandType.SendMedicinesDiseasesRequest));
                                                        break;
                                                }
                                            break;
                                    }

                                    break;
                                case 'p':
                                    switch (queryType[2])
                                    {
                                        case 'm':
                                            if (queryType.Length == 3)
                                                UIQuerySender.Instance.AddCommand(
                                                    new UICommand(UICommandType.SendPlantsRequest));
                                            else
                                                switch (queryType[3])
                                                {
                                                    case 'm':
                                                    case 'a':
                                                    case 'd':
                                                        UIQuerySender.Instance.AddCommand(
                                                            new UICommand(UICommandType.SendPlantsMedicinesRequest));
                                                        break;
                                                }
                                            break;
                                        case 'a':
                                            UIQuerySender.Instance.AddCommand(
                                                new UICommand(UICommandType.SendPlantsRequest));
                                            break;
                                        case 'd':
                                            if (queryType.Length == 3)
                                                UIQuerySender.Instance.AddCommand(
                                                    new UICommand(UICommandType.SendPlantsRequest));
                                            else
                                                switch (queryType[3])
                                                {
                                                    case 'm':
                                                    case 'a':
                                                    case 'd':
                                                        UIQuerySender.Instance.AddCommand(
                                                            new UICommand(UICommandType.SendPlantsDiseasesRequest));
                                                        break;
                                                }

                                            break;
                                    }

                                    break;
                                case 't':
                                    switch (queryType[2])
                                    {
                                        case 'm':
                                            UIQuerySender.Instance.AddCommand(
                                                new UICommand(UICommandType.SendGoodsRequest));
                                            break;
                                        case 'a':
                                            UIQuerySender.Instance.AddCommand(
                                                new UICommand(UICommandType.SendGoodsRequest));
                                            break;
                                        case 'd':
                                            UIQuerySender.Instance.AddCommand(
                                                new UICommand(UICommandType.SendGoodsRequest));
                                            break;
                                        case 'p':
                                            switch (queryType[3])
                                            {
                                                case 'd':
                                                    UIQuerySender.Instance.AddCommand(
                                                        new UICommand(UICommandType.SendGoodsAPRequest));
                                                    break;
                                            }

                                            break;
                                        case 's':
                                            switch (queryType[3])
                                            {
                                                case 'd':
                                                    UIQuerySender.Instance.AddCommand(
                                                        new UICommand(UICommandType.SendGoodsEditRequest));
                                                    break;
                                                case 'm':
                                                    UIQuerySender.Instance.AddCommand(
                                                        new UICommand(UICommandType.SendGoodsEditRequest));
                                                    break;
                                                case 'a':
                                                    UIQuerySender.Instance.AddCommand(
                                                        new UICommand(UICommandType.SendGoodsEditRequest));
                                                    break;
                                            }

                                            break;
                                    }

                                    break;
                            }

                            break;
                    }
                }
#pragma warning disable CS0168 // Variable is declared but never used
                catch (System.Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
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
    }
}