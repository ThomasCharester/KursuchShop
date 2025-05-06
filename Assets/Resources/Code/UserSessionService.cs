using Resources.Code.DataStructures;

namespace Resources.Code
{
    public class UserSessionService
    {
        private static UserSessionService _instance;

        public static UserSessionService Instance
        {
            get { return _instance; }
            private set { }
        }
        private static Account _account;

        public static Account UserAccount
        {
            get { return _account; }
            private set { }
        }

        public static void SetAccount(Account account)
        {
            
        }
    }
}