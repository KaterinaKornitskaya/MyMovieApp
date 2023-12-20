using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyMovieApp.Models
{
    public class Film
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Name { get; set; }  // название фильма

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Director { get; set; }  // режиссер

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Genre { get; set; }  // жанр

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Range(1900, 2023, ErrorMessage = "Недопустимый год")]
        public int Year { get; set; }  // год

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Description { get; set; }  // описание

        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Actors { get; set; }  // описание
      
        public string? Image { get; set; }  // путь к картинке
    }
}
