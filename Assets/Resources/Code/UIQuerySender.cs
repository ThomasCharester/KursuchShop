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
    private SimpleTCPClient _client;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [Inject]
    public void Construct(SimpleTCPClient client)
    {
        _client = client;
    }
    public void SendLoginQuery()
    {
        string login = inputFieldLoginLogin.text;
        string password = inputFieldLoginPassword.text;
        
        _client.SendQuery($"l;\'{login}\',\'{password}\'");
    }
    public void SendRegisterQuery()
    {
        string login = inputFieldRegisterLogin.text;
        string password = inputFieldRegisterPassword.text;
        string adminKey = inputFieldAdminKey.text;
        
        _client.SendQuery($"r;\'{login}\',\'{password}\',\'{adminKey}\'");
    }
    public void ActiveAuthorisePanel(bool active) => authorizationPanel.SetActive(active);
    public void SetExceptionText(string text) => exceptionText.text = text;
}
