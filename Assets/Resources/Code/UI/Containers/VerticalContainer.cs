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

        public void ShowAccounts(String accounts)
        {
            if (accounts.Length == 0) return;

            Clear();

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
            if (games.Length == 0) return;

            Clear();

            foreach (var gameName in games.Split(DataParsingExtension.AdditionalQuerySplitter))
            {
                GameElement temp = Instantiate(gamePrefab, transform);
                temp.game.gameName = gameName;

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
            if (paymentMethods.Length == 0) return;

            Clear();

            foreach (var methodName in paymentMethods.Split(DataParsingExtension.AdditionalQuerySplitter))
            {
                PaymentMethodElement temp = Instantiate(pmPrefab, transform);
                temp.paymentMethod.methodName = methodName;

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
            if (sellers.Length == 0) return;

            Clear();

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
            if (adminKeys.Length == 0) return;

            Clear();

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
            if (goods.Length == 0) return;

            Clear();

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
    }
}