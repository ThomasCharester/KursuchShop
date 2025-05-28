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

        sellerPanel.Toggle(sellerPanel.Hidden);
        goodsPanel.Toggle(sellerPanel.Hidden);
    }

    public void ContinueAccountsAdding() => adminPanel.verticalContainer.ContinueAccountsEdit();
    public void ContinueDiseasesAdding() => adminPanel.verticalContainer.ContinueDiseaseEdit();
    public void ContinueMedicinesAdding() => adminPanel.verticalContainer.ContinueMedicinesEdit();
    public void ContinuePlantsAdding() => adminPanel.verticalContainer.ContinuePlantsEdit();
    public void ContinuePlantsDiseasesAdding() => adminPanel.verticalContainer.ContinuePlantsDiseasesEdit();
    public void ContinuePlantsMedicinesAdding() => adminPanel.verticalContainer.ContinuePlantsMedicinesEdit();
    public void ContinueMedicinesDiseasesAdding() => adminPanel.verticalContainer.ContinueMedicinesDiseasesEdit();
    public void ContinueAdminKeyAdding() => adminPanel.verticalContainer.ContinueAdminKeyEdit();

    public void ContinueGoodsAdding()
    {
        // Скрыть панель редактирования + обновить список товаров
    }

    // public void ShowPlantsDiseases(String goods) => goodsPanel.gridContainer.ShowPlantsDiseases(goods);
    public void ShowGoodsAP(String plantsDiseases) =>
        adminPanel.verticalContainer.StartPlantsDiseasesEdit(plantsDiseases);
    // public void ShowGoodsEdit(String goods) => sellerPanel.gridContainer.ShowGoodsEdit(goods);
    public void ShowAccounts(String accounts) => adminPanel.verticalContainer.StartAccountsEdit(accounts);
    public void ShowDiseases(String games) => adminPanel.verticalContainer.StartDiseasesEdit(games);
    public void ShowPlantsDiseases(String plantsDiseases) => adminPanel.verticalContainer.StartPlantsDiseasesEdit(plantsDiseases);
    public void ShowPlantsMedicines(String plantsDiseases) => adminPanel.verticalContainer.StartPlantsMedicinesEdit(plantsDiseases);
    public void ShowMedicinesDiseases(String medicineDiseases) => adminPanel.verticalContainer.StartMedicinesDiseasesEdit(medicineDiseases);
    public void ShowPlants(String games) => adminPanel.verticalContainer.StartPlantsEdit(games);
    public void ShowMedicines(String games) => adminPanel.verticalContainer.StartMedicinesEdit(games);
    public void ShowAdminKeys(String adminKeys) => adminPanel.verticalContainer.StartAdminKey(adminKeys);
    public void EnterAdminMode() => controlPanel.ToggleAdminMenu(true);

    public void SendPlantsDiseasesRequest() => AddCommand(new UICommand("ppdl;", UICommandType.SendQuery));
    public void SendPlantsMedicinesRequest() => AddCommand(new UICommand("ppml;", UICommandType.SendQuery));
    public void SendMedicinesDiseasesRequest() => AddCommand(new UICommand("pmdl;", UICommandType.SendQuery));
    public void SendDiseasesRequest() => AddCommand(new UICommand("pdl;", UICommandType.SendQuery));
    public void SendGoodsRequest() => AddCommand(new UICommand("gtl;", UICommandType.SendQuery));
    public void SendGoodsAPRequest() => AddCommand(new UICommand("gtpl;", UICommandType.SendQuery));

    public void SendGoodsEditRequest() =>
        AddCommand(new UICommand("gtsl;" + SessionService.UserAccount.Login.DBReadable(), UICommandType.SendQuery));

    public void SendMedicineRequest() => AddCommand(new UICommand("pml;", UICommandType.SendQuery));
    public void SendPlantsRequest() => AddCommand(new UICommand("ppl;", UICommandType.SendQuery));
    public void SendAccountsRequest() => AddCommand(new UICommand("al;", UICommandType.SendQuery));
    public void SendAdminKeysRequest() => AddCommand(new UICommand("akl;", UICommandType.SendQuery));
}