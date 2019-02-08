using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly NewsContext _context;

        public NewsController(NewsContext context)
        {
            _context = context; // wird über Service injected
        }

        // nachfolgender Code wurde noch nicht in eine Domain extrahiert. C > D > M

        // GET: api/news
        [HttpGet]
        public async Task<ActionResult<IEnumerable<News>>> GetNewsItems()
        {
            return await _context.News.Include(c => c.Articles).ThenInclude(c => c.Source).ToListAsync();
        }

        // GET: api/news/5
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetNewsItem(int id)
        {
            var newsItem = await _context.News
                .Include(c => c.Articles).ThenInclude(c => c.Source)
                .FirstOrDefaultAsync(c => c.NewsId == id);

            if (newsItem == null)
            {
                return NotFound();
            }

            return newsItem;
        }


        // GET: api/news/5/article/1
        [HttpGet("{id}/article/{articleId}")]
        public async Task<ActionResult<Article>> GetArticleItem(int id, int articleId)
        {
            var item = await _context.Articles.Include(c => c.Source).FirstOrDefaultAsync(c => c.ArticleId == articleId);
            
            if (item == null || item.NewsId != id)
            {
                return NotFound();
            }

            return item;
        }

        // POST: api/news
        [HttpPost]
        public async Task<ActionResult<News>> PostNewsItem(News item)
        {
            _context.News.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNewsItem), new { id = item.NewsId }, item);
        }

        // PUT: api/news/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewsItem(int id, News item)
        {
            if (id != item.NewsId)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/news/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsItem(int id)
        {
            var item = await _context.News.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.News.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}