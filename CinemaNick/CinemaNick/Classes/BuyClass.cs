using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaNick
{
    class BuyClass // класс для вывода всей информации покупки
    {
        int idBuyMovie; // id покупки

        int idAccount; // ID аккаунта
        string login; // Логин

        string name; // Наименование фильма
        DateTime dateCreate; // Дата выпуска фильма
        string imageLink; // Ссылка на картинку фильма
        int price; // цена фильма

        DateTime dateBuy; // дата покупки

        public int IdBuyMovie { get => idBuyMovie; set => idBuyMovie = value; }
        public int IdAccount { get => idAccount; set => idAccount = value; }
        public string Login { get => login; set => login = value; }
        public string Name { get => name; set => name = value; }
        public DateTime DateCreate { get => dateCreate; set => dateCreate = value; }
        public string ImageLink { get => imageLink; set => imageLink = value; }
        public int Price { get => price; set => price = value; }
        public DateTime DateBuy { get => dateBuy; set => dateBuy = value; }
    }
}
