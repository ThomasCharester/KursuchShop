using System;
using System.Collections.Generic;
using Resources.Code;
using Resources.Code.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using VContainer;

public class UIQuerySender : MonoBehaviour
{
    
    [Header("Panels")]
    [SerializeField] private AuthorisationPanel authorizationPanel;
    [SerializeField] private ControlPanel controlPanel;
    [SerializeField] private GridContainer gridContainer;
    [SerializeField] private ExceptionPanel exceptionPanel;

    [Header("Misc")]
    [SerializeField] private SimpleTCPClient client;
    
    private Queue<UICommand> _command = new();
    private static UIQuerySender _instance;
    private float _cooldown = 0f;
    private float _maxCooldown = 0.5f;
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
    void FixedUpdate()
    {
        if (_cooldown < 0f && _command.Count > 0)
        {
            _cooldown = _maxCooldown;
            _command.Dequeue().Execute();
        }
        _cooldown -= Time.fixedDeltaTime;
    }

    public void AddCommand(UICommand command) => _command.Enqueue(command);

    public void SendQuery(string query)
    {
        SimpleTCPClient.Instance.SendQuery(query);
    }
    public void ToggleAuthorisePanel(bool active) => authorizationPanel.Toggle(active);

    public void ShowException(string exceptionText)
    {
        exceptionPanel.SetExceptionText(exceptionText);
        exceptionPanel.Show();
    }
    public void ToggleGridContainer(bool active) => gridContainer.Toggle(active);
    public void ToggleControlPanel(bool active) => controlPanel.Toggle(active);
    public void StartGoodsAdding(String goods)
    {
        gridContainer.StartGoodsEdit(goods);
    }

    public void Reconnect()
    {
        // SimpleTCPClient.Instance.ConnectToServer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ActivateShop()
    {
        gridContainer.Show();
        controlPanel.Show();
        
        SendGoodsRequest();
    }

    public void ActivateAccount()
    {
        gridContainer.Hide();
        
    }
    public void ContinueGoodsAdding()
    {
        gridContainer.ContinueGoodsEdit();
    }
    public void ShowGoods(String goods)
    {
        gridContainer.ShowGoods(goods);
    }

    public void EnterAdminMode()
    {
        controlPanel.Show();
    }

    public void SendGoodsRequest()
    {
        AddCommand(new UICommand("gtl;", UICommandType.SendQuery));
    }
}