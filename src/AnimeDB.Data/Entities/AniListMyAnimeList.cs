using System;
using System.Collections.Generic;

namespace AnimeDB.Data.Entities
{
    public partial class AniListMyAnimeList
    {
        public long AniListId { get; set; }
        public long MyAnimeListId { get; set; }
        public string CreatedOn { get; set; }
        public string LastModifiedOn { get; set; }

        public virtual AniList AniList { get; set; } = null!;
        public virtual MyAnimeList MyAnimeList { get; set; } = null!;
    }
}
