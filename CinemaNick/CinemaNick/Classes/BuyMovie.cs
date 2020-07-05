using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaNick
{
    public class BuyMovie
    {
        int idAccount; // ID аккаунта
        int idMovie; // ID Фильма
        DateTime dateBuy; // Дата покупки

        [PrimaryKey, AutoIncrement]
        public int IdBuyMovie { get; set; }
        public int IdAccount { get => idAccount; set => idAccount = value; }
        public int IdMovie { get => idMovie; set => idMovie = value; }
        public DateTime DateBuy { get => dateBuy; set => dateBuy = value; }
    }
}
