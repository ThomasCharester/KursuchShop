using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class PaymentAddMethod: DataStructureElement
    {
        [SerializeField] private TMP_InputField inputFieldMethodName;

        public String FormOutputValue()
        {
            if (inputFieldMethodName.text == "")
            {
                UIQuerySender.Instance.AddCommand(new UICommand("Field is empty",
                    UICommandType.ShowException));
            }

            return inputFieldMethodName.text.DBReadable();
        }

        public void SendAddQuery()
        {
            if (!inputFieldMethodName.enabled) return;

            UIQuerySender.Instance.AddCommand(new UICommand(
                $"gpa;" + FormOutputValue(),
                UICommandType.SendQuery));
            UIQuerySender.Instance.AddCommand(new UICommand(UICommandType.ContinuePaymentMethodsAdding));

            inputFieldMethodName.enabled = false;
        }
    }
}