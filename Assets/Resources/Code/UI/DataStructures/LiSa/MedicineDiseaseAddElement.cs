using System;
using System.Linq;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class MedicineDiseaseAddElement: DataStructureElement
    {
        [SerializeField] private TMP_Dropdown dropdownMedicineName;
        [SerializeField] private TMP_Dropdown dropdownDiseaseName;
        [SerializeField] private TMP_InputField inputFieldMinDosage;
        [SerializeField] private TMP_InputField inputFieldMaxDosage;

        public void Start()
        {
            base.Start();
            dropdownDiseaseName.AddOptions(SessionService.Diseases.Select(x => x.DiseaseName).ToList());
            dropdownDiseaseName.RefreshShownValue();
            dropdownMedicineName.AddOptions(SessionService.Medicines.Select(x => x.MedicineName).ToList());
            dropdownMedicineName.RefreshShownValue();
        }

        public String FormOutputValue()
        {
            // if (dropdownPlantName.value == 0 || dropdownDiseaseName.value == 0)
            // {
            //     UIQuerySender.Instance.AddCommand(new UICommand("Field is empty",
            //         UICommandType.ShowException));
            // }

            return dropdownDiseaseName.value.ToString() 
                   + DataParsingExtension.ValueSplitter 
                   + dropdownMedicineName.value.ToString() 
                   + DataParsingExtension.ValueSplitter
                   + inputFieldMinDosage.text 
                   + DataParsingExtension.ValueSplitter
                   + inputFieldMaxDosage.text;
        }

        public void SendAddQuery()
        {
            if (!dropdownDiseaseName.enabled) return;

            UIQuerySender.Instance.AddCommand(new UICommand(
                $"pmda;" + FormOutputValue(),
                UICommandType.SendQuery));
            UIQuerySender.Instance.AddCommand(new UICommand(UICommandType.ContinueMedicinesDiseasesAdding));

            dropdownDiseaseName.enabled = false;
            dropdownMedicineName.enabled = false;
            inputFieldMinDosage.enabled = false;
            inputFieldMaxDosage.enabled = false;
        }
    }
}