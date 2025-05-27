using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class DiseaseElement : DataStructureElement
    {
        public Disease Disease = new();
        [SerializeField] private GameObject inputPanel;
        [SerializeField] private GameObject outputPanel;

        [SerializeField] private TMP_InputField inputFieldDiseaseName;
        [SerializeField] private TMP_Text textDiseaseName;
        [SerializeField] private TMP_Text id;

        public void UpdateTextValues()
        {
            textDiseaseName.text = Disease.DiseaseName;
            id.text = Disease.Id.ToString();
        }

        public void ToggleEditMode(bool edit)
        {
            inputPanel.SetActive(edit);
            outputPanel.SetActive(!edit);
        }
        public String FormOutputValue()
        {
            return (inputFieldDiseaseName.text == "" ? Disease.DiseaseName : inputFieldDiseaseName.text).DBReadable();
        }

        public void SendModifyQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"pdm;" + FormOutputValue() + DataParsingExtension.AdditionalQuerySplitter + Disease.DiseaseName.DBReadable(),
                UICommandType.SendQuery));
        }

        public void SendDeleteQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"pdd;" + Disease.Id,
                UICommandType.SendQuery));
        }
    }
}