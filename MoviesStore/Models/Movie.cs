using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesStore.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReliseYear { get; set; }
        public string Director { get; set; }
        public byte[] Poster { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
