using System;
using System.Collections.Generic;
using Resources.Code;
using Resources.Code.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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

    [SerializeField] private GameObject gridContainer;
    [SerializeField] private GameObject plantGridElement;

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
    public void ActivateGridContainer(bool active) => gridContainer.gameObject.SetActive(active);

    public void ClearGridContainer()
    {
        foreach (var child in gridContainer.GetComponentsInChildren<UIScaling>())
            child.DIE();
    }

    public void AddGridElementPlantShow(Plant plant)
    {
        var plantElement = Instantiate(plantGridElement, gridContainer.transform);
        plantElement.GetComponent<VerticalContainerPanels>().panels[0].SetActive(true);
        plantElement.GetComponent<NameText>().field.text = plant.name;
        plantElement.GetComponent<IdText>().field.text = plant.plantId.ToString();
    }

    public void AddGridElementPlantAdd()
    {
        var plantElement = Instantiate(plantGridElement, gridContainer.transform);
        plantElement.GetComponent<VerticalContainerPanels>().panels[1].SetActive(true);
    }

    public void StartPlantAdding()
    {
        ActivateGridContainer(true);
        ClearGridContainer();
        AddGridElementPlantAdd();
    }

    public void ShowPlantsGrid(String plants)
    {
        ActivateGridContainer(true);
        ClearGridContainer();
        foreach (var plant in plants.Split(DataParsingExtension.AdditionalQuerySplitter))
        {
            var plantElement = Instantiate(plantGridElement, gridContainer.transform).GetComponent<Plant>();
            plantElement.name = plant.Split(DataParsingExtension.ValueSplitter)[1];
            plantElement.plantId = int.Parse(plant.Split(DataParsingExtension.ValueSplitter)[0]);
            AddGridElementPlantShow(plantElement);
        }
    }
}