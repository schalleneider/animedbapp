using System;
using System.Collections.Generic;

namespace AnimeDB.Data.Entities
{
    public partial class Media
    {
        public Media()
        {
            Downloads = new HashSet<Download>();
        }

        public long Id { get; set; }
        public long? ThemeId { get; set; }
        public string KeyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public long? DurationSeconds { get; set; }
        public long? NumberOfViews { get; set; }
        public long? NumberOfLikes { get; set; }
        public long? SearchSequence { get; set; }
        public long? IsLicensed { get; set; }
        public long? IsBestRank { get; set; }
        public long? IsFinalChoice { get; set; }
        public long? Rank { get; set; }
        public string SearchType { get; set; }
        public string Address { get; set; }
        public string CreatedOn { get; set; }
        public string LastModifiedOn { get; set; }

        public virtual Theme Theme { get; set; }
        public virtual ICollection<Download> Downloads { get; set; }
    }
}
