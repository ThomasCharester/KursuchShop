using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class MedicineAdd: DataStructureElement
    {
        [SerializeField] private TMP_InputField inputFieldMedicineName;
        [SerializeField] private TMP_InputField inputFieldConcentration;

        public String FormOutputValue()
        {
            if (inputFieldMedicineName.text == "" || inputFieldConcentration.text == "")
            {
                UIQuerySender.Instance.AddCommand(new UICommand("Field is empty",
                    UICommandType.ShowException));
            }

            return inputFieldMedicineName.text.DBReadable() + DataParsingExtension.ValueSplitter + inputFieldConcentration.text;
        }

        public void SendAddQuery()
        {
            if (!inputFieldMedicineName.enabled) return;

            UIQuerySender.Instance.AddCommand(new UICommand(
                $"pma;" + FormOutputValue(),
                UICommandType.SendQuery));
            UIQuerySender.Instance.AddCommand(new UICommand(UICommandType.ContinueMedicinesAdding));

            inputFieldMedicineName.enabled = false;
            inputFieldConcentration.enabled = false;
        }
    }
}