using System;
using Resources.Code.DataStructures;
using Resources.Code.DataStructures.LiSa;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code
{
    public class VerticalContainer : Container
    {
        [Header("Prefabs")] [SerializeField] private AccountElement accountPrefab;
        [SerializeField] private AccountAddElement accountAddPrefab;
        [SerializeField] private GameElement gamePrefab;
        [SerializeField] private GameAddElement gameAddPrefab;
        [SerializeField] private PaymentMethodElement pmPrefab;
        [SerializeField] private PaymentAddMethod pmAddPrefab;
        [SerializeField] private SellerElement sellerPrefab;
        [SerializeField] private SellerAddElement sellerAddPrefab;
        [SerializeField] private AdminKeyElement adminKeyPrefab;
        [SerializeField] private AdminKeyAddElement adminKeyAddPrefab;
        [SerializeField] private GoodElementAP goodPrefab;
        [SerializeField] private CartItemElement cartItemPrefab;

        public void ShowAccounts(String accounts)
        {
            Clear();

            if (accounts.Length == 0) return;

            foreach (var account in accounts.Split(DataParsingExtension.AdditionalQuerySplitter))
            {
                AccountElement temp = Instantiate(accountPrefab, transform);
                temp.account.Login = account.Split(DataParsingExtension.ValueSplitter)[0];
                temp.account.Password = account.Split(DataParsingExtension.ValueSplitter)[1];
                temp.account.AdminKey = account.Split(DataParsingExtension.ValueSplitter)[2];

                temp.ToggleEditMode(false);
                temp.UpdateTextValues();

                children.Add(temp);
            }
        }

        public void StartAccountsEdit(String accounts)
        {
            ShowAccounts(accounts);

            ContinueAccountsEdit();
        }
        public void ContinueAccountsEdit() => children.Add(Instantiate(accountAddPrefab, transform));
        public void ShowGames(String games)
        {
            Clear();
            
            if (SessionService.Games.Count == 0) return;
            
            foreach (var game in SessionService.Games)
            {
                GameElement temp = Instantiate(gamePrefab, transform);
                temp.game.gameName = game.gameName;

                temp.ToggleEditMode(false);
                temp.UpdateTextValues();

                children.Add(temp);
            }
        }

        public void StartGamesEdit(String games)
        {
            ShowGames(games);

            ContinueGamesEdit();
        }

        public void ContinueGamesEdit() => children.Add(Instantiate(gameAddPrefab, transform));
        public void ShowPaymentMethods(String paymentMethods)
        {
            Clear();

            if (SessionService.PaymentMethods.Count == 0) return;
            
            foreach (var method in SessionService.PaymentMethods)
            {
                PaymentMethodElement temp = Instantiate(pmPrefab, transform);
                temp.paymentMethod.methodName = method.methodName;

                temp.ToggleEditMode(false);
                temp.UpdateTextValues();
                
                children.Add(temp);
            }
        }

        public void StartPaymentMethodsEdit(String methods)
        {
            ShowPaymentMethods(methods);

            ContinuePaymentMethodsEdit();
        }

        public void ContinuePaymentMethodsEdit() => children.Add(Instantiate(pmAddPrefab, transform));

        public void ShowSellers(String sellers)
        {
            Clear();

            if (sellers.Length == 0) return;

            foreach (var seller in sellers.Split(DataParsingExtension.AdditionalQuerySplitter))
            {
                SellerElement temp = Instantiate(sellerPrefab, transform);
                temp.seller.Name = seller.Split(DataParsingExtension.ValueSplitter)[0];
                temp.seller.Login = seller.Split(DataParsingExtension.ValueSplitter)[1];
                temp.seller.Rate = int.Parse(seller.Split(DataParsingExtension.ValueSplitter)[2]);

                temp.ToggleEditMode(false);
                temp.UpdateTextValues();
                
                children.Add(temp);
            }
        }

        public void StartSellersEdit(String accounts)
        {
            ShowSellers(accounts);

            ContinueSellersEdit();
        }

        public void ContinueSellersEdit() => children.Add(Instantiate(sellerAddPrefab, transform));
        public void ShowAdminKey(String adminKeys)
        {
            Clear();

            if (adminKeys.Length == 0) return;

            foreach (var adminKey in adminKeys.Split(DataParsingExtension.AdditionalQuerySplitter))
            {
                AdminKeyElement temp = Instantiate(adminKeyPrefab, transform);
                temp.adminKey.adminKey = adminKey;

                temp.ToggleEditMode(false);
                temp.UpdateTextValues();
                
                children.Add(temp);
            }
        }

        public void StartAdminKey(String games)
        {
            ShowAdminKey(games);

            ContinueAdminKeyEdit();
        }

        public void ContinueAdminKeyEdit() => children.Add(Instantiate(adminKeyAddPrefab, transform));
        public void ShowGoods(String goods)
        {
            Clear();

            if (goods.Length == 0) return;

            foreach (var seller in goods.Split(DataParsingExtension.AdditionalQuerySplitter))
            {
                GoodElementAP temp = Instantiate(goodPrefab, transform);
                temp.good.goodName = seller.Split(DataParsingExtension.ValueSplitter)[0];
                temp.good.sellerName = seller.Split(DataParsingExtension.ValueSplitter)[1];
                temp.good.gameName = seller.Split(DataParsingExtension.ValueSplitter)[2];

                temp.UpdateTextValues();

                children.Add(temp);
            }
        }
        public void ShowCartItems()
        {

            Clear();

            if (SessionService.CartItems.Count == 0) return;
            
            foreach (var itemInfo in SessionService.CartItems)
            {
                CartItemElement temp = Instantiate(cartItemPrefab, transform);
                temp.cartItem.GoodName = itemInfo.GoodName;
                temp.cartItem.CartId = itemInfo.CartId;
                temp.cartItem.Quantity = itemInfo.Quantity;
                temp.cartItem.ItemId = itemInfo.ItemId;

                temp.UpdateTextValues();

                children.Add(temp);
            }
        }
    }
}