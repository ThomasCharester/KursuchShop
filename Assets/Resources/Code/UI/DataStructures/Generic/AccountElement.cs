using System;
using Resources.Code.UI.DataStructures;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class AccountElement : DataStructureElement
    {
        public Account account = new();
        [SerializeField] private GameObject inputPanel;
        [SerializeField] private GameObject outputPanel;

        [SerializeField] private TMP_InputField inputFieldLogin;
        [SerializeField] private TMP_InputField inputFieldPassword;
        [SerializeField] private TMP_InputField inputFieldAdminKey;

        [SerializeField] private TMP_Text textLogin;
        [SerializeField] private TMP_Text textPassword;
        [SerializeField] private TMP_Text textAdminKey;

        public void ToggleEditMode(bool edit)
        {
            inputPanel.SetActive(edit);
            outputPanel.SetActive(!edit);
        }

        public void UpdateTextValues()
        {
            textLogin.text = account.Login;
            textPassword.text = account.Password;
            textAdminKey.text = account.AdminKey;
        }
        
        public String FormOutputValue()
        {
            return (inputFieldLogin.text == "" ? account.Login : inputFieldLogin.text).DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + (inputFieldPassword.text == "" ? account.Password : inputFieldPassword.text).DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + (inputFieldAdminKey.text == "" ? account.AdminKey : inputFieldAdminKey.text).DBReadable();
        }

        public void SendModifyQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"am;" + FormOutputValue() + DataParsingExtension.AdditionalQuerySplitter + account.AccountToStringDB(),
                UICommandType.SendQuery));
        }
        public void SendDeleteQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"ad;" + account.AccountToString(),
                UICommandType.SendQuery));
        }
    }
}