using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures
{
    public class AdminKeyAddElement: DataStructureElement
    {
        [SerializeField] private TMP_InputField inputFieldAdminKey;

        public String FormOutputValue()
        {
            if (inputFieldAdminKey.text == "")
            {
                UIQuerySender.Instance.AddCommand(new UICommand("Field is empty",
                    UICommandType.ShowException));
            }

            return inputFieldAdminKey.text.DBReadable();
        }

        public void SendAddQuery()
        {
            if (!inputFieldAdminKey.enabled) return;

            UIQuerySender.Instance.AddCommand(new UICommand(
                $"aka;" + FormOutputValue(),
                UICommandType.SendQuery));
            UIQuerySender.Instance.AddCommand(new UICommand(UICommandType.ContinueAdminKeysAdding));

            inputFieldAdminKey.enabled = false;
        }
    }
}