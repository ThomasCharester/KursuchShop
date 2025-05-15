using TMPro;
using UnityEngine;

namespace Resources.Code
{
    public class AccountPanel : MonoBehaviour, IUIElement
    {
        private UISliding _sliding;

        [Header("Modify")] [SerializeField] private TMP_InputField inputFieldLogin;
        [SerializeField] private TMP_InputField inputFieldPassword;
        [Header("Labels")] [SerializeField] private TMP_Text login;
        [SerializeField] private TMP_Text admin;

        private bool _hidden = true;
        public bool Hidden => _hidden;

        public void SendModifyRequest()
        {
            // if (inputFieldLogin.text == "" && inputFieldPassword.text == "")
            // {
            //     UIQuerySender.Instance.AddCommand(new UICommand("Login or password is empty",
            //         UICommandType.ShowException));
            // }

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

        void Start()
        {
            _sliding = GetComponent<UISliding>();
        }

        public void Show()
        {
            _sliding.StartAnimation(true);
            Clear();
            _hidden = false;
            //gameObject.SetActive(true);
        }

        public void Hide()
        {
            _sliding.StartAnimation(false);
            _hidden = true;
            //gameObject.SetActive(false);
        }

        public void Clear()
        {
            login.text = UserSessionService.UserAccount.Login;
            admin.text = UserSessionService.UserAccount.IsSeller ? "Seller" :
                UserSessionService.UserAccount.sv_cheats ? "Admin" : "User";
        }

        public void Toggle(bool show)
        {
            _sliding.StartAnimation(show);
            if (show)
                Clear();
            

            //gameObject.SetActive(show);
            _hidden = !show;
        }

        public void Toggle()
        {
            _hidden = !_hidden;
            _sliding.StartAnimation();

            if (_hidden)
                Clear();
        }
    }
}