using TMPro;
using UnityEngine;

namespace Resources.Code
{
    public class AuthorisationPanel : Panel
    {
        [Header("Register")] [SerializeField] private TMP_InputField inputFieldLoginReg;
        [SerializeField] private TMP_InputField inputFieldPasswordReg;
        [SerializeField] private TMP_InputField inputFieldAdminKey;
        [Header("Login")] [SerializeField] private TMP_InputField inputFieldLogin;
        [SerializeField] private TMP_InputField inputFieldPassword;

        public void ShowLogin() => _sliding.StartAnimation(false);
        public void ShowRegister() => _sliding.StartAnimation(true);

        public new void Show() => gameObject.SetActive(true);

        public new void Hide() => gameObject.SetActive(false);

        public new void Toggle(bool show) => gameObject.SetActive(show);
        public new void Toggle() => _sliding.StartAnimation();
        public void SendLoginQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                "l;" + inputFieldLogin.text.DBReadable() + DataParsingExtension.ValueSplitter +
                inputFieldPassword.text.DBReadable(),
                UICommandType.SendQuery));
            inputFieldLogin.text = "";
            inputFieldPassword.text = "";
        }

        public void SendRegisterQuery()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                "r;" + inputFieldLoginReg.text.DBReadable() + DataParsingExtension.ValueSplitter +
                inputFieldPasswordReg.text.DBReadable() + DataParsingExtension.ValueSplitter +
                (inputFieldAdminKey.text == "" ? "NULL" : inputFieldAdminKey.text.DBReadable()),
                UICommandType.SendQuery));

            inputFieldLoginReg.text = "";
            inputFieldPasswordReg.text = "";
            inputFieldAdminKey.text = "";
        }
    }
}