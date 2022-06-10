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
using CLMS.Database;
using CLMS.Models;

namespace CLMS.Controllers
{
    public class RequestBooksController : ApiController
    {
        private LibraryContext db = new LibraryContext();

        // GET: api/RequestBooks
        public IQueryable<RequestBook> GetRequestBooks()
        {
            return db.RequestBooks;
        }

        // GET: api/RequestBooks/5
        [ResponseType(typeof(RequestBook))]
        public async Task<IHttpActionResult> GetRequestBook(int id)
        {
            RequestBook requestBook = await db.RequestBooks.FindAsync(id);
            if (requestBook == null)
            {
                return NotFound();
            }

            return Ok(requestBook);
        }

        // PUT: api/RequestBooks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRequestBook(int id, RequestBook requestBook)
        {
            var book = requestBook.BookId;
            AvailableBook available = await db.AvailableBooks.FindAsync(book);
            if (available != null)
            {
                available.Count--;
                db.SaveChanges();
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requestBook.RequestId)
            {
                return BadRequest();
            }

            db.Entry(requestBook).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestBookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/RequestBooks
        [ResponseType(typeof(RequestBook))]
        public async Task<IHttpActionResult> PostRequestBook(RequestBook requestBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RequestBooks.Add(requestBook);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = requestBook.RequestId }, requestBook);
        }

        // DELETE: api/RequestBooks/5
        [Route("api/RequestBooks/DeleteRequestBook")]
        [ResponseType(typeof(RequestBook))]
        public async Task<IHttpActionResult> DeleteRequestBook(int id)
        {
            
            var requestBook = await db.RequestBooks.FindAsync(id);
            var bookid = requestBook.BookId;
            var count = await db.AvailableBooks.FindAsync(bookid);
            if (count != null)
            {
                count.Count++;
                db.SaveChanges();
            }
            if (requestBook == null)
            {
                return NotFound();
            }


            db.RequestBooks.Remove(requestBook);
            await db.SaveChangesAsync();

            return Ok(requestBook);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestBookExists(int id)
        {
            return db.RequestBooks.Count(e => e.RequestId == id) > 0;
        }
    }
}