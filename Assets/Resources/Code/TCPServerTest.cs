using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;


namespace Resources.Code
{
public class SimpleTCPClient : MonoBehaviour
{
    private TcpClient _client;
    private NetworkStream _stream;
    private Thread _receiveThread;
    private bool _isConnected = false;

    private void Start()
    {
        ConnectToServer("127.0.0.1", 8888);//127.0.0.1
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

            // Отправляем тестовое сообщение
            SendMessage("am;Elissia|228|Nan");
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

    private void SendMessage(string message)
    {
        if (!_isConnected) return;

        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            _stream.Write(data, 0, data.Length);
            Debug.Log($"Sent: {message}");
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
            }
            catch (System.Exception)
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
            SendMessage("Space pressed!");
        }
    }
}
}