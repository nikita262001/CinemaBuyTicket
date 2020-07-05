using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaNick
{
    public class Account
    {
        int idAccount; // ID аккаунта
        string login; // Логин
        string password; // Пароль
        string name; // Имя
        string surname; // Фамилия
        string patronymic; // Отчество
        int money; // Денег на счету 
        bool admin; // Администратор/Пользователь
        string imageLink; // Картинка

        [PrimaryKey, AutoIncrement]
        public int IdAccount { get => idAccount; set => idAccount = value; }
        public string Login { get => login; set => login = value; }
        public string Password { get => password; set => password = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Patronymic { get => patronymic; set => patronymic = value; }
        public int Money { get => money; set => money = value; }
        public bool Admin { get => admin; set => admin = value; }
        public string ImageLink { get => imageLink; set => imageLink = value; }
    }
}
