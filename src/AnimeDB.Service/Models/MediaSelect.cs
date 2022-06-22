using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeDB.Service.Models
{
    public class MediaSelect
    {
        public Anime Anime { get; set; }
        public Theme Theme { get; set; }
        public IEnumerable<Media> MediaCollection { get; set; }
    }
}
