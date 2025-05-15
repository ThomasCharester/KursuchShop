using TMPro;
using UnityEngine;

namespace Resources.Code
{
    public class AuthorisationPanel : MonoBehaviour, IUIElement
    {
        [Header("Register")] [SerializeField] private TMP_InputField inputFieldLoginReg;
        [SerializeField] private TMP_InputField inputFieldPasswordReg;
        [SerializeField] private TMP_InputField inputFieldAdminKey;
        [Header("Login")] [SerializeField] private TMP_InputField inputFieldLogin;
        [SerializeField] private TMP_InputField inputFieldPassword;

        private UISliding _sliding;

        void Start()
        {
            _sliding = GetComponent<UISliding>();
        }

        public void ShowLogin() => _sliding.StartAnimation(false);
        public void ShowRegister() => _sliding.StartAnimation(true);

        public void Show()
        {
            // _sliding.StartAnimation(true);
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            // _sliding.StartAnimation(false);
            gameObject.SetActive(false);
        }

        public void Clear()
        {
        }

        public void Toggle(bool show)
        {
            // _sliding.StartAnimation(show);
            gameObject.SetActive(show);
        }

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
                inputFieldAdminKey.text.DBReadable(),
                UICommandType.SendQuery));

            inputFieldLoginReg.text = "";
            inputFieldPasswordReg.text = "";
            inputFieldAdminKey.text = "";
        }
    }
}