using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesStore.Models
{
    public class Movie
    {
        private IFormFile posterImg;

        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите название фильма")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        [UIHint("MultilineText")]
        public string Description { get; set; }

        [Display(Name = "Год выхода")]
        [Required(ErrorMessage = "Введите год выхода")]
        [YearValidation]
        //[Range(1895, 2020, ErrorMessage = "Введите год в диапазоне от 1985 до 2020")]
        public int ReliseYear { get; set; }

        [Display(Name = "Режиссёр")]
        public string Director { get; set; }

        public byte[] Poster { get; set; }

        [Display(Name = "Постер")]
        public IFormFile PosterImg
        {
            get => posterImg;
            set
            {
                posterImg = value;
                if (posterImg != null)
                {
                    using var reader = new BinaryReader(posterImg.OpenReadStream());
                    Poster = reader.ReadBytes((int)posterImg.Length);
                }
                else
                {
                    Poster = null;
                }
            }
        }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }
    }
}
