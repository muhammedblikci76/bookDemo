using bookDemo.Data;
using bookDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace bookDemo.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = ApplicationContex.Books.ToList();
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOneBooks([FromRoute(Name = "id")] int id)
        {
            var books = ApplicationContex.Books.Where(x => x.Id.Equals(id)).SingleOrDefault();

            if (books == null)
                return NotFound();//404

            return Ok(books);
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest();
                ApplicationContex.Books.Add(book);
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UptadeOneBook(int id, Book book)
        {
            var entity = ApplicationContex.Books.FirstOrDefault(x => x.Id == id);


            if (entity is null)
                return NotFound();

            if (id != book.Id)
                return BadRequest();

            ApplicationContex.Books.Remove(entity);
            book.Id = entity.Id;
            ApplicationContex.Books.Add(book);
            return NoContent();

        }

        [HttpDelete("OneBook")]
        public IActionResult DeleteOneBook(int id, Book book)
        {
            var entity = ApplicationContex.Books.FirstOrDefault(x => x.Id == id);

            if (entity is null)
                return NotFound();

            if (id != book.Id)
                return BadRequest();

            ApplicationContex.Books.Remove(entity);
            return Ok(book);

        }

        [HttpDelete("AllBooks")]
        public IActionResult DeleteAllBook()
        {
            ApplicationContex.Books.Clear();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            var entity=ApplicationContex.Books.Find(x => x.Id == id);

            if (entity is null)
                return NotFound();

            bookPatch.ApplyTo(entity);
            return NoContent();
        }
    }
}
