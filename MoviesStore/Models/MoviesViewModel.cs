using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesStore.Models
{
    public class MoviesViewModel
    {
        public MoviesViewModel(IQueryable<Movie> movies, int page, int pageSize)
        {
            PageViewModel = new PageViewModel(movies.Count(), page, pageSize);
            Movies = movies.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public IEnumerable<Movie> Movies { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public bool IsMy { get; set; }
    }
}
