using System;
using UnityEngine;

namespace Resources.Code.DataStructures
{
    public class Account : MonoBehaviour
    {
        [SerializeField] public String login;
        [SerializeField] public String password;
        [SerializeField] public String adminKey;

        public void SetValues(string login, string password, string adminKey)
        {
            this.login = login;
            this.password = password;
            this.adminKey = adminKey;
        }
    }
}