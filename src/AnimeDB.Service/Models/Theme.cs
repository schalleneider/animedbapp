namespace AnimeDB.Service.Models
{
    public partial class Theme
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public bool AppHidden { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsBad { get; set; }

        public string YoutubeSearchLink
        {
            get { return "https://www.youtube.com/results?search_query=" + this.Artist + "+" + this.Title; }
        }
    }
}
