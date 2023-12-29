namespace AnimeDB.Service.Models
{
    public partial class Anime
    {
        public partial class AniListEntry
        {
            public long Id { get; set; }
            public string Title { get; set; }
            public string Season { get; set; }
            public long Year { get; set; }
            public string Type { get; set; }
            public string Url
            {
                get { return "https://anilist.co/anime/" + this.Id; }
            }

        }

        public partial class MyAnimeListEntry
        {
            public long Id { get; set; }
            public string Url
            {
                get { return "https://myanimelist.net/anime/" + this.Id; }
            }

        }

        public AniListEntry AniList { get; set; }
        public MyAnimeListEntry MyAnimeList { get; set; }
    }
}
