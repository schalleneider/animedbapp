using System;
using System.Collections.Generic;

namespace AnimeDB.Data.Entities
{
    public partial class SourceType
    {
        public SourceType()
        {
            Sources = new HashSet<Source>();
        }

        public long Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string CreatedOn { get; set; }

        public virtual ICollection<Source> Sources { get; set; }
    }
}
