using System;
using System.Collections.Generic;

namespace AnimeDB.Data.Entities
{
    public partial class Theme
    {
        public Theme()
        {
            Media = new HashSet<Media>();
        }

        public long Id { get; set; }
        public string KeyId { get; set; }
        public string Theme1 { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public long? Sequence { get; set; }
        public string Algorithm { get; set; }
        public long? AppHidden { get; set; }
        public long? IsFavorite { get; set; }
        public long? IsBad { get; set; }
        public string CreatedOn { get; set; }

        public virtual Source Key { get; set; }
        public virtual ICollection<Media> Media { get; set; }
    }
}
