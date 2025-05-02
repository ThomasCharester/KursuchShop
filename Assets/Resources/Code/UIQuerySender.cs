using System.Collections.Generic;
using Resources.Code;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

public class UIQuerySender : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputFieldLoginLogin;
    [SerializeField] private TMP_InputField inputFieldLoginPassword;
    [SerializeField] private TMP_InputField inputFieldRegisterLogin;
    [SerializeField] private TMP_InputField inputFieldRegisterPassword;
    [SerializeField] private TMP_InputField inputFieldAdminKey;
    [SerializeField] private TMP_Text exceptionText;
    
    [SerializeField] private GameObject authorizationPanel;
    [SerializeField] private SimpleTCPClient client;

    private Queue<UICommand> _command = new();
    private static UIQuerySender _instance;

    public static UIQuerySender Instance
    {
        get { return _instance; }
        private set { }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (_command.Count > 0)
        {
            _command.Dequeue().Execute();
        }
    }
    
    public void AddCommand(UICommand command) => _command.Enqueue(command);
    
    public void SendLoginQuery()
    {
        string login = inputFieldLoginLogin.text;
        string password = inputFieldLoginPassword.text;
        
        client.SendQuery($"l;\'{login}\',\'{password}\'");
    }
    public void SendRegisterQuery()
    {
        string login = inputFieldRegisterLogin.text;
        string password = inputFieldRegisterPassword.text;
        string adminKey = inputFieldAdminKey.text;

        if (adminKey.Length == 0)
            adminKey = "NAN";
        
        client.SendQuery($"r;\'{login}\',\'{password}\',\'{adminKey}\'");
    }
    public void ActiveAuthorisePanel(bool active) => authorizationPanel.SetActive(active);
    public void SetExceptionText(string text) => exceptionText.text = text;
}
