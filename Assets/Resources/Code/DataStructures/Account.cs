using System;
using UnityEngine;

namespace Resources.Code.DataStructures
{
    public class Account: MonoBehaviour
    {
        [SerializeField] public String login;
        [SerializeField] public String password;
        [SerializeField] public String adminKey;
    }
}