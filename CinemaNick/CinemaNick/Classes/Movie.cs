    using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaNick
{
    public class Movie
    {
        int idMovie; // ID фильма
        string name; // Наименование фильма
        DateTime dateCreate; // Дата выпуска фильмов
        string imageLink; // Ссылка на картинку фильма
        int price; // цена фильма


        [PrimaryKey, AutoIncrement]
        public int IdMovie { get => idMovie; set => idMovie = value; }
        public string Name { get => name; set => name = value; }
        public DateTime DateCreate { get => dateCreate; set => dateCreate = value; }
        public string ImageLink { get => imageLink; set => imageLink = value; }
        public int Price { get => price; set => price = value; }
    }
}
