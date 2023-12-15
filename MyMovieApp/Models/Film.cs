namespace MyMovieApp.Models
{
    public class Film
    {
        public int ID { get; set; }
        public string? Name { get; set; }  // название фильма
        public string? Director { get; set; }  // режиссер
        public string? Genre { get; set; }  // жанр
        public int Year { get; set; }  // год
        public string? Description { get; set; }  // описание
        public string? Actors { get; set; }  // описание
        public string? Image { get; set; }  // путь к картинке
    }
}
