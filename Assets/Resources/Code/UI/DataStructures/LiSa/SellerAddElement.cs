using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class SellerAddElement: DataStructureElement
    {
        [SerializeField] private TMP_InputField inputFieldLogin;
        [SerializeField] private TMP_InputField inputFieldName;
        [SerializeField] private TMP_InputField inputFieldRate;

        public String FormOutputValue()
        {
            if (inputFieldLogin.text == "" && inputFieldName.text == "")
            {
                UIQuerySender.Instance.AddCommand(new UICommand("Some fields are empty",
                    UICommandType.ShowException));
            }

            return inputFieldName.text.DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + inputFieldLogin.text.DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + (inputFieldRate.text == "" ? "0" : inputFieldRate.text);
        }

        public void SendAddQuery()
        {
            if (!inputFieldLogin.enabled) return;
            
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"gsa;" + FormOutputValue(),
                UICommandType.SendQuery));
            UIQuerySender.Instance.AddCommand(new UICommand(UICommandType.ContinueSellersAdding));

            inputFieldLogin.enabled = false;
            inputFieldName.enabled = false;
            inputFieldRate.enabled = false;
        }
    }
}