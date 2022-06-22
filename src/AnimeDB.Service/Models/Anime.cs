namespace AnimeDB.Service.Models
{
    public partial class Anime
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Season { get; set; }
        public long Year { get; set; }
        public string Type { get; set; }
    }
}
