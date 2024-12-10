using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Book> GetBook()
        {
            // 2. Get the book with the highest cost of published copies
            var book = await _context.Books
                .OrderByDescending(b => b.Price * b.QuantityPublished)
                .FirstOrDefaultAsync();

            return book;
        }

        public async Task<List<Book>> GetBooks()
        {
            // 1. Get books with "Red" in the title and published after "Carolus Rex" (released June 25, 2012)
            DateTime carolusRexReleaseDate = new DateTime(2012, 6, 25);

            var books = await _context.Books
                .Where(b => b.Title.Contains("Red") && b.PublishDate > carolusRexReleaseDate)
                .ToListAsync();

            return books;
        }
    }
}
