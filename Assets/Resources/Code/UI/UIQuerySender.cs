using System;
using System.Collections.Generic;
using System.Text;
using Resources.Code;
using Resources.Code.DataStructures;
using Resources.Code.DataStructures.LiSa;
using Resources.Code.Panels;
using Resources.Code.UI.Panels;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class UIQuerySender : MonoBehaviour
{
    [Header("Panels")] [SerializeField] private AuthorisationPanel authorizationPanel;
    [FormerlySerializedAs("goodsPanel")] [SerializeField] private DosagePanel dosagePanel;
    [SerializeField] private ControlPanel controlPanel;
    [SerializeField] private ExceptionPanel exceptionPanel;
    [SerializeField] private AccountPanel accountPanel;
    [SerializeField] private AdminPanel adminPanel;
    [SerializeField] private PlantProcessPanel plantProcessPanel;
    [SerializeField] private LoadingPanel loadingPanel;

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
        dosagePanel.Hide();
        accountPanel.Hide();
        adminPanel.Hide();
        plantProcessPanel.Hide();
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

    public void ActivateWork()
    {
        dosagePanel.Show();
        controlPanel.Show();
        controlPanel.ToggleAccountMenu(true);
        Instance.ShowLoadingPanel();
        AddCommand(new UICommand("pl;", UICommandType.SendQuery));
    }

    public void HidePanels()
    {
        dosagePanel.Hide();
        accountPanel.Hide();
        adminPanel.Hide();
        exceptionPanel.Hide();
        plantProcessPanel.Hide();
    }

    public void ToggleAccount()
    {
        if (accountPanel.Hidden)
        {
            adminPanel.Hide();
            plantProcessPanel.Hide();
        }

        accountPanel.Toggle(accountPanel.Hidden);
        dosagePanel.Toggle(accountPanel.Hidden);
    }

    public void ToggleAdminPanel()
    {
        if (adminPanel.Hidden)
        {
            accountPanel.Hide();
            plantProcessPanel.Hide();
        }

        adminPanel.Toggle(adminPanel.Hidden);
        dosagePanel.Toggle(adminPanel.Hidden);
    }

    public void TogglePlantsProcessPanel(string info)
    {
        if (plantProcessPanel.Hidden)
        {
            accountPanel.Hide();
            adminPanel.Hide();
        }

        plantProcessPanel.ShowInfo(info);
        
        plantProcessPanel.Toggle(plantProcessPanel.Hidden);
        dosagePanel.Toggle(plantProcessPanel.Hidden);
    }
    public void TogglePlantsProcessPanel()
    {
        if (plantProcessPanel.Hidden)
        {
            accountPanel.Hide();
            adminPanel.Hide();
        }
        
        plantProcessPanel.Toggle(plantProcessPanel.Hidden);
        dosagePanel.Toggle(plantProcessPanel.Hidden);
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

    // public void ShowPlantsDiseases(String goods) => dosagePanel.gridContainer.ShowPlantsDiseases(goods);
    public void ShowGoodsAP(String plantsDiseases) =>
        adminPanel.verticalContainer.StartPlantsDiseasesEdit(plantsDiseases);

    // public void ShowGoodsEdit(String goods) => plantProcessPanel.gridContainer.ShowGoodsEdit(goods);
    public void ShowAccounts(String accounts) => adminPanel.verticalContainer.StartAccountsEdit(accounts);
    public void ShowDiseases(String games) => adminPanel.verticalContainer.StartDiseasesEdit(games);

    public void ShowPlantsDiseases(String plantsDiseases) =>
        adminPanel.verticalContainer.StartPlantsDiseasesEdit(plantsDiseases);

    public void ShowPlantsMedicines(String plantsDiseases) =>
        adminPanel.verticalContainer.StartPlantsMedicinesEdit(plantsDiseases);

    public void ShowMedicinesDiseases(String medicineDiseases) =>
        adminPanel.verticalContainer.StartMedicinesDiseasesEdit(medicineDiseases);

    public void ShowLoadingPanel() => loadingPanel.Show();
    public void HideLoadingPanel() => loadingPanel.Hide();
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

    public void SendTestPointRequest()
    {
        // int pointsCount = Random.Range(1, 5);
        //
        // float workRadius = 10.0f;
        // float dosage = 1.0f;
        //
        // StringBuilder output = new();
        //
        // output.Append("pz" + DataParsingExtension.AdditionalQuerySplitter);
        //
        // for (int i = 0; i < pointsCount; i++)
        //     output.Append(Random.Range(0, 100).ToString() 
        //                   + DataParsingExtension.ValueSplitter 
        //                   + Random.Range(0, 100).ToString()
        //                   + DataParsingExtension.QuerySplitter);
        //
        // output.Remove(output.Length - 1, 1);
        // output.Append(DataParsingExtension.AdditionalQuerySplitter + dosage.ToString() + DataParsingExtension.AdditionalQuerySplitter);
        // output.Append(workRadius.ToString());
        //
        // AddCommand(new UICommand(output.ToString(), UICommandType.SendQuery));
        Instance.ShowLoadingPanel();
        AddCommand(new UICommand("pl;", UICommandType.SendQuery));
    }
    public void SendPointRequest(float dosage, string additionalData)
    {
        int pointsCount = Random.Range(1, 5);

        float workRadius = 10.0f;

        StringBuilder output = new();
        
        output.Append("pz" + DataParsingExtension.AdditionalQuerySplitter);
        
        for (int i = 0; i < pointsCount; i++)
            output.Append(Random.Range(0, 100).ToString() 
                          + DataParsingExtension.ValueSplitter 
                          + Random.Range(0, 100).ToString()
                          + DataParsingExtension.QuerySplitter);
        
        output.Remove(output.Length - 1, 1);
        output.Append(DataParsingExtension.AdditionalQuerySplitter + dosage.ToString() + DataParsingExtension.AdditionalQuerySplitter);
        output.Append(workRadius.ToString());
        output.Append(additionalData);
        
        AddCommand(new UICommand(output.ToString(), UICommandType.SendQuery));
    }
}