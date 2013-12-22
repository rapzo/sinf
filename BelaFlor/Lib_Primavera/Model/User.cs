using Interop.StdBE800;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BelaFlor.Lib_Primavera.Model
{
    public class User
    {
        public string Username
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string Token
        {
            get;
            set;
        }

        public bool Activate
        {
            get;
            set;
        }

        public static int activate(User u)
        {
            StdBELista objList;
            string usr, pw;

            if(u.Token.Equals(UserController.token))
            {
                if (PriEngine.InitializeCompany("BELAFLOR", "", "") == true)
                {
                    objList = PriEngine.Engine.Consulta("SELECT CDU_CampoVar1 as Username, CDU_CampoVar2 as Password FROM Clientes WHERE CDU_CampoVar1 = '" + u.Username + "'");
                    usr = objList.Valor("Username");
                    pw = objList.Valor("Password");

                    if (u.Password.Equals(pw))
                        return 1;
                }
            }
            
            return 0;

            //if (u.Token.Equals(UserController.token))
            //{
            //    for (int i = 0; i < UserController.count(); i++)
            //    {
            //        if (UserController.users.ElementAt(i).Username.Equals(u.Username))
            //        {
            //            if (UserController.users.ElementAt(i).Password.Equals(u.Password))
            //            {
            //                if (UserController.users.ElementAt(i).Activate == false)
            //                {
            //                    UserController.users.ElementAt(i).Activate = true;
            //                    return 1;
            //                }
            //                else if(UserController.users.ElementAt(i).Activate == true)
            //                {
            //                    return 2;
            //                }
            //            }
            //            else return 3;
            //        }
            //    }

            //    User u2 = new User();
            //    u2.Username = u.Username;
            //    u2.Password = u.Password;
            //    u2.Activate = true;

            //    UserController.add(u);
            //    return 0;
            //}

            //return 3;
        }
    }

    public static class UserController
    {
        public static List<User> users;
        private static string salt = "t4g9";
        public static string token = "88c8be2787ff0f96a4e29ea93f1a165945d2ad99";

        public static void add(User u)
        {
            if(users == null)
            {
                users = new List<User>();
                users.Add(u);
            }
            else users.Add(u);
        }

        public static int count()
        {
            if (users == null)
            {
                users = new List<User>();
                return users.Count;
            }
            else return users.Count;
        }
    }
}