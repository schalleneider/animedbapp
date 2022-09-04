using AnimeDB.Data.Entities;
using AnimeDB.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeDB.Service.Mappings
{
    internal static class MediaSelectMappings
    {
        public static Anime ToModel(this AniList anilist)
        {
            return new Anime
            {
                Id = anilist.Id,
                Title = anilist.Title,
                Season = anilist.Season,
                Year = anilist.SeasonYear.GetValueOrDefault(),
                Type = anilist.Type
            };
        }

        public static Models.Theme ToModel(this Data.Entities.Theme theme)
        {
            return new Models.Theme
            {
                Id = theme.Id,
                Key = theme.KeyId,
                Artist = theme.Artist,
                Title = theme.Title,
                Type = theme.Type,
                AppHidden = theme.AppHidden.GetValueOrDefault() == 1 
            };
        }

        public static Models.Media ToModel(this Data.Entities.Media media)
        {
            return new Models.Media
            {
                Id = media.Id,
                Title = media.Title,
                Duration = media.Duration,
                DurationSeconds = media.DurationSeconds.GetValueOrDefault(),
                NumberOfViews = media.NumberOfViews.GetValueOrDefault(),
                NumberOfLikes = media.NumberOfLikes.GetValueOrDefault(),
                SearchSequence = media.SearchSequence.GetValueOrDefault(),
                IsBestRank = Convert.ToBoolean(media.IsBestRank.GetValueOrDefault()),
                IsFinalChoice = Convert.ToBoolean(media.IsFinalChoice.GetValueOrDefault()),
                Rank = media.Rank.GetValueOrDefault()
            };
        }

        public static IEnumerable<Models.Media> ToModel(this IEnumerable<Data.Entities.Media> medias)
        {
            return medias.Select(media => media.ToModel()).OrderBy(media => media.SearchSequence).ToList();
        }
    }
}
