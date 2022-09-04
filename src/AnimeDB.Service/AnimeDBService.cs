using AnimeDB.Data;
using AnimeDB.Service.Mappings;
using AnimeDB.Service.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimeDB.Service
{
    public class AnimeDBService
    {
        private AnimeDBContext AnimeDBContext;

        public AnimeDBService(AnimeDBContext animeDBContext)
        {
            this.AnimeDBContext = animeDBContext;
        }

        public List<long> GetThemesWithNoDownloads()
        {
            var themesWithNoDownloads = this.AnimeDBContext.Theme.FromSqlRaw("SELECT T.* FROM Theme T WHERE (T.AppHidden IS NULL OR T.AppHidden = 0) AND T.Id NOT IN ( SELECT DISTINCT _T.Id FROM Download _D JOIN Media _M on _D.KeyId = _M.Id JOIN Theme _T ON _M.ThemeId = _T.Id)");

            var result = from theme in themesWithNoDownloads
                         select theme.Id;

            return result.ToList();
        }

        public MediaSelect GetMediaToSelect(long themeId)
        {
            var myAnimeListSources = from source in this.AnimeDBContext.Source
                                     join sourceType in this.AnimeDBContext.SourceType on source.SourceTypeId equals sourceType.Id
                                     where sourceType.Name == "MyAnimeList"
                                     select source;

            var result = from theme in this.AnimeDBContext.Theme
                         join source in myAnimeListSources on theme.KeyId equals source.KeyId
                         join myanimelist in this.AnimeDBContext.MyAnimeList on theme.Key.ExternalId equals myanimelist.Id
                         join myanimelist_anilist in this.AnimeDBContext.AniListMyAnimeList on myanimelist.Id equals myanimelist_anilist.MyAnimeListId
                         join anilist in this.AnimeDBContext.AniList on myanimelist_anilist.AniListId equals anilist.Id
                         where theme.Id == themeId
                         select new MediaSelect
                         {
                             Anime = anilist.ToModel(),
                             Theme = theme.ToModel(),
                             MediaCollection = theme.Media.ToModel()
                         };

            return result.FirstOrDefault();
        }

        public int UpdateMediaSelected(long themeId, long mediaId)
        {
            var theme = this.AnimeDBContext.Theme.Where(theme => theme.Id == themeId).FirstOrDefault();

            if (theme != null)
            {
                foreach (var media in theme.Media)
                {
                    media.IsFinalChoice = media.Id == mediaId ? 1 : 0;
                }
            }

            return this.AnimeDBContext.SaveChanges();
        }

        public int UpdateThemeHide(long themeId, bool hide)
        {
            var theme = this.AnimeDBContext.Theme.Where(theme => theme.Id == themeId).FirstOrDefault();

            if (theme != null)
            {
                theme.AppHidden = hide ? 1 : 0;
            }

            return this.AnimeDBContext.SaveChanges();
        }
    }
}
