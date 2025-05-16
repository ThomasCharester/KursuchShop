using System;
using TMPro;
using UnityEngine;

namespace Resources.Code.DataStructures.LiSa
{
    public class AccountAddElement: MonoBehaviour, IUIElement
    {
        [SerializeField] private TMP_InputField inputFieldLogin;
        [SerializeField] private TMP_InputField inputFieldPassword;
        [SerializeField] private TMP_InputField inputFieldAdminKey;
        
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

        public String FormOutputValue()
        {
            if (inputFieldLogin.text == "" && inputFieldPassword.text == "")
            {
                UIQuerySender.Instance.AddCommand(new UICommand("Login or password is empty",
                    UICommandType.ShowException));
            }
            
            return inputFieldLogin.text.DBReadable()
                   + DataParsingExtension.ValueSplitter
                   +inputFieldPassword.text.DBReadable()
                   + DataParsingExtension.ValueSplitter
                   + (inputFieldAdminKey.text == "" ? "'NAN'" : inputFieldLogin.text).DBReadable();
        }

        public void SendAddQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                $"aa;" + FormOutputValue(),
                UICommandType.SendQuery));
        }
    }
}