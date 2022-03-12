using System;
using System.ComponentModel.DataAnnotations;

namespace NewsLibrary.Models
{
    public class NewsContain
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public DateTime PubDate { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string Link { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        public string Source { get; set; }
    }
}
