using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class PlantAddElement: DataStructureElement
    {
        [SerializeField] private TMP_InputField inputFieldName;
        
        public String FormOutputValue()
        {
            if (inputFieldName.text == "")
            {
                UIQuerySender.Instance.AddCommand(new UICommand("Some fields are empty",
                    UICommandType.ShowException));
            }

            return inputFieldName.text.DBReadable();
        }

        public void SendAddQuery()
        {
            if (!inputFieldName.enabled) return;
            
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"ppa;" + FormOutputValue(),
                UICommandType.SendQuery));
            UIQuerySender.Instance.AddCommand(new UICommand(UICommandType.ContinuePlantsAdding));

            inputFieldName.enabled = false;
        }
    }
}