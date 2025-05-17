using System;
using System.Collections.Generic;
using Resources.Code;
using Resources.Code.DataStructures;
using Resources.Code.DataStructures.LiSa;
using Resources.Code.Panels;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using VContainer;

public class UIQuerySender : MonoBehaviour
{
    [Header("Panels")] [SerializeField] private AuthorisationPanel authorizationPanel;
    [SerializeField] private GoodsPanel goodsPanel;
    [SerializeField] private ControlPanel controlPanel;
    [SerializeField] private ExceptionPanel exceptionPanel;
    [SerializeField] private AccountPanel accountPanel;
    [SerializeField] private AdminPanel adminPanel;
    [SerializeField] private SellerPanel sellerPanel;

    [Header("Misc")] [SerializeField] private SimpleTCPClient client;

    private Queue<UICommand> _command = new();
    private static UIQuerySender _instance;
    private float _cooldown = 0f;
    private float _maxCooldown = 0.1f;

    public static UIQuerySender Instance
    {
        get { return _instance; }
        private set { }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _instance = this;

        controlPanel.Hide();
        goodsPanel.Hide();
        accountPanel.Hide();
        adminPanel.Hide();
        sellerPanel.Hide();
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

    public void SendQuery(string query) => SimpleTCPClient.Instance.SendQuery(query);
    public void ToggleAuthorisePanel(bool active) => authorizationPanel.Toggle(active);

    public void ShowException(string exceptionText)
    {
        exceptionPanel.SetExceptionText(exceptionText);
        exceptionPanel.Show();
    }

    public void RefreshAccountInfo() => accountPanel.Clear();

    public void Reconnect()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ActivateShop()
    {
        goodsPanel.Show();
        controlPanel.Show();
        controlPanel.ToggleAccountMenu(true);

        SendGoodsRequest();
    }

    public void HidePanels()
    {
        goodsPanel.Hide();
        accountPanel.Hide();
        adminPanel.Hide();
        exceptionPanel.Hide();
        sellerPanel.Hide();
    }

    public void ToggleAccount()
    {
        if (accountPanel.Hidden)
        {
            adminPanel.Hide();
            sellerPanel.Hide();
        }

        accountPanel.Toggle(accountPanel.Hidden);
        goodsPanel.Toggle(accountPanel.Hidden);
    }

    public void ToggleAdminPanel()
    {
        if (adminPanel.Hidden)
        {
            accountPanel.Hide();
            sellerPanel.Hide();
        }

        adminPanel.Toggle(adminPanel.Hidden);
        goodsPanel.Toggle(adminPanel.Hidden);
    }

    public void ToggleSellerPanel()
    {
        if (sellerPanel.Hidden)
        {
            accountPanel.Hide();
            adminPanel.Hide();
        }

        sellerPanel.Toggle(adminPanel.Hidden);
        goodsPanel.Toggle(adminPanel.Hidden);
    }

    public void ContinueAccountsAdding() => adminPanel.verticalContainer.ContinueAccountsEdit();
    public void ContinueGamesAdding() => adminPanel.verticalContainer.ContinueGamesEdit();
    public void ContinuePaymentMethods() => adminPanel.verticalContainer.ContinuePaymentMethodsEdit();
    public void ContinueSellersAdding() => adminPanel.verticalContainer.ContinueSellersEdit();
    public void ContinueAdminKeyAdding() => adminPanel.verticalContainer.ContinueAdminKeyEdit();

    public void ContinueGoodsAdding()
    {
        // Скрыть панель редактирования + обновить список товаров
    }

    public void ShowGoods(String goods) => goodsPanel.gridContainer.ShowGoods(goods);
    public void ShowGoodsAP(String goods) => adminPanel.verticalContainer.ShowGoods(goods);
    public void ShowGoodsEdit(String goods) => sellerPanel.gridContainer.ShowGoodsEdit(goods);
    public void ShowAccounts(String accounts) => adminPanel.verticalContainer.StartAccountsEdit(accounts);
    public void ShowGames(String games) => adminPanel.verticalContainer.StartGamesEdit(games);
    public void ShowSellers(String games) => adminPanel.verticalContainer.StartSellersEdit(games);
    public void ShowPaymentMethods(String games) => adminPanel.verticalContainer.StartPaymentMethodsEdit(games);
    public void ShowAdminKeys(String adminKeys) => adminPanel.verticalContainer.StartAdminKey(adminKeys);
    public void EnterAdminMode() => controlPanel.ToggleAdminMenu(true);
    public void EnterSellerMode() => controlPanel.ToggleSellerMenu(true);

    public void SendGamesRequest() => AddCommand(new UICommand("ggl;", UICommandType.SendQuery));
    public void SendGoodsRequest() => AddCommand(new UICommand("gtl;", UICommandType.SendQuery));
    public void SendGoodsAPRequest() => AddCommand(new UICommand("gtpl;", UICommandType.SendQuery));

    public void SendGoodsEditRequest() =>
        AddCommand(new UICommand("gtsl;" + UserSessionService.UserAccount.Login.DBReadable(), UICommandType.SendQuery));

    public void SendSellersRequest() => AddCommand(new UICommand("gsl;", UICommandType.SendQuery));
    public void SendAccountsRequest() => AddCommand(new UICommand("al;", UICommandType.SendQuery));
    public void SendPaymentMethodsRequest() => AddCommand(new UICommand("gpl;", UICommandType.SendQuery));
    public void SendAdminKeysRequest() => AddCommand(new UICommand("akl;", UICommandType.SendQuery));
}