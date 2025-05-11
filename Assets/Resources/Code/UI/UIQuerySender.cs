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
    [Header("Authorization")]
    [SerializeField] private TMP_InputField inputFieldLoginLogin;
    [SerializeField] private TMP_InputField inputFieldLoginPassword;
    [SerializeField] private TMP_InputField inputFieldRegisterLogin;
    [SerializeField] private TMP_InputField inputFieldRegisterPassword;
    [SerializeField] private TMP_InputField inputFieldAdminKey;
    [SerializeField] private TMP_Text exceptionText;
    
    [Header("Panels")]
    [SerializeField] private GameObject authorizationPanel;
    [SerializeField] private GameObject controlPanel;
    [SerializeField] private GameObject gridContainer;
    [SerializeField] private GameObject editButtonsPanel;

    [Header("ElementGrid")]
    [SerializeField] private GameObject plantGridElement;

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

    public void SendLoginQuery()
    {
        string login = inputFieldLoginLogin.text;
        string password = inputFieldLoginPassword.text;

        AddCommand(new UICommand($"l;\'{login}\',\'{password}\'",UICommandType.SendQuery));
    }

    public void SendQuery(string query)
    {
        SimpleTCPClient.Instance.SendQuery(query);
    }

    public void SendRegisterQuery()
    {
        string login = inputFieldRegisterLogin.text;
        string password = inputFieldRegisterPassword.text;
        string adminKey = inputFieldAdminKey.text;

        if (adminKey.Length == 0)
            adminKey = "NAN";

        AddCommand(new UICommand($"r;\'{login}\',\'{password}\',\'{adminKey}\'",UICommandType.SendQuery));
    }

    public void SendGetAllPlantsQuery()
    {
        AddCommand(new UICommand("ppl;", UICommandType.SendQuery));
    }
    public void ActiveAuthorisePanel(bool active) => authorizationPanel.SetActive(active);
    public void SetExceptionText(string text) => exceptionText.text = text;
    public void ActivateGridContainer(bool active) => gridContainer.GetComponent<UISliding>().StartAnimation(active);

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
        plantElement.GetComponent<UIScaling>().StartAnimation();
    }
    public void ShowControlPanel(bool active) => controlPanel.SetActive(active);
    
    public void ShowEditButtons(bool active) => editButtonsPanel.SetActive(active);
    
    private bool _editMode = false;
    public void StartPlantAdding()
    {
        _editMode = true;
        SendGetAllPlantsQuery();
    }
    public void ShowPlants()
    {
        _editMode = false;
        SendGetAllPlantsQuery();
    }

    public void HideGridContainer()
    {
        _editMode = false;
        ActivateGridContainer(false);
        ClearGridContainer();
    }
    public void SwitchGridContainer()
    {
        bool active = !gridContainer.activeSelf;
        ActivateGridContainer(active);
        ClearGridContainer();
    }

    public void EnterGridEditMode(bool active)
    {
        _editMode = active;
    }
    public void EnterAdminMode()
    {
        controlPanel.SetActive(true);
    }
    public void ShowPlantsGrid(String plants)
    {
        if(plants.Length == 0) return;
        
        ActivateGridContainer(true);
        ClearGridContainer();
        
        foreach (var plant in plants.Split(DataParsingExtension.AdditionalQuerySplitter))
        {
            var plantElement = Instantiate(plantGridElement, gridContainer.transform).GetComponent<Plant>();
                plantElement.name = plant.Split(DataParsingExtension.ValueSplitter)[1];
                plantElement.plantId = int.Parse(plant.Split(DataParsingExtension.ValueSplitter)[0]);
            
            plantElement.GetComponent<VerticalContainerPanels>().panels[0].SetActive(true);
            plantElement.GetComponent<NameText>().field.text = plantElement.name;
            plantElement.GetComponent<IdText>().field.text = plantElement.plantId.ToString();
            
            plantElement.GetComponent<UIScaling>().StartAnimation();
        }
        if(_editMode) AddGridElementPlantAdd();
    }
}