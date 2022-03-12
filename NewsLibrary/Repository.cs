using Microsoft.EntityFrameworkCore;
using NewsLibrary.Contexts;
using NewsLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsLibrary
{
    public class Repository
    {
        #region Fields
        private readonly Context _context;
        public static string dbConnectionString =
            "server=localhost;Port=3306;UserId=root;Password=root1234;database=NewsDB;Connection Timeout=360;CharSet=utf8mb4;Persist Security Info=True";
        #endregion

        public Repository() => _context = new Context(dbConnectionString); 

        public Repository(string connection)
        {
            dbConnectionString = connection;
            _context = new Context(dbConnectionString);
        }

        public IEnumerable<NewsContain> GetNews() => _context.NewsContains.ToList();

        public async Task<IEnumerable<NewsContain>> GetNewsAsync() => await _context.NewsContains.ToListAsync(); 

        public void SetNews(IEnumerable<NewsContain> enumerableNews)
        {
            _context.NewsContains.AddRange(enumerableNews);
            _context.SaveChanges(); 
        }
        public async Task SetNewsAsync(IEnumerable<NewsContain> enumerableNews)
        {
            await _context.NewsContains.AddRangeAsync(enumerableNews);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<NewsContain> GetUniqueNews(IEnumerable<NewsContain> enumerableNews)
        {
            var links = _context.NewsContains.Select(x => x.Link).ToList();
            var truelist = enumerableNews.Where(x => !links.Contains(x.Link)).ToList();
            return truelist;
        } 
    }
}
