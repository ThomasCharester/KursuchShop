using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class PlantDiseaseAddElement: DataStructureElement
    {
        [SerializeField] private TMP_Dropdown dropdownPlantName;
        [SerializeField] private TMP_Dropdown dropdownDiseaseName;

        public String FormOutputValue()
        {
            if (dropdownPlantName.value == 0 || dropdownDiseaseName.value == 0)
            {
                UIQuerySender.Instance.AddCommand(new UICommand("Field is empty",
                    UICommandType.ShowException));
            }

            return dropdownPlantName.value.ToString() + DataParsingExtension.ValueSplitter + dropdownDiseaseName.value.ToString();
        }

        public void SendAddQuery()
        {
            if (!dropdownPlantName.enabled) return;

            UIQuerySender.Instance.AddCommand(new UICommand(
                $"ppda;" + FormOutputValue(),
                UICommandType.SendQuery));
            UIQuerySender.Instance.AddCommand(new UICommand(UICommandType.ContinuePlantsDiseasesAdding));

            dropdownPlantName.enabled = false;
            dropdownDiseaseName.enabled = false;
        }
    }
}