using System;
using UnityEngine;

namespace Resources.Code.DataStructures
{
    public class Account 
    {
        public String Login;
        public String Password;
        public String AdminKey = "";
        public bool sv_cheats;

        public Account(string login = "NAN", string password = "NAN", bool sv_cheats = false)
        {
            Login = login;
            Password = password;
            this.sv_cheats = sv_cheats;
        }

    }
}