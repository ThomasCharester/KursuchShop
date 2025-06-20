using System.Collections.Generic;
using Resources.Code.DataStructures;
using Resources.Code.DataStructures.LiSa;

namespace Resources.Code
{
    public class SessionService
    {
        // private static UserSessionService _instance;
        //
        // public static UserSessionService Instance
        // {
        //     get { return _instance; }
        //     private set { }
        // }
        private static List<CartItem> _cartItems = new();
        public static List<CartItem> CartItems => _cartItems;

        public static void UpdateCartItems(string items)
        {
            _cartItems.Clear();

            if (items.Length != 0)
                foreach (var item in items.Split(DataParsingExtension.AdditionalQuerySplitter))
                    _cartItems.Add(new CartItem(int.Parse(item.Split(DataParsingExtension.ValueSplitter)[0]),
                        int.Parse(item.Split(DataParsingExtension.ValueSplitter)[1]),
                        item.Split(DataParsingExtension.ValueSplitter)[2],
                        int.Parse(item.Split(DataParsingExtension.ValueSplitter)[3])));
        }
        private static List<Game> _games = new();
        public static List<Game> Games => _games;

        public static void UpdateGames(string items)
        {
            _games.Clear();

            if (items.Length != 0)
                foreach (var item in items.Split(DataParsingExtension.AdditionalQuerySplitter))
                    _games.Add(new Game(item));
        }

        private static List<PaymentMethod> _paymentMethods = new();
        public static List<PaymentMethod> PaymentMethods => _paymentMethods;

        public static void UpdatePaymentMethods(string items)
        {
            _paymentMethods.Clear();

            if (items.Length != 0)
                foreach (var item in items.Split(DataParsingExtension.AdditionalQuerySplitter))
                    _paymentMethods.Add(new PaymentMethod(item));
        }
        
        private static Account _account;

        public static Account UserAccount
        {
            get { return _account; }
            private set { }
        }

        public static void SetAccount(Account account)
        {
            _account = account;
        }

        public static bool isAdmin()
        {
            return _account.sv_cheats;
        }
    }
}