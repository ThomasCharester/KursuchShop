using TMPro;
using UnityEngine;

namespace Resources.Code
{
    public class AccountPanel : Panel
    {
        [Header("Modify")] [SerializeField] private TMP_InputField inputFieldLogin;
        [SerializeField] private TMP_InputField inputFieldPassword;
        [Header("Labels")] [SerializeField] private TMP_Text login;
        [SerializeField] private TMP_Text admin;

        public void SendModifyRequest()
        {
            if (inputFieldLogin.text == "" && inputFieldPassword.text == "")
            {
                UIQuerySender.Instance.AddCommand(new UICommand("Login and password is empty",
                    UICommandType.ShowException));
            }

            UIQuerySender.Instance.AddCommand(new UICommand(
                "as;" + (inputFieldLogin.text != ""
                    ? inputFieldLogin.text.DBReadable()
                    : UserSessionService.UserAccount.Login.DBReadable()) + DataParsingExtension.ValueSplitter +
                (inputFieldPassword.text != ""
                    ? inputFieldPassword.text.DBReadable()
                    : UserSessionService.UserAccount.Password.DBReadable()),
                UICommandType.SendQuery));

            inputFieldLogin.text = "";
            inputFieldPassword.text = "";
        }

        public new void Show()
        {
            base.Show();
            
            Clear();
        }

        public new void Toggle(bool show)
        {
            base.Toggle(show);
            
            if (show)
                Clear();
        }

        public new void Toggle()
        {
            base.Toggle();
            
            Clear();
        }

        public void Clear()
        {
            login.text = UserSessionService.UserAccount.Login;
            admin.text = UserSessionService.UserAccount.IsSeller ? "Seller" :
                UserSessionService.UserAccount.sv_cheats ? "Admin" : "User";
        }
    }
}