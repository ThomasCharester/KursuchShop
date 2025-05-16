using System;
using Resources.Code.DataStructures.LiSa;
using UnityEngine;
using UnityEngine.Serialization;

namespace Resources.Code
{
    public class VerticalContainer: MonoBehaviour
    {
        [Header("Prefabs")] [SerializeField] private AccountElement accountPrefab;
        [SerializeField] private AccountAddElement accountAddPrefab;

        private UISliding _sliding;

        void Start()
        {
            _sliding = GetComponent<UISliding>();
        }

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
            }
        }

        public void StartAccountsEdit(String accounts)
        {
            ShowAccounts(accounts);

            ContinueAccountsEdit();
        }
        public void ContinueAccountsEdit()
        {
            AccountElement temp = Instantiate(accountPrefab, transform);
            temp.ToggleEditMode(true);
        }

        public void Clear()
        {
            //
            foreach (var child in GetComponentsInChildren<UIScaling>())
                child.DIE();
        }
    }
}