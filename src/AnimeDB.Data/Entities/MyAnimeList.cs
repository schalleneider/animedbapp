using System;
using System.Collections.Generic;

namespace AnimeDB.Data.Entities
{
    public partial class MyAnimeList
    {
        public MyAnimeList()
        {
            AniListMyAnimeLists = new HashSet<AniListMyAnimeList>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Season { get; set; }
        public string SeasonYear { get; set; }
        public long? NumberOfEpisodes { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
        public string CreatedOn { get; set; }
        public string LastModifiedOn { get; set; }

        public virtual ICollection<AniListMyAnimeList> AniListMyAnimeLists { get; set; }
    }
}
