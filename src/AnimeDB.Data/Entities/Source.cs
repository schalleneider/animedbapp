using System;
using System.Collections.Generic;

namespace AnimeDB.Data.Entities
{
    public partial class Source
    {
        public Source()
        {
            Themes = new HashSet<Theme>();
        }

        public long Id { get; set; }
        public string KeyId { get; set; } = null!;
        public long? ExternalId { get; set; }
        public long? SourceTypeId { get; set; }
        public string CreatedOn { get; set; }

        public virtual SourceType SourceType { get; set; }
        public virtual ICollection<Theme> Themes { get; set; }
    }
}
