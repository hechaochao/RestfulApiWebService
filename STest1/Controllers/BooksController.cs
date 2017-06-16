using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
//using STest1.Models;
using STest1.DTOs;
using System.Linq.Expressions;

namespace STest1.Controllers
{
    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        private testEntities1 db = new testEntities1();

        private static readonly Expression<Func<Book, BookDto>> AsBookDto =
            x => new BookDto
            {
                Title = x.Title,
                Author = x.Author.Name,
                Genre = x.Genre
            };

        //Get api/Books
        [Route("")]
        public IQueryable<BookDto> GetBooks()
        {
            return db.Books.Include(b => b.Author).Select(AsBookDto);
        }

        //Get api/Books/5
        [Route("{id:int}")]
        [ResponseType(typeof(BookDto))]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            BookDto book = await db.Books.Include(b => b.Author)
                .Where(b => b.BookId == id)
                .Select(AsBookDto)
                .FirstOrDefaultAsync();
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        //get book details
        [Route("{id:int}/details")]
        [ResponseType(typeof(BookDetailDto))]
        public async Task<IHttpActionResult> GetBookDetail(int id)
        {
            var book = await (from b in db.Books.Include(b => b.Author)
                              where b.AuthorId == id
                              select new BookDetailDto
                              {
                                  Title = b.Title,
                                  Genre = b.Genre,
                                  PublishDate = b.PublishDate,
                                  Price = b.Price,
                                  Description = b.Description,
                                  Author = b.Author.Name
                              }).FirstOrDefaultAsync();
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        //get books by genre
        [Route("{genre}")]
        public IQueryable<BookDto> GetBookByGenre(string genre)
        {
            return db.Books.Include(b => b.Author)
                .Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                .Select(AsBookDto);
        }

        //get books by author
        [Route("~/api/authors/{authorId:int}/books")]
        public IQueryable<BookDto> GetBooksByAuthor(int authorId)
        {
            return db.Books.Include(b => b.Author)
                .Where(b => b.AuthorId == authorId)
                .Select(AsBookDto);
        }

        //get books by publication date
        [Route("date/{pubdate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        [Route("date/{*pubdate:datetime:regex(\\d{4}/\\d{2}/\\d{2})}")]  // new
        public IQueryable<BookDto> GetBooks(DateTime pubdate)
        {
            return db.Books.Include(b => b.Author)
                .Where(b => DbFunctions.TruncateTime(b.PublishDate)
                    == DbFunctions.TruncateTime(pubdate))
                    .Select(AsBookDto);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        // private BooksAPIContext db = new BooksAPIContext();

        // GET: api/Books
        // public IQueryable<Book> GetBooks()
        // {
        //     return db.Books;
        // }

        // GET: api/Books/5
        // [ResponseType(typeof(Book))]
        // public async Task<IHttpActionResult> GetBook(int id)
        // {
        //     Book book = await db.Books.FindAsync(id);
        //     if (book == null)
        //     {
        //         return NotFound();
        //     }

        //     return Ok(book);
        // }

        // PUT: api/Books/5
        // [ResponseType(typeof(void))]
        // public async Task<IHttpActionResult> PutBook(int id, Book book)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     if (id != book.BookId)
        //     {
        //         return BadRequest();
        //     }

        //     db.Entry(book).State = EntityState.Modified;

        //     try
        //     {
        //         await db.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!BookExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return StatusCode(HttpStatusCode.NoContent);
        // }

        // POST: api/Books
        //[ResponseType(typeof(Book))]
        // public async Task<IHttpActionResult> PostBook(Book book)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     db.Books.Add(book);
        //     await db.SaveChangesAsync();

        //     return CreatedAtRoute("DefaultApi", new { id = book.BookId }, book);
        // }

        // DELETE: api/Books/5
        // [ResponseType(typeof(Book))]
        // public async Task<IHttpActionResult> DeleteBook(int id)
        // {
        //     Book book = await db.Books.FindAsync(id);
        //     if (book == null)
        //     {
        //         return NotFound();
        //     }

        //     db.Books.Remove(book);
        //     await db.SaveChangesAsync();

        //     return Ok(book);
        // }

        // protected override void Dispose(bool disposing)
        // {
        //     if (disposing)
        //     {
        //         db.Dispose();
        //     }
        //     base.Dispose(disposing);
        // }

        // private bool BookExists(int id)
        // {
        //     return db.Books.Count(e => e.BookId == id) > 0;
        // }
    }
}