using System;
using System.Collections.Generic;

namespace AnimeDB.Data.Entities
{
    public partial class Personal
    {
        public long UserId { get; set; }
        public long AniListId { get; set; }
        public string Status { get; set; }
        public string CreatedOn { get; set; }

        public virtual AniList AniList { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
