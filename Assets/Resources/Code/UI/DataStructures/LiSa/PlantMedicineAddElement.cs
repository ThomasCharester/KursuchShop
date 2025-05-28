using System;
using System.Linq;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class PlantMedicineAddElement: DataStructureElement
    {
        [SerializeField] private TMP_Dropdown dropdownPlantName;
        [SerializeField] private TMP_Dropdown dropdownMedicineName;

        public void Start()
        {
            base.Start();
            dropdownPlantName.AddOptions(SessionService.Plants.Select(x => x.PlantName).ToList());
            dropdownPlantName.RefreshShownValue();
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

            return dropdownPlantName.value.ToString() + DataParsingExtension.ValueSplitter +
                   dropdownMedicineName.value.ToString();
        }

        public void SendAddQuery()
        {
            if (!dropdownPlantName.enabled) return;

            UIQuerySender.Instance.AddCommand(new UICommand(
                $"ppma;" + FormOutputValue(),
                UICommandType.SendQuery));
            UIQuerySender.Instance.AddCommand(new UICommand(UICommandType.ContinuePlantsMedicinesAdding));

            dropdownPlantName.enabled = false;
            dropdownMedicineName.enabled = false;
        }
    }
}