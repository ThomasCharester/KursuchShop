using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class PlantElement: DataStructureElement
    {
        public Plant Plant = new();
        [SerializeField] private GameObject inputPanel;
        [SerializeField] private GameObject outputPanel;

        [SerializeField] private TMP_InputField inputFieldName;

        [SerializeField] private TMP_Text textName;
        [SerializeField] private TMP_Text id;
        
        public void ToggleEditMode(bool edit)
        {
            inputPanel.SetActive(edit);
            outputPanel.SetActive(!edit);
        }

        public void UpdateTextValues()
        {
            textName.text = Plant.PlantName;
            id.text = Plant.Id.ToString();
        }
        
        public String FormOutputValue()
        {
            return (inputFieldName.text == "" ? Plant.PlantName : inputFieldName.text).DBReadable();
        }

        public void SendModifyQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"ppm;" + FormOutputValue() + DataParsingExtension.AdditionalQuerySplitter + Plant.PlantToString(),
                UICommandType.SendQuery));
        }
        public void SendDeleteQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"ppd;" + Plant.Id,
                UICommandType.SendQuery));
        }
    }
}