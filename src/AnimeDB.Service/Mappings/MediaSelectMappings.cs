using AnimeDB.Data.Entities;
using AnimeDB.Service.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeDB.Service.Mappings
{
    internal static class MediaSelectMappings
    {
        public static Anime.AniListEntry ToModel(this AniList aniList)
        {
            return new Anime.AniListEntry
            {
                Id = aniList.Id,
                Title = aniList.Title,
                Season = aniList.Season,
                Year = aniList.SeasonYear.GetValueOrDefault(),
                Type = aniList.Type
            };
        }
        public static Anime.MyAnimeListEntry ToModel(this MyAnimeList myAnimeList)
        {
            return new Anime.MyAnimeListEntry
            {
                Id = myAnimeList.Id
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
                AppHidden = theme.AppHidden.GetValueOrDefault() == 1,
                IsFavorite = theme.IsFavorite.GetValueOrDefault() == 1,
                IsBad = theme.IsBad.GetValueOrDefault() == 1
            };
        }

        public static Models.Media ToModel(this Data.Entities.Media media)
        {
            return new Models.Media
            {
                Id = media.Id,
                Title = media.Title,
                Address = media.Address,
                Channel = media.Channel,
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
