using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Author> GetAuthor()
        {
            // 4. Get the author who wrote the book with the longest title
            // If there are multiple, return the one with the smallest Id
            var author = await _context.Books
                .OrderByDescending(b => b.Title.Length)
                .ThenBy(b => b.AuthorId)
                .Select(b => b.Author)
                .FirstOrDefaultAsync();

            return author;
        }

        public async Task<List<Author>> GetAuthors()
        {
            // 3. Get authors who wrote an even number of books published after 2015
            DateTime year2015 = new DateTime(2015, 1, 1);

            var authors = await _context.Authors
    .Where(a => a.Books.Count(b => b.PublishDate > year2015) > 0 &&
                a.Books.Count(b => b.PublishDate > year2015) % 2 == 0)
    .ToListAsync();

            return authors;
        }
    }
}
