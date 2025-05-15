using TMPro;
using UnityEngine;

namespace Resources.Code
{
    public class AccountPanel : MonoBehaviour, IUIElement
    {
        private UISliding _sliding;

        [Header("Modify")] [SerializeField] private TMP_InputField inputFieldLogin;
        [SerializeField] private TMP_InputField inputFieldPassword;
        [SerializeField] private TMP_InputField inputFieldAdminKey;
        [Header("Labels")] [SerializeField] private TMP_Text login;
        [SerializeField] private TMP_Text admin;

        public void SendModifyRequest()
        {
            UIQuerySender.Instance.AddCommand(new UICommand(
                "as;" + inputFieldLogin.text.DBReadable() + DataParsingExtension.ValueSplitter +
                inputFieldPassword.text.DBReadable() + DataParsingExtension.ValueSplitter +
                inputFieldAdminKey.text.DBReadable(),
                UICommandType.SendQuery));

            inputFieldLogin.text = "";
            inputFieldPassword.text = "";
            inputFieldAdminKey.text = "";
        }

        void Start()
        {
            _sliding = GetComponent<UISliding>();
        }

        public void Show()
        {
            _sliding.StartAnimation(true);
            login.text = UserSessionService.UserAccount.login;
            admin.text = UserSessionService.UserAccount.sv_cheats ? "Admin" : "User";
            //gameObject.SetActive(true);
        }

        public void Hide()
        {
            _sliding.StartAnimation(false);
            //gameObject.SetActive(false);
        }

        public void Clear()
        {
            //
            login.text = UserSessionService.UserAccount.login;
            admin.text = UserSessionService.UserAccount.sv_cheats ? "Admin" : "User";
        }

        public void Toggle(bool show)
        {
            _sliding.StartAnimation(show);
            //gameObject.SetActive(show);
        }
    }
}