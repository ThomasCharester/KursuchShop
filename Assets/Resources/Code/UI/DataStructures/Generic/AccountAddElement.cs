using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;

namespace Resources.Code.DataStructures.LiSa
{
    public class AccountAddElement : DataStructureElement
    {
        [SerializeField] private TMP_InputField inputFieldLogin;
        [SerializeField] private TMP_InputField inputFieldPassword;
        [SerializeField] private TMP_InputField inputFieldAdminKey;

        public String FormOutputValue()
        {
            if (inputFieldLogin.text == "" && inputFieldPassword.text == "")
            {
                UIQuerySender.Instance.AddCommand(new UICommand("Login or password is empty",
                    UICommandType.ShowException));
            }

            return inputFieldLogin.text.DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + inputFieldPassword.text.DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + (inputFieldAdminKey.text == "" ? "NULL" : inputFieldAdminKey.text.DBReadable());
        }

        public void SendAddQuery()
        {
            if (!inputFieldLogin.enabled) return;
            
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"aa;" + FormOutputValue(),
                UICommandType.SendQuery));
            UIQuerySender.Instance.AddCommand(new UICommand(UICommandType.ContinueAccountAdding));

            inputFieldLogin.enabled = false;
            inputFieldPassword.enabled = false;
            inputFieldAdminKey.enabled = false;
        }
    }
}