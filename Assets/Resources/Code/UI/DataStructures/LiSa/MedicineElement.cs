using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class MedicineElement : DataStructureElement
    {
        public Medicine Medicine = new();
        [SerializeField] private GameObject inputPanel;
        [SerializeField] private GameObject outputPanel;

        [SerializeField] private TMP_InputField inputFieldMedicineName;
        [SerializeField] private TMP_InputField inputFieldConcentration;
        [SerializeField] private TMP_Text textMedicineName;
        [SerializeField] private TMP_Text id;
        [SerializeField] private TMP_Text concentration;

        public void UpdateTextValues()
        {
            textMedicineName.text = Medicine.MedicineName;
            id.text = Medicine.Id.ToString();
            concentration.text = Medicine.Concentration.ToString();
        }

        public void ToggleEditMode(bool edit)
        {
            inputPanel.SetActive(edit);
            outputPanel.SetActive(!edit);
        }

        public String FormOutputValue()
        {
            return (inputFieldMedicineName.text == "" ? Medicine.MedicineName : inputFieldMedicineName.text)
                   .DBReadable()
                   + DataParsingExtension.ValueSplitter + inputFieldConcentration.text;
        }

        public void SendModifyQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"pmm;" + FormOutputValue() + DataParsingExtension.AdditionalQuerySplitter 
                + Medicine.MedicineName.DBReadable()
                + DataParsingExtension.ValueSplitter
                + Medicine.Concentration,
                UICommandType.SendQuery));
        }

        public void SendDeleteQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"pmd;" + Medicine.Id,
                UICommandType.SendQuery));
        }
    }
}