using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    // Класс с информацией о пользователе
    public class User
    {
        public string Id { get; protected set; } // id пользователя
        public string Name { get; protected set; } // имя пользователя
        public string LastName { get; protected set; } // фамилия пользователя
        public string Login { get; protected set; } // логин пользователя
        public string Password { get; protected set; } // пароль пользователя
        public string Role { get; protected set; } // роль пользователя: admin, worker or director
        public string Result { get; protected set; } // результативность пользователя

        public User(string login, string password)
        {
            this.Login = login;
            this.Password = password;
            this.Id = "-1";
            this.Name = "";
            this.LastName = "";
            this.Role = "";
            this.Result = "-2";
        }

        public User(string id,string lastName, string name,  string login, string password, string role, string result)
        {
            this.Id = id;
            this.Name = name;
            this.LastName = lastName;
            this.Login = login;
            this.Password = password;
            this.Role = role;
            this.Result = result;
        }

        public bool IsEqual(User another) // проверка равнества двух пользователей
        {
            if(another.Login == this.Login && another.Password == this.Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PressedResult() // если нажата функциональная кнопка
        {
            this.Result = (int.Parse(this.Result) + 1).ToString();
        }

        public void Change(string login, string password, string name, string lastname, string role)
        {
            this.Login = login;
            this.Password = password;
            this.Name = name;
            this.LastName = lastname;
            this.Role = role;
        }
    }
}
