using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesStore.Models
{
    public class YearValidationAttribute : ValidationAttribute
    {
        private const int YearReleaseFirstFilm = 1895;

        public override bool IsValid(object value)
        {
            if (int.TryParse(value.ToString(), out int year))
                if (year >= YearReleaseFirstFilm && year <= DateTime.Now.Year)
                    return true;
            return false;
        }
    }
}
