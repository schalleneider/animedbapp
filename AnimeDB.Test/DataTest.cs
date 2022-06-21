using AnimeDB.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;

namespace AnimeDB.Test
{
    [TestFixture]
    public class DataTest
    {
        private SqliteConnection InitializeConnection()
        {
            return new SqliteConnection("Data Source=C:\\Users\\willi\\Desktop\\Code\\animedb\\database\\template.sqlite3");
        }

        private AnimeDBContext InitializeDbContext(SqliteConnection connection)
        {
            var options = new DbContextOptionsBuilder<AnimeDBContext>()
                    .UseSqlite(connection)
                    .Options;

            var context = new AnimeDBContext(options);

            context.Database.EnsureCreated();

            return context;
        }

        [Test]
        public async Task GetAllAsync()
        {
            // Arrange
            var connection = InitializeConnection();

            await connection.OpenAsync();

            try
            {
                using (var context = InitializeDbContext(connection))
                {
                    var anilist = await context.AniList.ToListAsync();
                }
            }
            finally
            {
                await connection.CloseAsync();
                await connection.DisposeAsync();
            }
        }
    }
}
