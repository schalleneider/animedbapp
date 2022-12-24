using AnimeDB.Data.Entities;
using System.Collections.Generic;

namespace AnimeDB.Service.Models
{
    public partial class Media
    {
        private enum MediaPropertyStatus : int
        {
            Worst = 0,
            Bad = 1,
            Neutral = 2,
            Good = 3,
            Best = 4
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Channel { get; set; }
        public string Duration { get; set; }
        public long DurationSeconds { get; set; }
        public long NumberOfViews { get; set; }
        public long NumberOfLikes { get; set; }
        public long SearchSequence { get; set; }
        public bool IsBestRank { get; set; }
        public bool IsFinalChoice { get; set; }
        public long Rank { get; set; }

        public int ChannelStatus { get; set; }

        public int DurationStatus
        {
            get
            {
                if (this.DurationSeconds < (1 * 60) || this.DurationSeconds > (10 * 60))
                    return (int)MediaPropertyStatus.Worst;

                if (this.DurationSeconds < (2 * 60) || this.DurationSeconds > (6 * 60))
                    return (int)MediaPropertyStatus.Bad;

                if (this.DurationSeconds > (3 * 60) || this.DurationSeconds < (5 * 60))
                    return (int)MediaPropertyStatus.Best; 
                
                if (this.DurationSeconds > (2 * 60) || this.DurationSeconds < (6 * 60))
                    return (int)MediaPropertyStatus.Good; 
                
                return (int)MediaPropertyStatus.Neutral;
            }
        }
        
        public int NumberOfViewsStatus
        {
            get
            {
                if (this.NumberOfViews > 100000)
                    return (int)MediaPropertyStatus.Best;

                if (this.NumberOfViews > 50000)
                    return (int)MediaPropertyStatus.Good; 
                
                if (this.NumberOfViews < 1000)
                    return (int)MediaPropertyStatus.Worst;

                if (this.NumberOfViews < 5000)
                    return (int)MediaPropertyStatus.Bad;

                return (int)MediaPropertyStatus.Neutral;
            }
        }

        public int NumberOfLikesStatus
        {
            get
            {
                if (this.NumberOfLikes > 5000)
                    return (int)MediaPropertyStatus.Best;

                if (this.NumberOfLikes > 1000)
                    return (int)MediaPropertyStatus.Good;

                if (this.NumberOfLikes < 50)
                    return (int)MediaPropertyStatus.Worst;

                if (this.NumberOfLikes < 100)
                    return (int)MediaPropertyStatus.Bad;

                return (int)MediaPropertyStatus.Neutral;
            }
        }

        public int SearchSequenceStatus
        {
            get
            {
                if (this.SearchSequence == 1)
                    return (int)MediaPropertyStatus.Best;

                return (int)MediaPropertyStatus.Neutral;
            }
        }

        public int IsBestRankStatus
        {
            get
            {
                if (this.IsBestRank)
                    return (int)MediaPropertyStatus.Best;

                return (int)MediaPropertyStatus.Neutral;
            }
        }

        public int RankStatus
        {
            get
            {
                if (this.Rank >= 9)
                    return (int)MediaPropertyStatus.Best;

                if (this.Rank >= 7)
                    return (int)MediaPropertyStatus.Good;

                if (this.Rank <= 3)
                    return (int)MediaPropertyStatus.Worst;

                if (this.Rank <= 5)
                    return (int)MediaPropertyStatus.Bad;

                return (int)MediaPropertyStatus.Neutral;
            }
        }

        public void CalculateChannelStatus(List<string> channelBlacklistList, List<string> channelWhitelistList)
        {
            this.ChannelStatus = (int)MediaPropertyStatus.Neutral;
            
            if (channelBlacklistList.Contains(this.Channel))
                this.ChannelStatus = (int)MediaPropertyStatus.Worst;
            
            if (channelWhitelistList.Contains(this.Channel))
                this.ChannelStatus = (int)MediaPropertyStatus.Best;
                
        }
    }
}
