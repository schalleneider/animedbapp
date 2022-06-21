using System;
using System.Collections.Generic;

namespace AnimeDB.Data.Entities
{
    public partial class AniList
    {
        public AniList()
        {
            AniListMyAnimeLists = new HashSet<AniListMyAnimeList>();
            Personals = new HashSet<Personal>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public string Season { get; set; }
        public long? SeasonYear { get; set; }
        public string Genres { get; set; }
        public long? NumberOfEpisodes { get; set; }
        public string StartDate { get; set; }
        public long? StartWeekNumber { get; set; }
        public string StartDayOfWeek { get; set; }
        public long? HasPrequel { get; set; }
        public long? HasSequel { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string CreatedOn { get; set; }
        public string LastModifiedOn { get; set; }

        public virtual ICollection<AniListMyAnimeList> AniListMyAnimeLists { get; set; }
        public virtual ICollection<Personal> Personals { get; set; }
    }
}
