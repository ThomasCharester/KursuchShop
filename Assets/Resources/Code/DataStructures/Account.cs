using System;
using UnityEngine;

namespace Resources.Code.DataStructures
{
    public class Account 
    {
        public String login;
        public String password;
        public bool sv_cheats;

        public void SetValues(string login, string password, bool sv_cheats)
        {
            this.login = login;
            this.password = password;
            this.sv_cheats = sv_cheats;
        }
    }
}