using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code.DataStructures.LiSa
{
    public class AccountElement : MonoBehaviour, IUIElement
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

        [SerializeField] private UIScaling _uiScaling;

        public bool modifiyng = true;

        private void Start()
        {
            _uiScaling.StartAnimation();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Clear()
        {
            _uiScaling.DIE();
        }

        public void Toggle(bool show)
        {
            gameObject.SetActive(show);
        }

        public void Toggle()
        {
            _uiScaling.StartAnimation();
        }

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

        // public void SetInputValues()
        // {
        //     account.Login = inputFieldLogin.text;
        //     account.Password = inputFieldPassword.text;
        //     account.AdminKey = inputFieldAdminKey.text;
        // }

        public String FormOutputValue()
        {
            return (inputFieldLogin.text == "" ? account.Login : inputFieldLogin.text).DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + (inputFieldPassword.text == "" ? account.Password : inputFieldPassword.text).DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + (inputFieldAdminKey.text == "" ? account.AdminKey : inputFieldLogin.text).DBReadable();
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