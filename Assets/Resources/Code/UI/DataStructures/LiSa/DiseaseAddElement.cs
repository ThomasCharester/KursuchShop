using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class DiseaseAddElement : DataStructureElement
    {
        [SerializeField] private TMP_InputField inputFieldDiseaseName;

        public String FormOutputValue()
        {
            if (inputFieldDiseaseName.text == "")
            {
                UIQuerySender.Instance.AddCommand(new UICommand("Field is empty",
                    UICommandType.ShowException));
            }

            return inputFieldDiseaseName.text.DBReadable();
        }

        public void SendAddQuery()
        {
            if (!inputFieldDiseaseName.enabled) return;

            UIQuerySender.Instance.AddCommand(new UICommand(
                $"pda;" + FormOutputValue(),
                UICommandType.SendQuery));
            UIQuerySender.Instance.AddCommand(new UICommand(UICommandType.ContinueDiseasesAdding));

            inputFieldDiseaseName.enabled = false;
        }
    }
}